﻿using GUTSNet;
using NUnit.Framework;
using System;
using System.IO;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.WindowItems;

namespace Oef13_1_VerwijderItems.Tests
{
    [TestFixture, GUTSSuite]
    public class MainWindowTests
    {
        private ListBox singersListBox = null;
        private Application application = null;
        private Button deleteButton = null;
        private ListBox seriesListBox = null;
        private Random rnd;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var testDirectory = TestContext.CurrentContext.TestDirectory;

            string projectName = "Oef13_1_VerwijderItems";
            //var applicationPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + projectName;
            var applicationPath = Directory.GetParent(testDirectory).Parent.Parent.FullName;
            applicationPath = Path.Combine(applicationPath, projectName, @"bin\Debug", projectName + ".exe");
            //var applicationPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar + "Debug";
            //applicationPath = Path.Combine(applicationPath, projectName + ".exe");
            TestContext.Progress.WriteLine("Using EXE: " + applicationPath);
            application = Application.Launch(applicationPath);
            Window window = application.GetWindow("Oef 13.1 Verwijderen");      //This needs to be the title of the window, not the name of the class
            rnd = new Random();

            try
            {
                singersListBox = window.Get<ListBox>("singersListBox");
                seriesListBox = window.Get<ListBox>("seriesListBox");
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

        //[TearDown]
        //public void Teardown()
        //{
        //    TestContext.Progress.WriteLine(TestContext.CurrentContext.Test.Name + " has " + TestContext.CurrentContext.Result.Outcome.Status);
        //}

        [Test, GUTSTest("Should have Singer Listbox")]
        public void ShouldHaveSingersListBox()
        {
            Assert.That(singersListBox, Is.Not.Null);
        }

        [Test, GUTSTest("Shoud have Series Listbox")]
        public void ShouldHaveSeriesListBox()
        {
            Assert.That(seriesListBox, Is.Not.Null);
        }

        [Test, GUTSTest("Should have delete button")]
        public void ShouldHaveDeleteButton()
        {
            Assert.That(deleteButton, Is.Not.Null);
        }

        [Test, GUTSTest("Should delete singer when clicking singer")]
        [Repeat(2)]
        public void ShouldDeleteSingerWhenClickingSinger()
        {
            int countBefore = singersListBox.Items.Count;

            //Click on a singer
            int index = rnd.Next(countBefore);
            singersListBox.Select(index);

            Assert.That(singersListBox.Items.Count, Is.EqualTo(countBefore - 1));
        }

        [Test, GUTSTest("Should delete series when clicking button")]
        [Repeat(2)]
        public void ShouldDeleteSeriesWhenClickingButton()
        {
            int countBefore = seriesListBox.Items.Count;

            //Click on a singer
            int index = rnd.Next(countBefore);
            seriesListBox.Select(index);

            //Click on the button
            deleteButton.Click();

            Assert.That(seriesListBox.Items.Count, Is.EqualTo(countBefore - 1));
        }

        [Test, GUTSTest("Should not delete series when clicking button and nothing is selected")]
        [Repeat(5)]
        public void ShouldNotDeleteSeriesWhenClickingButtonAndNothingIsSelected()
        {
            int countBefore = seriesListBox.Items.Count;

            //Click on the button
            deleteButton.Click();

            Assert.That(seriesListBox.Items.Count, Is.EqualTo(countBefore));
        }

        [Test, GUTSTest("Should not delete singer when clicking button and nothing is selected")]
        [Repeat(5)]
        public void ShouldNotDeleteSingerWhenClickingButtonAndNothingIsSelected()
        {
            int countBefore = singersListBox.Items.Count;

            //Click on the button
            deleteButton.Click();

            Assert.That(singersListBox.Items.Count, Is.EqualTo(countBefore));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            application.Close();
        }

    }
}
