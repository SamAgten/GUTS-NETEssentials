using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.WindowItems;

namespace Oef13_8_Personen.Tests
{
    [TestFixture]
    public class MainWindowTests
    {
        private Application application;
        private TestStack.White.UIItems.ListBoxItems.ListBox personListBox;
        private Button detailButton;

        #region DUMMY DATA
        //private List<Persoon> people = new List<Persoon>()
        //    {
        //        new Persoon()
        //        {
        //            Naam="Hermans",
        //            Voornaam="Kris",
        //            Adres="Kerkhof 24, 3560 Houthalen",
        //            GeboorteDatum = new DateTime(1975, 5, 15),
        //            Telefoon="1234567",
        //            Geslacht=GeslachtEnum.M
        //        },
        //        new Persoon()
        //        {
        //            Naam="Stasik",
        //            Voornaam="Marijke",
        //            Adres="Kerkhof 24, 3560 Houthalen",
        //            GeboorteDatum = new DateTime(1975, 2, 14),
        //            Telefoon="12345667",
        //            Geslacht=GeslachtEnum.V
        //        },
        //        new Persoon()
        //        {
        //            Naam="Hermans",
        //            Voornaam="Ella",
        //            Adres="Kerkhof 24, 3560 Houthalen",
        //            GeboorteDatum=new DateTime(2003, 12, 25),
        //            Telefoon="1234567",
        //            Geslacht=GeslachtEnum.V
        //        },

        //        new Persoon()
        //        {
        //            Naam="Hermans",
        //            Voornaam="Gilles",
        //            Adres="Kerkhof 24, 3560 Houthalen",
        //            GeboorteDatum=new DateTime(2008, 9, 29),
        //            Telefoon="1234567",
        //            Geslacht=GeslachtEnum.M
        //        }
        //    };
        #endregion

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var applicationDirectory = TestContext.CurrentContext.TestDirectory;

            string projectName = "Oef13_8_Personen";
            var applicationPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar + "Debug";
            applicationPath = Path.Combine(applicationPath, projectName + ".exe");
            TestContext.Progress.WriteLine("Using EXE: " + applicationPath);
            application = Application.Launch(applicationPath);
            Window window = application.GetWindow("Oef 13.8 Personen");      //This needs to be the title of the window, not the name of the class
            try
            {
                //Get all the UI items here
                personListBox = window.Get<TestStack.White.UIItems.ListBoxItems.ListBox>("persoonListBox");
                detailButton = window.Get<Button>(SearchCriteria.ByText("Details"));
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
        public void ShouldHavePersonListBox()
        {
            Assert.That(personListBox, Is.Not.Null);
        }

        [Test]
        public void ShouldHaveDetailButton()
        {
            Assert.That(detailButton, Is.Not.Null);
        }

        [Test]
        public void ShouldOpenDetailWindowWhenClickingButton()
        {
            //Select the first person in the list
            personListBox.Select(0);

            //Click the button
            detailButton.Click();
            Window detailWindow = application.GetWindow("Details");

            Assert.That(detailWindow, Is.Not.Null);

            detailWindow.Close();
        }

        [Test]
        [TestCase("naamTextBox")]
        [TestCase("voornaamTextBox")]
        [TestCase("adresTextBox")]
        [TestCase("telTextBox")]
        [TestCase("geborenTextBox")]
        public void DetailWindowShouldHaveTextBox(string textboxName)
        {
            //Select the first person in the list
            personListBox.Select(0);

            //Click the button
            detailButton.Click();
            Window detailWindow = application.GetWindow("Details");

            //Get the ui element
            TextBox textBox = detailWindow.Get<TextBox>(textboxName);
            Assert.That(textBox, Is.Not.Null);

            //Should contain some stuff (we cannot actually check if this matches your dummy data...)
            Assert.That(textBox.Text, Is.Not.Empty);

            detailWindow.Close();
        }

        [Test]
        public void DetailWindowShouldHaveRadioButtonsForSex()
        {
            //Select the first person in the list
            personListBox.Select(0);

            //Click the button
            detailButton.Click();
            Window detailWindow = application.GetWindow("Details");

            //Get the ui element
            RadioButton manButton = detailWindow.Get<RadioButton>("manRadioButton");
            RadioButton womanButton = detailWindow.Get<RadioButton>("vrouwRadioButton");
            Assert.That(manButton, Is.Not.Null);
            Assert.That(womanButton, Is.Not.Null);

            //one of the two should be checked
            Assert.That(manButton.IsSelected || womanButton.IsSelected);
            
            detailWindow.Close();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            application.Close();
        }
    }
}
