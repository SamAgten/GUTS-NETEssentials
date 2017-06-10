using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GUTSNet
{
    class GUTSTestManager
    {
        private const string URL = "Localhost";     //The host url
        private static GUTSTestManager instance = null;
        public static GUTSTestManager Use
        {
            get
            {
                if (instance == null)
                    instance = new GUTSTestManager();

                return instance;
            }
        }

        private Guid guid;
        private HttpClient client;
        private List<GUTSTestResult> testResults;

        private GUTSTestManager()
        {
            //Create a new guid
            guid = System.Guid.NewGuid();

            //Create a new http client to be shared across tests
            client = new HttpClient();

            //Keep track of the test results
            testResults = new List<GUTSTestResult>();
        }

        public void AddTestResult(GUTSTestResult testResult)
        {
            //Do not add duplicates
            GUTSTestResult duplicate = testResults.FirstOrDefault(x => x.Name == testResult.Name);
            if (duplicate != null) return;

            //Add it to the list!
            testResults.Add(testResult);
        }

        public void SendResults()
        {
            foreach(GUTSTestResult test in testResults)
            {
                Console.WriteLine(test.Name + ": " + test.Passed);
            }

            /*
             * var values = new Dictionary<string, string>
{
   { "thing1", "hello" },
   { "thing2", "world" }
};

var content = new FormUrlEncodedContent(values);

var response = await client.PostAsync("http://www.example.com/recepticle.aspx", content);

var responseString = await response.Content.ReadAsStringAsync();
*/
        }
    }
}
