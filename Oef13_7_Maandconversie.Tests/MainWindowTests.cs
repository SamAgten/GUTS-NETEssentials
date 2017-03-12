using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;
using TestStack.White.UIItems.WindowItems;

namespace Oef13_7_Maandconversie.Tests
{
    [TestFixture]
    public class MainWindowTests
    {
        private Application application;

        [OneTimeSetUp]
        public void Setup()
        {
            var applicationDirectory = TestContext.CurrentContext.TestDirectory;

            string projectName = "Oef13_7_Maandconversie";
            var applicationPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar + "Debug";
            applicationPath = Path.Combine(applicationPath, projectName + ".exe");
            TestContext.Progress.WriteLine("Using EXE: " + applicationPath);
            application = Application.Launch(applicationPath);
            Window window = application.GetWindow("Oef 13.7 Maandconversie");      //This needs to be the title of the window, not the name of the class

            try
            {
                //Get all the UI items here
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
        public void DeleteMe()
        {
            Assert.That(true, Is.True);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            application.Close();
        }
    }
}
