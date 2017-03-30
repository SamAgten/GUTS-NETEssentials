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

namespace Oef13_4_VervangItem.Tests
{
    [TestFixture]
    public class MainWindowTests
    {
        private Application application;

        private ListBox seriesListBox = null;
        private Button addButton = null;
        private TextBox itemTextBox = null;
        private TextBox replaceTextBox = null;

        private Button deleteButton = null;
        private Button clearButton = null;
        private Button replaceButton = null;
        private Random random;

        [OneTimeSetUp]
        public void Setup()
        {
            var testDirectory = TestContext.CurrentContext.TestDirectory;

            string projectName = "Oef13_4_VervangItem";
            //var applicationPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar + "Debug";
            //applicationPath = Path.Combine(applicationPath, projectName + ".exe");
            var applicationPath = Directory.GetParent(testDirectory).Parent.Parent.FullName;
            applicationPath = Path.Combine(applicationPath, projectName, @"bin\Debug", projectName + ".exe");

            TestContext.Progress.WriteLine("Using EXE: " + applicationPath);
            application = Application.Launch(applicationPath);
            Window window = application.GetWindow("Oef 13.4 Vervang");      //This needs to be the title of the window, not the name of the class

            random = new Random();
            try
            {
                //Get all the UI items here
                seriesListBox = window.Get<ListBox>("seriesListBox");
                addButton = window.Get<Button>(SearchCriteria.ByText("Voeg toe"));
                itemTextBox = window.Get<TextBox>("itemTextBox");
                deleteButton = window.Get<Button>(SearchCriteria.ByText("Verwijder"));
                clearButton = window.Get<Button>(SearchCriteria.ByText("Wis lijst"));
                replaceButton = window.Get<Button>(SearchCriteria.ByText("Vervang"));
                replaceTextBox = window.Get<TextBox>("replaceTextBox");
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
        public void ShouldHaveItemTextBox()
        {
            Assert.That(itemTextBox, Is.Not.Null);
        }

        [Test]
        public void ShouldHaveAddButton()
        {
            Assert.That(addButton, Is.Not.Null);
        }

        [Test]
        public void ShouldHaveDeleteButton()
        {
            Assert.That(deleteButton, Is.Not.Null);
        }

        [Test]
        public void ShouldHaveClearButton()
        {
            Assert.That(clearButton, Is.Not.Null);
        }

        [Test]
        public void ShouldHaveReplaceButton()
        {
            Assert.That(replaceButton, Is.Not.Null);
        }

        [Test]
        public void ShouldHaveReplaceTextBox()
        {
            Assert.That(replaceTextBox, Is.Not.Null);
        }

        [Test]
        public void ShouldAddSeriesAfterClickingButton()
        {
            //generate a random item to add
            string randomString = RandomString(5);
            int countBefore = seriesListBox.Items.Count;

            //Add the string
            itemTextBox.Text = randomString;
            addButton.Click();

            //Should still be ordered
            Assert.That(seriesListBox.Items.Count, Is.EqualTo(countBefore + 1));
        }

        [Test]
        public void ShouldClearListWhenClickingButton()
        {
            //Click the button
            clearButton.Click();

            Assert.That(seriesListBox.Items.Count, Is.EqualTo(0));
        }

        public void ShouldReplaceTextOfSelectedItemWhenClickingButton()
        {
            //Fill in the textbox
            string newText = RandomString(5);

            //Select an item in the list
            int index = random.Next(seriesListBox.Items.Count);
            seriesListBox.Select(index);

            //Click the button
            replaceButton.Click();

            string replacedText = seriesListBox.Items[index].Text;
            Assert.That(replacedText, Is.EqualTo(newText));
        }

        [Test]
        [Repeat(10)]
        public void ShouldDeleteSeriesWhenClickingButton()
        {
            int countBefore = seriesListBox.Items.Count;

            //This test assumes that there is always an item in the list which is not always the case
            if (countBefore == 0)
                return;

            //Click on a singer
            int index = random.Next(countBefore);
            seriesListBox.Select(index);

            //Click on the button
            deleteButton.Click();

            Assert.That(seriesListBox.Items.Count, Is.EqualTo(countBefore - 1));
        }

        [Test]
        public void ShouldNotDeleteSeriesWhenClickingButtonAndNothingIsSelected()
        {
            int countBefore = seriesListBox.Items.Count;

            //Click on the button
            deleteButton.Click();

            Assert.That(seriesListBox.Items.Count, Is.EqualTo(countBefore));
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
