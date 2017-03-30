using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems;
using System.Windows.Media;

namespace Oef13_9_ComboBox.Tests
{
    [TestFixture]
    public class MainWindowTests
    {
        private Application application;
        private ComboBox comboBox;
        private Label label;

        [OneTimeSetUp]
        public void Setup()
        {
            var testDirectory = TestContext.CurrentContext.TestDirectory;

            string projectName = "Oef13_9_ComboBox";
            var applicationPath = Directory.GetParent(testDirectory).Parent.Parent.FullName;
            applicationPath = Path.Combine(applicationPath, projectName, @"bin\Debug", projectName + ".exe");
            //var applicationPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar + "Debug";
            //applicationPath = Path.Combine(applicationPath, projectName + ".exe");
            TestContext.Progress.WriteLine("Using EXE: " + applicationPath);
            application = Application.Launch(applicationPath);
            Window window = application.GetWindow("ComboBox");      //This needs to be the title of the window, not the name of the class

            try
            {
                //Get all the UI items here
                comboBox = window.Get<ComboBox>("colorComboBox");
                label = window.Get<Label>("colorLabel");
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
        public void ShouldHaveComboBox()
        {
            Assert.That(comboBox, Is.Not.Null);
        }

        [Test]
        public void ShouldHaveLabel()
        {
            Assert.That(label, Is.Not.Null);
        }

        [Test]
        public void ComboBoxShouldHave3Items()
        {
            Assert.That(comboBox.Items.Count, Is.EqualTo(3));

        }

        [Test]
        [TestCase("Red")]
        [TestCase("Blue")]
        [TestCase("Green")]
        public void ComboBoxShouldHaveItem(string item)
        {
            Assert.That(comboBox.Items, Has.Some.Matches((ListItem x) => x.Text == item));
        }

        //Cannot test the color of a label. 
        // White is basically a wrapper for the UI Automation framework which 
        // does not expose the background property of a UI Control Element.
        //[Test]
        //public void LabelShouldBeRedWhenSelectingRed()
        //{
        //    Assert.That(label.BorderColor, Is.EqualTo(Colors.Red));
        //}

        [OneTimeTearDown]
        public void TearDown()
        {
            application.Close();
        }

    }
}
