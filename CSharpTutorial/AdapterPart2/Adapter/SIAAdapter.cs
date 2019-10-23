using AdapterPart2.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPart2.Adapter
{
    /*
     * An adapter is required when working with clients that are not compatible with the ISiaClient. Because those clients don't implement the ISiaClient interface and may not have the same model structure used by SIAModel to post data.
     * Clients that are incompatible with the ISiaClient must be included in this Adapter.
     * The Adapter should be use to provide support for any incompatible client that needs to post test data to Stratus.
     * */
    class SIAAdapter : ISiaClient
    {
        private string FileName { get; set; }    
        private TestTypeSelector TestTypeSelector { get; set; }

        #region All Clients supported by the Adapter
        private NUnitClient NUnitClient { get; set; }
        #endregion

        //Specify file name to be used for extracting test result data.
        public SIAAdapter(TestTypeSelector testTypeSelector, string fileName)
        {
            this.TestTypeSelector = testTypeSelector;
            this.FileName = fileName;
        }

        //can be exposed to public if needed but dont see any need for this now. Since objective is to post data
        private string GetTestResultData(string fileName)
        {
            //use file to fetch test result data
            return (this.FileName == "SIA.txt") ? "SIA Data" : string.Empty;
        }

        private string[] FormatTestResultData(TestDataFormatter formatter, string data)
        {
            var obj = new string[3] { "GameWardens", Guid.NewGuid().ToString(), "Passed" };
            return obj;
        }

        

        //client calls this method to post test data to Stratus
        public void Post()
        {
            var testResultData = GetTestResultData(this.FileName);
            switch (this.TestTypeSelector)
            {
                case TestTypeSelector.XUnit: { }break;
                case TestTypeSelector.SIA: {
                        //Can even make the SiaClient use the adapter if needed. Just simply overload the PostTestResultData
                    }
                    break;
                case TestTypeSelector.ExpressionRule: { }break;
                case TestTypeSelector.NUnit:
                    {
                        NUnitClient nunitClient = new NUnitClient();
                        nunitClient.PostTestResults(testResultData);
                    } break;
            }            
        }
    }
}
