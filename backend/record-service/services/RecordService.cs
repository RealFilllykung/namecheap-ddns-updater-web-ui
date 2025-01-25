﻿using Microsoft.EntityFrameworkCore;
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
    private readonly IIPService _ipService;
    private readonly ILogger<RecordService> _logger;
    private readonly IPasswordRepository _passwordRepository;
    private readonly DatabaseContext _databaseContext;

    public RecordService(IIPService ipService, ILogger<RecordService> logger, IPasswordRepository passwordRepository, DatabaseContext databaseContext)
    {
        _ipService = ipService;
        _logger = logger;
        _passwordRepository = passwordRepository;
        _databaseContext = databaseContext;
    }

    public async Task CreateRecord(CreateRecordRequest request)
    {
        string ip;
        EncryptPasswordResponse? encryptPasswordResponse;
        using (_ipService)
        {
            _logger.LogInformation($"Getting current IP for {request.domain}");
            ip = await _ipService.GetCurrentPublicIP();
            _logger.LogInformation($"Obtained public IP of {ip}");
        }

        using (_passwordRepository)
        {
            _logger.LogInformation($"Requesting password encryption");
            encryptPasswordResponse = await _passwordRepository.EncryptPassword(request.password);
            _logger.LogInformation($"Done encrypting password: {encryptPasswordResponse?.encryptedPassword}");
        }

        await using (_databaseContext)
        {
            _logger.LogInformation($"Recording the record into database.");
            RecordModel? recordModel = new RecordModel
            {
                encryptedPassword = encryptPasswordResponse?.encryptedPassword,
                domain = request.domain,
                ip = ip
            };
            try
            {
                _databaseContext.Records.Add(recordModel);
                await _databaseContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new CreateRecordException("There is an error while trying to create the record. Please see logs for more information.");
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
            
            EncryptPasswordResponse? encryptPasswordResponse = await _passwordRepository.EncryptPassword(record.password);
            recordModel.encryptedPassword = encryptPasswordResponse.encryptedPassword;
            recordModel.ip = await _ipService.GetCurrentPublicIP();
            try
            {
                _databaseContext.Records.Update(recordModel);
                _logger.LogInformation($"Saving new {recordModel.domain}");
                await _databaseContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new UpdateRecordException("There is an error while trying to update the record. Please see logs for more information.");
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
        _ipService.Dispose();
        _passwordRepository.Dispose();
        _databaseContext.Dispose();
        GC.SuppressFinalize(this);
    }
}