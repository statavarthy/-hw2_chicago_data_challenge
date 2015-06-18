///////////////////////////////////////////////////////////////////////////
// ParsedataTest.cs - Program for Testing methods in the ParseData class //
// Language:    C#, .Net Framework 4.0                                   //
// Application: Open Source Computing, HW#2, Summer 2015                 //
// Author:      SMRUTI TATAVARTHY, COMP 412, Loyola University           //
//              statavarthy@luc.edu                                      //
///////////////////////////////////////////////////////////////////////////
/*Summary
 * The test files for this Program are under the testData folder
 * The test cases below are used for checking the functionality of every module in Parse Data
 * Nunit is required to run the tests below. To run any test it is necessary to build this program first
 * A reference to the Nunit famework is essential to build the code.
 * Some of the test cases have been failed deliberately to show that the code is working correctly
 * All the cases have been considered for the year 2013 and only grocery stores have been taken into account.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Hw2
{

    
    [TestFixture]
    class ParseDataTest
    {

        // Unit Test 1 : To check if the file in the given path is present. 
        // If it is not present the test is failed and error message is displayed.
        [Test]
        public void fileNotFoundTest()
        {
            try
            {
                string filePath = "..\\..\\..\\..\\Hw2\\Hw2\\data\\testData\\noFile.csv";
                ParseData pd = new ParseData();
                Hw2.ParseData.Grocery[] groceryData = pd.ParseGrocery(filePath);
                Assert.IsNotNull(groceryData[0].licenseID, "File is Empty");                
            }
            catch(FileNotFoundException ex)
            {
                Assert.Fail("File Not Found");
                Console.WriteLine("File Not available {0} ", ex.StackTrace);
            }


        }

        // Unit Test 2 : To check if the file in the given path is empty. 
        // If it is empty the test is failed and message is displayed.
        [Test]
        public void fileCheckTest()
        {
            string filepath_new= "..\\..\\..\\..\\Hw2\\Hw2\\data\\testData\\Grocery_Stores_FileChk.csv";            
            ParseData pd = new ParseData();
            Hw2.ParseData.Grocery[] groceryData = pd.ParseGrocery(filepath_new);
            Assert.IsNotNull(groceryData[0].licenseID, "File is Empty");
        }

        // Unit Test 3 : To check if the grocery file in the given path is getting parsed. 
        // Test passes if the condition in assert matches, else the test fails. 
        // The license ID in the given file is 980. Hence this test will pass.

        [Test]
        public void ParseGroceryTest()
        {
            try
            {
                string filePath = "..\\..\\..\\..\\Hw2\\Hw2\\data\\testData\\Grocery_Stores_2013.csv";
                ParseData pd = new ParseData();
                Hw2.ParseData.Grocery[] groceryData = pd.ParseGrocery(filePath);
                Assert.AreEqual(groceryData[0].licenseID, "980");
            }
            catch (FileNotFoundException ex)
            {
                Assert.Fail("File Not Available");
                Console.WriteLine("File Not available {0} ", ex.StackTrace);
            }
            catch (Exception e)
            {
                Console.WriteLine("Problem in parsing data {0} ", e.StackTrace);
                Assert.Fail("Problem in parsing {0}", e.StackTrace);
            }

        }

        // Unit Test 4 : To check if the FoodInpection file in the given path is getting parsed. 
        // Test passes if the condition in assert matches, else the test fails. 
        // The license ID in the given file is 2391097.Hence this test will pass.
        [Test]
        public void ParseFoodInspectionTest()
        {
            try
            {
                string filePath = "..\\..\\..\\..\\Hw2\\Hw2\\data\\testData\\Food_Inspections_Test_Grocery_2013.csv";
                ParseData pd = new ParseData();
                Hw2.ParseData.FoodInspection[] foodInspectionData = pd.ParseFoodInspection(filePath);
                Assert.AreEqual(foodInspectionData[0].storeLicenseID, "2391097");                                                             
            }
            catch (FileNotFoundException ex)
            {
                Assert.Fail("File Not Available");
                Console.WriteLine("File Not available {0} ", ex.StackTrace);
            }
            catch (Exception e)
            {
                Console.WriteLine("Problem in parsing data {0} ", e.StackTrace);
                Assert.Fail("Problem in parsing {0}", e.StackTrace);
            }

        }

        // Unit Test 5 : To check if the data in food inspection is for grocery stores and for the year 2013 
        // Test passes if the condition in assert matches, else the test fails. 
        // The data in the file is not for grocery store. Hence this test will fail.
        [Test]
        public void TestForNotGrocery2013()
        {
            try
            {
                string filePath = "..\\..\\..\\..\\Hw2\\Hw2\\data\\testData\\Food_Inspections_Test_NotGrocery_2013.csv";
                ParseData pd = new ParseData();
                Hw2.ParseData.FoodInspection[] foodInspectionData = pd.ParseFoodInspection(filePath);
                Assert.AreEqual(foodInspectionData[0].storeLicenseID, "2391097");


            }
            catch (FileNotFoundException ex)
            {
                Assert.Fail("File Not Available");
                Console.WriteLine("File Not available {0} ", ex.StackTrace);
            }
            catch (Exception e)
            {
                Console.WriteLine("Problem in parsing data {0} ", e.StackTrace);
                Assert.Fail("The parsed data is not a grocery store");
            }

        }

        // Unit Test 6 : To check if the data in food inspection is for grocery stores and for the year 2013 
        // Test passes if the condition in assert matches, else the test fails. 
        // The data in the file is not for the year 2013. Hence this test will fail.
        [Test]
        public void TestForGroceryNot2013()
        {
            try
            {
                string filePath = "..\\..\\..\\..\\Hw2\\Hw2\\data\\testData\\Food_Inspections_Test_Grocery_Not2013.csv";
                ParseData pd = new ParseData();
                Hw2.ParseData.FoodInspection[] foodInspectionData = pd.ParseFoodInspection(filePath);
                Assert.AreEqual(foodInspectionData[0].storeLicenseID, "2391097");


            }
            catch (FileNotFoundException ex)
            {
                Assert.Fail("File Not Available");
                Console.WriteLine("File Not available {0} ", ex.StackTrace);
            }
            catch (Exception e)
            {
                Console.WriteLine("Problem in parsing data {0} ", e.StackTrace);
                Assert.Fail("The Grocery Store was not inspected in 2013");
            }

        }

        // Unit Test 7 : To check if the data in food inspection is for grocery stores and for the year 2013 
        // Test passes if the condition in assert matches, else the test fails.
        // The data in the file is not for the year 2013 and it is not grocery store. Hence this test will fail.

        [Test]

        public void TestForNotGroceryNot2013()
        {
            try
            {
                string filePath = "..\\..\\..\\..\\Hw2\\Hw2\\data\\testData\\Food_Inspections_Test_NotGrocery_Not2013.csv";
                ParseData pd = new ParseData();
                Hw2.ParseData.FoodInspection[] foodInspectionData = pd.ParseFoodInspection(filePath);
                Assert.AreEqual(foodInspectionData[0].storeLicenseID, "2391097");


            }
            catch (FileNotFoundException ex)
            {
                Assert.Fail("File Not Available");
                Console.WriteLine("File Not available {0} ", ex.StackTrace);
            }
            catch (Exception e)
            {
                Console.WriteLine("Problem in parsing data {0} ", e.StackTrace);
                Assert.Fail("The parsed data is not a Grocery Store and was not inspected in the year 2013");
            }

        }


        // Unit Test 8 : To check if the builidng violations file in the given path is getting parsed. 
        // Test passes if the condition in assert matches, else the test fails. 
        // The address in the given file is same as that in the assert condition. Hence this test will pass.
      
        
        [Test]
        public void ParseBuildingInspectionTest()
        {

            try
            {
                string filePath = "..\\..\\..\\..\\Hw2\\Hw2\\data\\testData\\Building_Violations.csv";
                ParseData pd = new ParseData();
                Hw2.ParseData.BuildingInspection[] buildingData  = pd.ParseBuildingInspection(filePath);
                Assert.AreEqual(buildingData[0].buildingAddress, "8046 S THROOP ST");


            }
            catch (FileNotFoundException ex)
            {
                Assert.Fail("File Not Available");
                Console.WriteLine("File Not available {0} ", ex.StackTrace);
            }
            catch (Exception e)
            {
                Console.WriteLine("Problem in parsing data {0} ", e.StackTrace);
                Assert.Fail("Building Data could not be parsed, Problem in parsing");
            }


        }

        // Unit Test 9 : To check if the data in grocery store is correlated with data in food inspection and building violations 
        // Test passes if the condition in assert matches, else the test fails. The data in the final analysis has a license id of 980
        // The correlation of the store with license ID 980 is done with food and building data. Hence this test will pass.
        [Test]
        public void AnalysisTestcase1()
        {
            int n=0;
            try
            {
                string filePath1 = "..\\..\\..\\..\\Hw2\\Hw2\\data\\testData\\Grocery_Stores_Analysis_case1.csv";
                string filePath2 = "..\\..\\..\\..\\Hw2\\Hw2\\data\\testData\\Food_Inspections_Analysis_case1.csv";
                string filePath3 = "..\\..\\..\\..\\Hw2\\Hw2\\data\\testData\\Building_Violations_Analysis_case1.csv";
                ParseData analysisTest = new ParseData();
                Hw2.ParseData.Grocery[] groceryData = analysisTest.ParseGrocery(filePath1);
                Hw2.ParseData.FoodInspection[] foodInspectionData = analysisTest.ParseFoodInspection(filePath2);
                Hw2.ParseData.BuildingInspection[] buildingData = analysisTest.ParseBuildingInspection(filePath3);

                Hw2.ParseData.FinalAnalysis[] finalAnalysis = analysisTest.AnalysisGroceryFood(groceryData, foodInspectionData, buildingData,ref n);                
                
                Assert.AreEqual(finalAnalysis[0].storeLicenceID, "980");


            }
            catch (FileNotFoundException ex)
            {
                Assert.Fail("File Not Available");
                Console.WriteLine("File Not available {0} ", ex.StackTrace);
            }
            catch (Exception e)
            {
                Console.WriteLine("Problem in parsing data {0} ", e.StackTrace);
                Assert.Fail("Analysis Failed");
            }


        }

        // Unit Test 10 : To check if the data in grocery store is correlated with data in food inspection and building violations 
        // This test will check for multiple entries having different inspection dates and will consider the latest record.
        // The correlation of the store with latest inspection is done with food and building data. Hence this test will pass.

        [Test]
        public void AnalysisTestcase2()
        {
            int n = 0;
            try
            {
                string filePath1 = "..\\..\\..\\..\\Hw2\\Hw2\\data\\testData\\Grocery_Stores_Analysis_case2.csv";
                string filePath2 = "..\\..\\..\\..\\Hw2\\Hw2\\data\\testData\\Food_Inspections_Analysis_case2.csv";
                string filePath3 = "..\\..\\..\\..\\Hw2\\Hw2\\data\\testData\\Building_Violations_Analysis_case2.csv";
                ParseData analysisTest = new ParseData();
                Hw2.ParseData.Grocery[] groceryData = analysisTest.ParseGrocery(filePath1);
                Hw2.ParseData.FoodInspection[] foodInspectionData = analysisTest.ParseFoodInspection(filePath2);
                Hw2.ParseData.BuildingInspection[] buildingData = analysisTest.ParseBuildingInspection(filePath3);

                Hw2.ParseData.FinalAnalysis[] finalAnalysis = analysisTest.AnalysisGroceryFood(groceryData, foodInspectionData, buildingData,ref n);

                Assert.AreEqual(finalAnalysis[0].storeInspectionStatus.ToUpper(), "FAIL");


            }
            catch (FileNotFoundException ex)
            {
                Assert.Fail("File Not Available");
                Console.WriteLine("File Not available {0} ", ex.StackTrace);
            }
            catch (Exception e)
            {
                Console.WriteLine("Problem in parsing data {0} ", e.StackTrace);
                Assert.Fail("Analysis Failed");
            }


        }

        // Unit Test 11 : To check if the data in grocery store is correlated with data in food inspection and building violations 
        // This test will check that the final database does not have any records of the stores that have passed the food inpsection.
        // Hence they will not be populated in the final database. This test will pass since the data is NULL.
        [Test]
        public void AnalysisTestcase3()
        {
            int n = 0;
            try
            {
                string filePath1 = "..\\..\\..\\..\\Hw2\\Hw2\\data\\testData\\Grocery_Stores_Analysis_case3.csv";
                string filePath2 = "..\\..\\..\\..\\Hw2\\Hw2\\data\\testData\\Food_Inspections_Analysis_case3.csv";
                string filePath3 = "..\\..\\..\\..\\Hw2\\Hw2\\data\\testData\\Building_Violations_Analysis_case3.csv";
                ParseData analysisTest = new ParseData();
                Hw2.ParseData.Grocery[] groceryData = analysisTest.ParseGrocery(filePath1);
                Hw2.ParseData.FoodInspection[] foodInspectionData = analysisTest.ParseFoodInspection(filePath2);
                Hw2.ParseData.BuildingInspection[] buildingData = analysisTest.ParseBuildingInspection(filePath3);

                Hw2.ParseData.FinalAnalysis[] finalAnalysis = analysisTest.AnalysisGroceryFood(groceryData, foodInspectionData, buildingData, ref n);

                Assert.IsNullOrEmpty(finalAnalysis[0].storeLicenceID, "FAIL");


            }
            catch (FileNotFoundException ex)
            {
                Assert.Fail("File Not Available");
                Console.WriteLine("File Not available {0} ", ex.StackTrace);
            }
            catch (Exception e)
            {
                Console.WriteLine("Problem in parsing data {0} ", e.StackTrace);
                Assert.Fail("Data is null");
            }


        }


    }
}
