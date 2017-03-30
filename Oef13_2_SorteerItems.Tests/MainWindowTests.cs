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

namespace Oef13_2_SorteerItems.Tests
{
    [SingleThreaded]
    [TestFixture]
    public class MainWindowTests
    {
        private Application application;
        private ListBox seriesListBox = null;
        private Button addButton = null;
        private TextBox itemTextBox = null;

        private Button deleteButton = null;
        private Random random;

        [OneTimeSetUp]
        public void Setup()
        {
            var testDirectory = TestContext.CurrentContext.TestDirectory;

            string projectName = "Oef13_2_SorteerItems";
            var applicationPath = Directory.GetParent(testDirectory).Parent.Parent.FullName;
            applicationPath = Path.Combine(applicationPath, projectName, @"bin\Debug", projectName + ".exe");
            //var applicationPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar + "Debug";
            //applicationPath = Path.Combine(applicationPath, projectName + ".exe");
            TestContext.Progress.WriteLine("Using EXE: " + applicationPath);
            application = Application.Launch(applicationPath);
            Window window = application.GetWindow("Oef 13.2 Sorteren");      //This needs to be the title of the window, not the name of the class
            random = new Random();

            try
            {
                //Get all the UI items here
                seriesListBox = window.Get<ListBox>("seriesListBox");
                addButton = window.Get<Button>(SearchCriteria.ByText("Voeg toe"));
                itemTextBox = window.Get<TextBox>("itemTextBox");
                deleteButton = window.Get<Button>(SearchCriteria.ByText("Verwijder"));
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
        public void ListShouldStartSorted()
        {
            Assert.That(ListIsAlphabetic(), Is.True);
        }
        
        
        [Test]
        [Repeat(3)]
        public void ShouldKeepAlphabetticalOrderAfterAddingItem()
        {
            //generate a random item to add
            string randomString = RandomString(5);
            int countBefore = seriesListBox.Items.Count;

            //Add the string
            itemTextBox.Text = randomString;
            addButton.Click();

            //Should still be ordered
            Assert.That(ListIsAlphabetic(), Is.True);
        }


        [Test]
        [Repeat(30)]
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

            //...and still be ordered
            Assert.That(ListIsAlphabetic(), Is.True);
        }

        [Test]
        [Repeat(3)]
        public void ShouldNotDeleteSeriesWhenClickingButtonAndNothingIsSelected()
        {
            int countBefore = seriesListBox.Items.Count;

            //Click on the button
            deleteButton.Click();

            Assert.That(seriesListBox.Items.Count, Is.EqualTo(countBefore));
        }

        private bool ListIsAlphabetic()
        {
            if (seriesListBox.Items.Count <= 1) return true;

            //Get the first item
            ListItem previousItem = seriesListBox.Items[0];
            for (int i = 1; i < seriesListBox.Items.Count; i++)
            {
                ListItem currentItem = seriesListBox.Items[i];
                if (String.Compare(previousItem.Text, currentItem.Text) > 0)
                    return false;
                previousItem = currentItem;
            }

            return true;
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
