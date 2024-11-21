using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters;

public class ApiExcepitionFilter : IExceptionFilter
{

    private readonly ILogger<ApiExcepitionFilter> _logger;

    public ApiExcepitionFilter(ILogger<ApiExcepitionFilter> logger)
    {
        _logger = logger;
    }


    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "Ocorreu uma exceção não tratada: Status Code 500");

        context.Result = new ObjectResult("Ocorreum um problema ao tratar a sua solicitação: Status Code 500")
        {
            StatusCode =StatusCodes.Status500InternalServerError,
        };
    }
}
