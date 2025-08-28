using Microsoft.Extensions.Logging;

namespace file2prompt.Core.UseCases.Common;

internal abstract class BaseUseCase<TIn, TOut>(ILogger logger) :
    IUseCase<TIn, TOut>
    where TOut : Result, new()
{
    protected readonly ILogger _logger = logger;

    public virtual async Task<TOut> Execute(TIn @params)
    {
        _logger.LogInformation("Start processing usecase");

        try
        {
            return await MainMethod(@params);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while executing usecase");

            return new TOut()
            {
                IsSuccess = false,
                Exception = ex,
            };
        }
    }

    protected abstract Task<TOut> MainMethod(TIn options);
}