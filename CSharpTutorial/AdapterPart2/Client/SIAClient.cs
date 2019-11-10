using AdapterPart2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPart2.Client
{
    /*
     * SIAClient is compatible with the ISiaClient.
     * An adapter is not required to post data to Stratus using the SIAClient.
     * */
    class SIAClient : ISiaClient
    {
        private string FileName { get; set; }
        private SIAModel SIAModel { get; set; }

        //Specify file name to be used for extracting test result data.
        public SIAClient(string fileName)
        {
            this.FileName = fileName;
        }
        

        //can be exposed to public if needed but dont see any need for this now. Since objective is to post data
        private string GetTestResultData(string fileName)
        {
            //use file to fetch test result data
            return (fileName == "SIA.txt") ? "SIA Data" : string.Empty;
        }

        private string[] FormatTestResultData(TestDataFormatter formatter, string data)
        {
            var obj = new string[3] { "GameWardens", Guid.NewGuid().ToString(), "Passed" };
            return obj;
        }

        /*
         * This method is abstracted/private and so is only available when you do INUnitClient cli = new NUNitClient();
         * client calls this method to post test data to Stratus
         * */
        void ISiaClient.Post()
        {
            var testResultData = GetTestResultData(this.FileName);

            //We will assume we formatted test data into JSON
            var jsonTestResultData = FormatTestResultData(TestDataFormatter.JSON, testResultData);

            //Populate SIAModel with formmatted data
            SIAModel = new SIAModel();
            SIAModel.ProjectName = jsonTestResultData[0];
            SIAModel.TestID = jsonTestResultData[1];
            SIAModel.TestStatus = jsonTestResultData[2];

            //Using the compatible SIAModel that Stratus understands, POST result
            Console.WriteLine($"Posting to Stratus: \nProject Name: {SIAModel.ProjectName}\nTest ID: {SIAModel.TestID}\nStatus: {SIAModel.TestStatus}\n...");
        }
    }
}
