using file2prompt.Core.Tools.FileSystemManagement;

namespace file2prompt.Core.UseCases.Common.Tools.ResponsePrinting;

internal class ReponsePrinter(IFileSystemManager fileSysManager) :
    IReponsePrinter
{
    public void Print(LlmResponse llmResponse)
    {
        // TODO: add validation input

        string text = llmResponse.Body;

        // PRE-PROCESSING
        if (llmResponse.ExtractionTags?.IsEmpty() != true)
        {
            text = llmResponse.Body.ExtractContent(
                startTag: llmResponse.ExtractionTags!.StartTag,
                endTag: llmResponse.ExtractionTags!.EndTag);
        }

        fileSysManager.WriteTextFile(
            filePath: llmResponse.TargetFilePath,
            content: text);
    }
}