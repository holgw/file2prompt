namespace file2prompt.Core.UseCases.Common;

internal interface IUseCase<TIn, TOut>
{
    Task<TOut> Execute(TIn options);
}