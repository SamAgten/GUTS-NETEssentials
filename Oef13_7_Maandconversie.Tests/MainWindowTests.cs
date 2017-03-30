using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;

namespace Oef13_7_Maandconversie.Tests
{
    [TestFixture]
    public class MainWindowTests
    {
        private Application application;

        private TextBox monthIndexTextBox = null;
        private Button conversionButton = null;
        private TextBox resultTextBox = null;

        [OneTimeSetUp]
        public void Setup()
        {
            var testDirectory = TestContext.CurrentContext.TestDirectory;

            string projectName = "Oef13_7_Maandconversie";
          //  var applicationPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar + "Debug";
          //  applicationPath = Path.Combine(applicationPath, projectName + ".exe");

            var applicationPath = Directory.GetParent(testDirectory).Parent.Parent.FullName;
            applicationPath = Path.Combine(applicationPath, projectName, @"bin\Debug", projectName + ".exe");
            TestContext.Progress.WriteLine("Using EXE: " + applicationPath);
            application = Application.Launch(applicationPath);
            Window window = application.GetWindow("Oef 13.7 Maandconversie");      //This needs to be the title of the window, not the name of the class

            try
            {
                //Get all the UI items here
                monthIndexTextBox = window.Get<TextBox>("monthNumberTextBox");
                conversionButton = window.Get<Button>(SearchCriteria.ByText("is"));
                resultTextBox = window.Get<TextBox>("monthNameTextBox");
            }
            catch (Exception)
            {
                //The reason we are ignoring errors while finding ui items is because
                //we don't want every test to fail when an items isn't found.
                //Ignoring the exceptions still runs the tests. This makes it easier to pin down
                //which ui element was not found.
            }
        }

        [Test]
        public void ShouldHaveResultTextBox()
        {
            Assert.That(resultTextBox, Is.Not.Null);
        }

        [Test]
        public void ShouldHaveConversionButton()
        {
            Assert.That(conversionButton, Is.Not.Null);
        }


        [Test]
        public void ShouldHaveMonthNumberTextBox()
        {
            Assert.That(monthIndexTextBox, Is.Not.Null);
        }

        [Test]
        [TestCase(1, "January")]
        [TestCase(2, "February")]
        [TestCase(3, "March")]
        [TestCase(4, "April")]
        [TestCase(5, "May")]
        [TestCase(6, "June")]
        [TestCase(7, "July")]
        [TestCase(8, "August")]
        [TestCase(9, "September")]
        [TestCase(10, "October")]
        [TestCase(11, "November")]
        [TestCase(12, "December")]
        public void ShouldDoSomeProperConversion(int index, string month)
        {
            //input the number
            monthIndexTextBox.Text = "" + index;

            //press the button
            conversionButton.Click();

            //Check the result
            Assert.That(resultTextBox.Text, Is.EqualTo(month));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            application.Close();
        }
    }
}
