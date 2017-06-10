using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Interfaces;

namespace GUTSNet
{
    [AttributeUsage(AttributeTargets.Class,
                AllowMultiple = false)]
    public class GUTSSuiteAttribute : Attribute, ITestAction
    {
        public ActionTargets Targets
        {
            get
            {
                return ActionTargets.Suite;
            }
        }

        public void AfterTest(ITest test)
        {
            Console.WriteLine("Sending Results..");
            GUTSTestManager.Use.SendResults();
        }

        public void BeforeTest(ITest test)
        {
            
        }
    }
}
