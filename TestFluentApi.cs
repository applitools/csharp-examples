namespace Applitools.Selenium.Tests
{
    using NUnit.Framework;
    using OpenQA.Selenium;
    using System;
    using System.Drawing;

    [TestFixture]
    public class TestFluentApi : TestSetup
    {
        public TestFluentApi() : base("Eyes Selenium SDK - Fluent API - .Net") { }

        [Test]
        public void TestCheckWindowWithIgnoreRegion_Fluent()
        {
            SetUp();
            eyes_.Check("Fluent - Window with Ignore region", Target.Window()
                                                           .Fully()
                                                           .Timeout(TimeSpan.FromSeconds(5))
                                                           .Ignore(new Rectangle(50, 50, 100, 100)));
        }

        [Test]
        public void TestCheckRegionWithIgnoreRegion_Fluent()
        {
            SetUp();
            eyes_.Check("Fluent - Region with Ignore region", Target.Region(By.Id("overflowing-div"))
                                                           .Ignore(new Rectangle(50, 50, 100, 100)));
        }

        [Test]
        public void TestCheckFrame_Fully_Fluent()
        {
            SetUp();
            eyes_.Check("Fluent - Full Frame", Target.Frame("frame1").Fully());
        }

        [Test, Order(2)]
        public void TestCheckFrame_Fluent()
        {
            SetUp();
            eyes_.Check("Fluent - Frame", Target.Frame("frame1"));
        }

        [Test, Order(1)]
        public void TestCheckFrameInFrame_Fully_Fluent()
        {
            SetUp();
            eyes_.Check("Fluent - Full Frame in Frame", Target.Frame("frame1")
                                                     .Frame("frame1-1")
                                                     .Fully());
        }

        [Ignore("Code doesn't work yet")]
        public void TestCheckFrameInFrame_Fluent()
        {
            SetUp();
            eyes_.Check("Fluent - Frame in Frame", Target.Frame("frame1")
                                                .Frame("frame1-1"));
        }


        [Test]
        public void TestCheckRegionInFrame_Fluent()
        {
            SetUp();
            eyes_.Check("Fluent - Region in Frame", Target.Frame("frame1")
                                                 .Region(By.Id("inner-frame-div"))
                                                 .Fully()
                                                 .Exact());
        }

        [Test]
        public void TestCheckRegionInFrameInFrame_Fluent()
        {
            SetUp();
            eyes_.Check("Fluent - Region in Frame in Frame", Target.Frame("frame1")
                                                          .Frame("frame1-1")
                                                          .Region(By.TagName("img"))
                                                          .Fully());
        }

        [Test]
        public void TestCheckRegionInFrame2_Fluent()
        {
            SetUp();
            eyes_.Check("Fluent - Inner frame div 1", Target.Frame("frame1")
                                                   .Fully()
                                                   .Timeout(TimeSpan.FromSeconds(5))
                                                   .Region(By.Id("inner-frame-div"))
                                                   .Ignore(new Rectangle(50, 50, 100, 100))
                                                   );

            eyes_.Check("Fluent - Inner frame div 2", Target.Frame("frame1")
                                                   .Region(By.Id("inner-frame-div"))
                                                   .Fully()
                                                   .Ignore(new Rectangle(50, 50, 100, 100))
                                                   .Ignore(new Rectangle(70, 170, 90, 90)));

            eyes_.Check("Fluent - Inner frame div 3", Target.Frame("frame1")
                                                   .Region(By.Id("inner-frame-div"))
                                                   .Fully()
                                                   .Timeout(TimeSpan.FromSeconds(5)));

            eyes_.Check("Fluent - Inner frame div 4", Target.Frame("frame1")
                                                   .Region(By.Id("inner-frame-div"))
                                                   .Fully());

            eyes_.Check("Fluent - Full frame with floating region", Target.Frame("frame1")
                                                                 .Fully()
                                                                 .Layout()
                                                                 .Floating(25, new Rectangle(200, 200, 150, 150)));
        }

        [Test]
        public void TestCheckFrameInFrame_Fully_Fluent2()
        {
            SetUp();
            eyes_.Check("Fluent - Window with Ignore region 2", Target.Window()
                    .Fully());

            eyes_.Check("Fluent - Full Frame in Frame 2", Target.Frame("frame1")
                    .Fully()
                    .Frame("frame1-1"));
        }

        [Test]
        public void TestCheckWindowWithIgnoreBySelector_Fluent()
        {
            SetUp();

            eyes_.Check("Fluent - Window with ignore region by selector", Target.Window()
                    .Ignore(By.Id("overflowing-div")));
        }

        [Test]
        public void TestCheckWindowWithFloatingBySelector_Fluent()
        {
            SetUp();

            eyes_.Check("Fluent - Window with floating region by selector", Target.Window()
                    .Floating(By.Id("overflowing-div"), 3, 3, 20, 30));
        }

        [Test]
        public void TestCheckRegionByCoordinates_Fluent()
        {
            SetUp();

            eyes_.Check("Fluent - Region by coordinates", Target.Region(new Rectangle(50, 70, 90, 110)));
        }

        [Test]
        public void TestCheckOverflowingRegionByCoordinates_Fluent()
        {
            SetUp();

            eyes_.Check("Fluent - Region by coordinates", Target.Region(new Rectangle(50, 110, 90, 550)));
        }

        [Test]
        public void TestCheckElementFully_Fluent()
        {
            SetUp();

            IWebElement element = chromeDriver_.FindElement(By.Id("overflowing-div-image"));
            eyes_.Check("Fluent - Region by element - fully", Target.Region(element).Fully());
        }

        [Test]
        public void TestCheckElement_Fluent()
        {
            SetUp();

            IWebElement element = chromeDriver_.FindElement(By.Id("overflowing-div-image"));
            eyes_.Check("Fluent - Region by element", Target.Region(element));
        }
    }
}
