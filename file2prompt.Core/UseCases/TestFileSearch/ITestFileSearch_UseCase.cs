using file2prompt.Core.External.OutputWriter;
using file2prompt.Core.UseCases.Common;

namespace file2prompt.Core.UseCases.TestFileSearch;

internal interface ITestFileSearch_UseCase :
    IUseCase<TestFileSearch_Params, TestFileSearch_Result>
{
    void Setup(IOutputWriter outputWriter);
}