using Cake.Common.Tools.DotNet;
using Cake.Frosting;

public sealed class CleanTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.DotNetClean(ProjectHelpers.SolutionFile, new Cake.Common.Tools.DotNet.Clean.DotNetCleanSettings
        {
            Verbosity = context.Verbosity
        });
    }
}
