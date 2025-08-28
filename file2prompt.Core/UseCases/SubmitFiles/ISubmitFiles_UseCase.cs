using file2prompt.Core.External.OutputWriter;
using file2prompt.Core.UseCases.Common;

namespace file2prompt.Core.UseCases.SubmitFiles;

internal interface ISubmitFiles_UseCase :
    IUseCase<SubmitFiles_Params, SubmitFiles_Result>
{
    void Setup(IOutputWriter outputWriter);
}
