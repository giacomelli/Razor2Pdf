#tool nuget:?package=MSBuild.SonarQube.Runner.Tool&version=4.6.0
#tool nuget:?package=GitVersion.CommandLine&version=4.0.0
#addin nuget:?package=Cake.Sonar&version=1.1.22
#addin nuget:?package=Cake.Git&version=0.21.0

var target = Argument("target", "Default");
var solutionDir = "src";
var sonarLogin = "1c3d2042c0c7e8816bb97e95b137e44a71559cc6";
var branch = EnvironmentVariable("APPVEYOR_REPO_BRANCH") ?? GitBranchCurrent(".").FriendlyName;
var configuration = "Release";
string version = null;

Task("Build")
    .Does(() =>
{
    var settings = new DotNetCoreBuildSettings
    {
        Configuration = configuration,
    };

    DotNetCoreBuild(solutionDir, settings);
});

Task("Version")    
    .Does(() =>
    {
        version = (GitVersion(new GitVersionSettings {
            UpdateAssemblyInfo = false
        })).NuGetVersionV2;
    });

Task("Test")
    .Does(() =>
{
    var settings = new DotNetCoreTestSettings
    {
        ArgumentCustomization = args => {
            return args.Append("/p:CollectCoverage=true")
                       .Append("/p:CoverletOutputFormat=opencover");
        }
    };

    DotNetCoreTest(solutionDir, settings);
});

Task("Package")
    .Does(() =>
{
    var settings = new DotNetCorePackSettings
    {
        Configuration = configuration,
        OutputDirectory  = "./artifacts",
        ArgumentCustomization = args => 
        {
            return 
                args
                    .Append("/p:NuspecFile=Razor2Pdf.nuspec")
                    .Append("/p:Version=" + version);
        }
    };

    DotNetCorePack("src/Razor2Pdf", settings);
});

Task("SonarBegin")
    .Does(() => 
{
    SonarBegin(new SonarBeginSettings {
        Key = "Razor2Pdf",
        Branch = branch,
        Organization = "giacomelli-github",
        Url = "https://sonarcloud.io",
        Exclusions = "**/Samples/**/*.cs,**/Razor2Pdf.Tests/*.cs",
        OpenCoverReportsPath = "**/*.opencover.xml",
        Login = sonarLogin   
     });
});

Task("SonarEnd")
  .Does(() => {
     SonarEnd(new SonarEndSettings{
        Login = sonarLogin
     });
  });

Task("Default")
   // .IsDependentOn("SonarBegin")
    .IsDependentOn("Build")
    .IsDependentOn("Version")
    //.IsDependentOn("Test")
    .IsDependentOn("Package")
    //.IsDependentOn("SonarEnd")
	.Does(()=> { 
});

RunTarget(target);