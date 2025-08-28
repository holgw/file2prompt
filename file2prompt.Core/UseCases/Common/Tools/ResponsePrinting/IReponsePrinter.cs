namespace file2prompt.Core.UseCases.Common.Tools.ResponsePrinting;

internal interface IReponsePrinter
{
    void Print(LlmResponse llmResponse);
}