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

namespace Oef13_5_ToevoegenVerwijderen.Tests
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

        private TextBox positionTextBox = null;

        [SetUp]
        public void Setup()
        {
            var testDirectory = TestContext.CurrentContext.TestDirectory;

            string projectName = "Oef13_5_ToevoegenVerwijderen";

            var applicationPath = Directory.GetParent(testDirectory).Parent.Parent.FullName;
            applicationPath = Path.Combine(applicationPath, projectName, @"bin\Debug", projectName + ".exe");
            //var applicationPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar + "Debug";
            //applicationPath = Path.Combine(applicationPath, projectName + ".exe");
            TestContext.Progress.WriteLine("Using EXE: " + applicationPath);
            application = Application.Launch(applicationPath);
            Window window = application.GetWindow("Oef 13.5 Toevoegen / Verwijderen");      //This needs to be the title of the window, not the name of the class


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
                positionTextBox = window.Get<TextBox>("posTextBox");
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
        public void ShouldHavePositionTextBox()
        {
            Assert.That(positionTextBox, Is.Not.Null);
        }

        [Test]
        public void ShouldInsertSeriesAfterClickingButton()
        {
            //generate a random item to add
            string randomString = RandomString(5);
            itemTextBox.Text = randomString;

            //Choose an index
            int index = random.Next(seriesListBox.Items.Count);
            positionTextBox.Text = "" + index;

            //Add the string
            addButton.Click();

            //Find the item that we added
            int newIndex = 0;
            foreach(ListItem item in seriesListBox.Items)
            {
                if (item.Text == randomString)
                    break;
                newIndex++;
            }

            
            Assert.That(newIndex, Is.EqualTo(index));
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
        public void ShouldDeleteSeriesWhenClickingButton()
        {
            int index = random.Next(seriesListBox.Items.Count);
            positionTextBox.Text = "" + index;
            ListItem itemToDelete = seriesListBox.Items[index];

            //Click on the button
            deleteButton.Click();

            Assert.That(seriesListBox.Items, Has.None.EqualTo(itemToDelete));
        }


        [Test]
        public void ShouldClearListWhenClickingButton()
        {
            //Click the button
            clearButton.Click();

            Assert.That(seriesListBox.Items.Count, Is.EqualTo(0));
        }

        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [TearDown]
        public void TearDown()
        {
            application.Close();
        }

    }
}
