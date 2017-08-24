namespace Applitools.Selenium.Tests
{
    using NUnit.Framework;
    using OpenQA.Selenium;

    [TestFixture]
    public class TestClassicApi : TestSetup
    {
        public TestClassicApi() : base("Eyes Selenium SDK - Classic API - .Net") { }

        [Test, Order(0)]
        public void TestCheckWindow()
        {
            SetUp();
            eyes_.CheckWindow("Window");
        }

        [Test]
        public void TestCheckRegion()
        {
            SetUp();
            eyes_.CheckRegion(By.Id("overflowing-div"), "Region", true);
        }

        [Test]
        public void TestCheckFrame()
        {
            SetUp();
            eyes_.CheckFrame("frame1", "frame1");
        }

        [Test]
        public void TestCheckRegionInFrame()
        {
            SetUp();
            eyes_.CheckRegionInFrame("frame1", By.Id("inner-frame-div"), "Inner frame div", true);
        }

    }
}