namespace Applitools.Selenium.Tests
{
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Remote;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class TestSetup
    {
        protected Eyes eyes_;
        protected ILogHandler logHandler_;
        protected IWebDriver driver_;
        protected IWebDriver chromeDriver_;
        private string testSuitName_;
        protected static BatchInfo batchInfo_;

        public TestSetup(string testSuitName)
        {
            testSuitName_ = testSuitName;
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Initialize the eyes SDK and set your private API key.
            eyes_ = new Eyes();
            //eyes_.ServerUrl = "https://localhost.applitools.com";
            eyes_.ApiKey = Environment.GetEnvironmentVariable("APPLITOOLS_API_KEY");

            logHandler_ = new FileLogHandler(@"c:\temp\logs\TestElement.log", true, true);
            eyes_.SetLogHandler(logHandler_);
            eyes_.ForceFullPageScreenshot = true;
            eyes_.StitchMode = StitchModes.CSS;
            eyes_.HideScrollbars = true;
            //eyes_.StitchMode = StitchModes.Scroll;
            //eyes_.MatchLevel = MatchLevel.Layout;
            //eyes_.DebugScreenshotProvider = new FileDebugScreenshotProvider() { Path = @"c:\temp", Prefix = "DotNetElementTest" };

            eyes_.Batch = batchInfo_ = new BatchInfo(testSuitName_);
        }

        public void SetUp([CallerMemberName] string testName = null, params string[] arguments)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("disable-infobars");
            options.AddArguments(arguments);

            chromeDriver_ = new ChromeDriver(options);

            driver_ = eyes_.Open(chromeDriver_, testSuitName_, testName, new Size(/*1024, 635*/ 800, 599));

            driver_.Navigate().GoToUrl("http://applitools.github.io/demo/TestPages/FramesTestPage/");

            eyes_.Log(testName + ": " + batchInfo_.Name);
        }

        [TearDown]
        public void Teardown()
        {
            TestResults results = eyes_.Close(false);
            eyes_.Log("Mismatches: " + results.Mismatches);
            driver_.Quit();
        }
    }
}
