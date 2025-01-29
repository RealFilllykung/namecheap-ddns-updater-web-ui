using System.Net;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Npgsql;
using record_service.infrastructures.abstracts;
using record_service.models.exceptions;
using record_service.models.responses;

namespace record_service.services.middlewares;

public class ExceptionHandlerMiddleware : AbstractExceptionHandlerMiddleware
{
    public ExceptionHandlerMiddleware(ILogger<AbstractExceptionHandlerMiddleware> logger, RequestDelegate next) : base(logger, next)
    {
    }

    public override (HttpStatusCode code, string message) GetResponse(Exception exception)
    {
        HttpStatusCode code = (HttpStatusCode)0;
        switch (exception)
        {
            case CreateRecordException:
                code = HttpStatusCode.BadRequest;
                break;
            case GetRecordException:
                code = HttpStatusCode.BadRequest;
                break;
            case UpdateRecordException:
                code = HttpStatusCode.BadRequest;
                break;
            case DeleteRecordException:
                code = HttpStatusCode.BadRequest;
                break;
            default:
                code = HttpStatusCode.InternalServerError;
                break;
        }
        return (code, JsonConvert.SerializeObject(new ExceptionResponse(exception.Message)));
    }
}