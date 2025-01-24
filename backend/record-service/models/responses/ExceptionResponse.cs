namespace record_service.models.responses;

public class ExceptionResponse
{
    public string message { get; set; }

    public ExceptionResponse(string message)
    {
        this.message = message;
    }
}