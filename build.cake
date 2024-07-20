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

        DotNetTest("./TC - Teste/TC - Teste.csproj", new DotNetTestSettings
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
			ExcludeByAttribute = new List<string> { "*.ExcludeFromCodeCoverage*" }
		};

    	DotNetTest("./TC - Teste/TC - Teste.csproj", testSettings, coverletSettings);
    }); 

Task("TestReport")
    .IsDependentOn("BuildSolution")
    .IsDependentOn("TestRun")
    .Does(() => 
    {

            var reportGeneratorSettings = new ReportGeneratorSettings()
            {
                HistoryDirectory = new DirectoryPath("./TestReports/ReportsHistory")
            };

            ReportGenerator(new FilePath("./TestReports/**/coverage.cobertura.xml"), new DirectoryPath("./TestReports/ReportGeneratorOutput"), reportGeneratorSettings);
			var reportFilePath = "./TestReports/ReportGeneratorOutput//index.html";
         
            if (System.Environment.OSVersion.Platform == PlatformID.Unix)
            {
                // Para macOS ou Linux
                StartProcess("open", reportFilePath);
            }
            else
            {
                // Para Windows
                StartProcess("explorer", reportFilePath);
            }
                
            });



RunTarget(target);



