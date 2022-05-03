using Cake.Common;
using Cake.Common.Tools.DotNet;
using Cake.Core;
using Cake.Frosting;

public static class Program
{
    public static int Main(string[] args)
    {
        return new CakeHost()
            .UseContext<BuildContext>()
            .Run(args);
    }
}

public class BuildContext : FrostingContext
{
    private const string ConfigurationStr = "configuration";
    private const string DefaultConfiguration = "Release";
    public string MsBuildConfiguration { get; set; }
    public DotNetVerbosity Verbosity { get; set; }

    public BuildContext(ICakeContext context)
        : base(context)
    {
        MsBuildConfiguration = context.Argument(ConfigurationStr, DefaultConfiguration);
        Verbosity = context.Argument("verbosity", DotNetVerbosity.Minimal);
    }
}

[TaskName("Default")]
[IsDependentOn(typeof(CleanTask))]
[IsDependentOn(typeof(BuildTask))]
public class DefaultTask : FrostingTask
{
}