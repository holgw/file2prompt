using file2prompt.Core.External.OutputWriter;
using file2prompt.Core.UseCases.Common;

namespace file2prompt.Core.UseCases.TestConnection;

internal interface ITestConnection_UseCase :
    IUseCase<TestConnection_Params, TestConnection_Result>
{
    void Setup(IOutputWriter outputWriter);
}
