using AdapterPart2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPart2.Client
{
    class NUnitClient : INUnitClient
    {
        private string FileName { get; set; }
        private NUnitModel NUnitModel { get; set; }

        //Specify file name to be used for extracting test result data.
        public NUnitClient(string fileName)
        {
            this.FileName = fileName;
        }

       
        //Intended for an adapter use
        public NUnitClient()
        {

        }


        //can be exposed to public if needed but dont see any need for this now. Since objective is to post data
        private string GetTestResultData(string fileName)
        {
            //use file to fetch test result data
            return (fileName == "NUNIT.txt") ? "NUNIT Data" : string.Empty;
        }

        private string[] FormatTestResultData(TestDataFormatter formatter, string data)
        {
            var obj = new string[3] { "GameWardens", Guid.NewGuid().ToString(), "Failed:Error(1)" };
            return obj;
        }

        /*
         * This method is abstracted/private and so is only available when you do INUnitClient cli = new NUNitClient();
         * Client calls this method to post test data TO its compatible reporter.
         * */
        void INUnitClient.PostTestResults()
        {
            var testResultData = GetTestResultData(this.FileName);

            //We will assume we formatted test data into JSON
            var jsonTestResultData = FormatTestResultData(TestDataFormatter.JSON, testResultData);

            //Populate NUnitModel with formmatted data
            NUnitModel = new NUnitModel();
            NUnitModel.ProjName = jsonTestResultData[0];
            NUnitModel.TestId = jsonTestResultData[1];
            NUnitModel.TestStatus = jsonTestResultData[2];

            //Automapping not required when working with a compatible reporter. See overloaded method for when working with incompatible reporter.

            //Post to Stratus using its expected payload in SIAModel format
            Console.WriteLine($"Posting to Stratus: \nProject Name: {NUnitModel.ProjName}\nTest ID: {NUnitModel.TestId}\nStatus: {NUnitModel.TestStatus}\n...");
        }

        /*
         * when client is incompatible with the reporter, an adapter is then used.
         * Automapping will be necessary here to sync the contract/payload needed for posting to reporter.
         * adapter calls this method of the client to help post test data a reporter not supported by the client.
         * */
        public void PostTestResults(string testData)
        {
            var testResultData = testData;

            //We will assume we formatted test data into JSON
            var jsonTestResultData = FormatTestResultData(TestDataFormatter.JSON, testResultData);

            //Populate NUnitModel with formmatted data
            NUnitModel = new NUnitModel();
            NUnitModel.ProjName = jsonTestResultData[0];
            NUnitModel.TestId = jsonTestResultData[1];
            NUnitModel.TestStatus = jsonTestResultData[2];

            //Perform Any automapping needed.
            var SIAModel = new SIAModel();
            SIAModel.ProjectName = NUnitModel.ProjName;
            SIAModel.TestID = NUnitModel.TestId;
            SIAModel.TestStatus = NUnitModel.TestStatus;

            //Post to Stratus using its expected payload in SIAModel format
            Console.WriteLine($"Posting to Stratus: \nProject Name: {SIAModel.ProjectName}\nTest ID: {SIAModel.TestID}\nStatus: {SIAModel.TestStatus}\n...");
        }
    }
}
