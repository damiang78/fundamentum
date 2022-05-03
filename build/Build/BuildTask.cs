using Cake.Common.Tools.DotNet;
using Cake.Frosting;

public sealed class BuildTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.DotNetBuild(ProjectHelpers.SolutionFile, new Cake.Common.Tools.DotNet.Build.DotNetBuildSettings
        {
            Configuration = context.MsBuildConfiguration,
            Verbosity = context.Verbosity,
        });
    }
}
