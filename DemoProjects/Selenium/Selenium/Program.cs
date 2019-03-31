using Applitools.VisualGrid;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Drawing;
using ScreenOrientation = Applitools.VisualGrid.ScreenOrientation;

namespace Applitools.Selenium.Demo
{
    public class MinimalSeleniumDemo
    {
        public static void Main()
        {
            MinimalSeleniumDemo program = new MinimalSeleniumDemo();

            program.RunSeleniumDemo();

            program.RunVisualGridDemo();
        }

        private void RunSeleniumDemo()
        {
            // Create Eyes object with the runner, meaning it'll be a Visual Grid eyes.
            Eyes eyes = new Eyes();

            // Create a file logger with default file ('eyes.log', verbose, in current directory).
            FileLogHandler logHandler = new FileLogHandler();

            // Set the log handler.
            eyes.SetLogHandler(logHandler);

            // Create Selenium Configuration.
            Configuration sconf = new Configuration();

            sconf.AppName = "Selenium WebDriver Demo App";
            sconf.TestName = "Selenium WebDriver Demo Test";
            sconf.ViewportSize = new Size(640, 480);

            RunTest(eyes, sconf);
        }

        private void RunVisualGridDemo()
        {
            // Create a runner with concurrency of 10
            VisualGridRunner runner = new VisualGridRunner(10);

            // Create a file logger with default file ('eyes.log', verbose, in current directory).
            FileLogHandler logHandler = new FileLogHandler();

            // Set the log handler.
            runner.SetLogHandler(logHandler);

            // Create Eyes object with the runner, meaning it'll be a Visual Grid eyes.
            Eyes eyes = new Eyes(runner);

            // Create Selenium Configuration.
            Configuration sconf = new Configuration();

            // Set app name
            sconf.AppName = "Visual Grid Demo App";

            // Set test name
            sconf.TestName = "Visual Grid Demo Test";

            // Add browsers
            sconf.AddBrowser(800, 600, Configuration.BrowserType.CHROME);
            sconf.AddBrowser(700, 500, Configuration.BrowserType.FIREFOX);
            sconf.AddBrowser(1200, 800, Configuration.BrowserType.IE10);
            sconf.AddBrowser(1200, 800, Configuration.BrowserType.IE11);
            sconf.AddBrowser(1600, 1200, Configuration.BrowserType.EDGE);

            // Add iPhone 4 device emulation
            EmulationInfo iphone4 = new EmulationInfo(EmulationInfo.DeviceNameEnum.iPhone_4, ScreenOrientation.Portrait);
            sconf.AddDeviceEmulation(iphone4);

            // Add custom mobile device emulation
            EmulationDevice customMobile = new EmulationDevice(width: 1024, height: 768, deviceScaleFactor: 2);
            sconf.AddDeviceEmulation(customMobile);

            sconf.AddDeviceEmulation(EmulationInfo.DeviceNameEnum.iPhone_5SE, ScreenOrientation.Landscape);
            sconf.AddDeviceEmulation(EmulationInfo.DeviceNameEnum.Galaxy_S5);

            sconf.AddDeviceEmulation(800, 640);

            RunTest(eyes, sconf);

            TestResultSummary allTestResults = runner.GetAllTestResults();
        }

        private static void RunTest(Eyes eyes, Configuration sconf)
        {
            // Create a new webdriver
            IWebDriver webDriver = new ChromeDriver();

            // Navigate to the url we want to test
            webDriver.Url = "https://applitools.com/helloworld";

            // Set the configuration object to eyes
            eyes.Configuration = sconf;

            // Call Open on eyes to initialize a test session
            eyes.Open(webDriver);

            // Add 2 checks
            eyes.Check(Target.Window().WithName("Step 1 - Viewport").Ignore(By.CssSelector(".primary")));
            eyes.Check(Target.Window().Fully().WithName("Step 1 - Full Page").Floating(By.CssSelector(".primary"), 10, 20, 30, 40).Floating(By.TagName("button"), 1, 2, 3, 4));

            webDriver.FindElement(By.TagName("button")).Click();

            // Add 2 checks
            eyes.Check(Target.Window().WithName("Step 2 - Viewport"));
            eyes.Check(Target.Window().Fully().WithName("Step 2 - Full Page"));

            // Close the browser
            webDriver.Quit();

            // Close eyes and collect results
            TestResults results = eyes.Close();
        }

    }
}
