// Load necessary addins and tools
#tool nuget:?package=Cake.Tool&version=1.3.0
#addin nuget:?package=Cake.DotNetTool.Module&version=1.3.0
#addin nuget:?package=Cake.Coverlet
#tool nuget:?package=coverlet.console

var target = Argument("target", "TestReport");

Task("Clean")
    .Does(() =>
	{
		var settings = new DotNetCleanSettings
        {
            NoLogo = true,
            Verbosity = DotNetVerbosity.Quiet
        };
        DotNetClean(".", settings);
        CleanDirectory("./TestReports");
	});

Task("BuildSolution")
	.IsDependentOn("Clean")
    .Does(() => 
    {
	    var settings = new DotNetBuildSettings
        {
            Configuration = "Debug"
        };

        DotNetBuild(".", settings);
    });

Task("BuildTest")
    .Does(() => 
    {

        DotNetTest("./src/Hackathon.Fiap.Teste/Hackathon.Fiap.Teste.csproj", new DotNetTestSettings
            {
            Configuration = "Debug",
            ArgumentCustomization = args => args
                .Append("--collect:\"XPlat Code Coverage\"")
                .Append("--results-directory ./TestReports/")

            });

    });

Task("TestRun")
.IsDependentOn("BuildTest")
    .Does(() => 
    {
		var testSettings = new DotNetTestSettings
		{
			Configuration = "Debug",
            NoBuild = true,
			ArgumentCustomization = args => args.Append("--logger trx;LogFileName=result.trx --results-directory TestReports")
		};	

        var coverletSettings = new CoverletSettings {
            CollectCoverage = true,
            CoverletOutputFormat = CoverletOutputFormat.opencover,
            CoverletOutputDirectory = Directory(@".\TestReports\"),
            CoverletOutputName = $"coverage",
            ExcludeByAttribute = new List<string> { "ExcludeFromCodeCoverage" },
            Exclude = new List<string> {
                "[*]*Comando*",
                "[*]*RegularExpressions*"
            }
        };

    	DotNetTest("./src/Hackathon.Fiap.Teste/Hackathon.Fiap.Teste.csproj", testSettings, coverletSettings);
    }); 

Task("TestReport")
    .IsDependentOn("BuildSolution")
    .IsDependentOn("TestRun")
    .Does(() => 
    {
        StartProcess("reportgenerator", new ProcessSettings {
            Arguments = string.Join(" ", new[]
            {
                "-reports:\"./TestReports/**/coverage.cobertura.xml\"",
                "-targetdir:\"./TestReports/ReportGeneratorOutput\"",
                "-historydir:\"./TestReports/ReportsHistory\"",
                "-reporttypes:Html",
                "-classfilters:-System.Text.RegularExpressions.Generated*"
            })
        });

        var reportFilePath = "./TestReports/ReportGeneratorOutput/index.html";

        if (System.Environment.OSVersion.Platform == PlatformID.Unix)
        {
            StartProcess("open", reportFilePath);
        }
        else
        {
            StartProcess("explorer", reportFilePath);
        }
    });



RunTarget(target);



