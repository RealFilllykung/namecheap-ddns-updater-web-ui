using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using record_service.infrastructures.databases;
using record_service.infrastructures.interfaces.repositories;
using record_service.infrastructures.interfaces.services;
using record_service.models;
using record_service.models.exceptions;
using record_service.models.requests;
using record_service.models.responses;

namespace record_service.services;

public class RecordService : IRecordService
{
    private readonly ILogger<RecordService> _logger;
    private readonly IPasswordRepository _passwordRepository;
    private readonly DatabaseContext _databaseContext;
    private readonly IDdnsService _ddnsService;

    public RecordService(ILogger<RecordService> logger, IPasswordRepository passwordRepository, DatabaseContext databaseContext, IDdnsService ddnsService)
    {
        _logger = logger;
        _passwordRepository = passwordRepository;
        _databaseContext = databaseContext;
        _ddnsService = ddnsService;
    }

    public async Task CreateRecord(CreateRecordRequest request)
    {
        EncryptPasswordResponse? encryptPasswordResponse;

        using (_passwordRepository)
        {
            _logger.LogInformation($"Requesting password encryption");
            encryptPasswordResponse = await _passwordRepository.EncryptPassword(request.password.Replace(" ",""));
            _logger.LogInformation($"Done encrypting password: {encryptPasswordResponse?.encryptedPassword}");
        }

        await using (_databaseContext)
        {
            _logger.LogInformation($"Recording the record into database.");
            RecordModel? recordModel = new RecordModel
            {
                encryptedPassword = encryptPasswordResponse?.encryptedPassword,
                domain = request.domain
            };
            try
            {
                _databaseContext.Records.Add(recordModel);
                await _databaseContext.SaveChangesAsync();

                using (_ddnsService)
                {
                    try
                    {
                        await _ddnsService.UpdateDdnsRecord(recordModel.domain);
                    }
                    catch (Exception)
                    {
                        _databaseContext.Records.Remove(recordModel);
                        await _databaseContext.SaveChangesAsync();
                        throw new CreateRecordException("The provided record and password information cannot update DDNS record.");
                    }
                    
                }
            }
            catch (Exception exception)
            {
                throw new CreateRecordException(exception.Message);
            }
            _logger.LogInformation($"Successfully created new record for {request.domain}");
        }
    }

    public async Task<GetRecordResponse?> GetRecordByDomainName(string domainName)
    {
        _logger.LogInformation($"Getting record {domainName} from database");
        RecordModel? recordModel;
        await using (_databaseContext)
        {
            try
            {
                recordModel = await _databaseContext.Records.FindAsync(domainName);
            }
            catch (Exception exception)
            {
                throw new GetRecordException("There is an error while trying to get the record. Please see logs for more information.");
            }
        }
        
        _logger.LogInformation($"Found record {domainName}");
        
        _logger.LogInformation($"Building response to the client of the record {domainName}");
        GetRecordResponse getRecordResponse = await MapToGetRecordResponse(recordModel);
        
        _logger.LogInformation($"Returning {domainName} record detail to the client");
        return getRecordResponse;
    }

    public async Task<List<GetRecordResponse>> GetRecords()
    {
        _logger.LogInformation($"Getting records from database");
        List<RecordModel?> response;
        await using (_databaseContext)
        {
            try
            {
                response = await _databaseContext.Records.ToListAsync();
            }
            catch (Exception e)
            {
                throw new GetRecordException("There is an error while trying to get all record. Please see logs for more information.");
            }
            _logger.LogInformation($"Found {response.Count} records");
        }

        using (_passwordRepository)
        {
            _logger.LogInformation($"Building response to the client of the records");
            List<GetRecordResponse> getRecordResponses = new List<GetRecordResponse>();
            foreach (RecordModel? record in response)
            {
                GetRecordResponse getRecordResponse = await MapToGetRecordResponse(record);
                getRecordResponses.Add(getRecordResponse);
            }
        
            _logger.LogInformation($"Returning {response.Count} records");
            return getRecordResponses;
        }
    }

    public async Task UpdateRecord(UpdateRecordRequest record)
    {
        _logger.LogInformation($"Updating record {record.domain}");
        RecordModel? recordModel;
        
        await using (_databaseContext)
        {
            try
            {
                recordModel = await _databaseContext.Records.FindAsync(record.domain);
            }
            catch (Exception)
            {
                throw new GetRecordException("There is an error while trying to get the record before update the record. Please see logs for more information.");
            }

            string previousRecordModelString = JsonConvert.SerializeObject(recordModel);
            EncryptPasswordResponse? encryptPasswordResponse = await _passwordRepository.EncryptPassword(record.password.Replace(" ",""));
            recordModel.encryptedPassword = encryptPasswordResponse.encryptedPassword;
            try
            {
                _databaseContext.Records.Update(recordModel);
                _logger.LogInformation($"Saving new {recordModel.domain}");
                await _databaseContext.SaveChangesAsync();

                using (_ddnsService)
                {
                    try
                    {
                        await _ddnsService.UpdateDdnsRecord(recordModel.domain);
                    }
                    catch (Exception)
                    {
                        _databaseContext.Records.Remove(recordModel);
                        await _databaseContext.SaveChangesAsync();
                        
                        recordModel = JsonConvert.DeserializeObject<RecordModel>(previousRecordModelString);
                        _databaseContext.Records.Add(recordModel);
                        await _databaseContext.SaveChangesAsync();
                        throw new UpdateRecordException("The provided record and password information cannot update DDNS record.");
                    }
                }
            }
            catch (Exception exception)
            {
                throw new UpdateRecordException(exception.Message);
            }
        }
    }

    public async Task DeleteRecord(string domainName)
    {
        _logger.LogInformation($"Deleting record {domainName}");
        using (_databaseContext)
        {
            RecordModel recordModel = await _databaseContext.Records.FindAsync(domainName);

            try
            {
                _databaseContext.Records.Remove(recordModel);
                _logger.LogInformation($"Saving delete record {domainName}");
                await _databaseContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new DeleteRecordException("There is an error while trying to delete the record. Please see logs for more information.");
            }
        }
    }

    private async Task<GetRecordResponse> MapToGetRecordResponse(RecordModel? recordModel)
    {
        DecryptPasswordResponse decryptPasswordResponse = await _passwordRepository.DecryptPassword(recordModel.encryptedPassword);
        GetRecordResponse getRecordResponse = new GetRecordResponse();
        getRecordResponse.domain = recordModel.domain;
        getRecordResponse.password = decryptPasswordResponse.password;
        getRecordResponse.ip = recordModel.ip;
        return getRecordResponse;
    }

    public void Dispose()
    {
        _passwordRepository.Dispose();
        _databaseContext.Dispose();
        GC.SuppressFinalize(this);
    }
}