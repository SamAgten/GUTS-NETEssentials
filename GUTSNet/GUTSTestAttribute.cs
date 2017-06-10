using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Interfaces;

namespace GUTSNet
{
    [AttributeUsage(AttributeTargets.Method,
                AllowMultiple = false)]
    public class GUTSTestAttribute : Attribute, ITestAction
    {
        private string displayName = "";
        public GUTSTestAttribute(string displayName)
        {
            this.displayName = displayName;
        }

        public ActionTargets Targets
        {
            get
            {
                return ActionTargets.Test; // throw new NotImplementedException();
            }
        }

        public void AfterTest(ITest test)
        {
            GUTSTestResult testResult = new GUTSTestResult();
            testResult.Name = displayName;
            testResult.Passed = (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed) ? true : false;
        }

        public void BeforeTest(ITest test)
        {
        }

    }
}
