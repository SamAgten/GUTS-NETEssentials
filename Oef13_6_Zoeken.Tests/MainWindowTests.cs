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
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.WindowItems;

namespace Oef13_6_Zoeken.Tests
{
    [TestFixture]
    public class MainWindowTests
    {
        private Application application;
        private Window window;

        private ListBox seriesListBox = null;
        private Button searchButton = null;
        private TextBox searchTextbox = null;

        private Random random;

        [OneTimeSetUp]
        public void Setup()
        {
            var applicationDirectory = TestContext.CurrentContext.TestDirectory;

            string projectName = "Oef13_6_Zoeken";
            var applicationPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar + "Debug";
            applicationPath = Path.Combine(applicationPath, projectName + ".exe");
            TestContext.Progress.WriteLine("Using EXE: " + applicationPath);
            application = Application.Launch(applicationPath);
            window = application.GetWindow("Oef 13.6 Zoeken");      //This needs to be the title of the window, not the name of the class

            random = new Random();
            try
            {
                //Get all the UI items here
                seriesListBox = window.Get<ListBox>("seriesListBox");
                searchButton = window.Get<Button>(SearchCriteria.ByText("Zoek"));
                searchTextbox = window.Get<TextBox>("findTextBox");
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
        public void ShouldHaveSeriesListBox()
        {
            Assert.That(seriesListBox, Is.Not.Null);
        }

        [Test]
        public void ShouldHaveSearchButton()
        {
            Assert.That(searchButton, Is.Not.Null);
        }

        [Test]
        public void ShouldHaveSearchTextbox()
        {
            Assert.That(searchTextbox, Is.Not.Null);
        }

        [Test]
        public void ShouldShowMessageOnSuccesfulSearch()
        {
            int index = random.Next(seriesListBox.Items.Count);
            searchTextbox.Text = seriesListBox.Items[index].Text;

            //Press the button
            searchButton.Click();

            //Title of the messagebox should be: "Gevonden"!
            var messageBox = window.MessageBox("Gevonden");
            Assert.That(messageBox, Is.Not.Null);
            messageBox.Close();
        }

        [Test]
        public void ShouldShowMessageBoxOnFailedSearch()
        {
            int index = random.Next(seriesListBox.Items.Count);
            searchTextbox.Text = seriesListBox.Items[index].Text + RandomString(5);

            //Press the button
            searchButton.Click();

            //Title of the messagebox should be: "Gevonden"!
            var messageBox = window.MessageBox("Niet gevonden");
            Assert.That(messageBox, Is.Not.Null);
            messageBox.Close();
        }

        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            application.Close();
        }

    }
}
