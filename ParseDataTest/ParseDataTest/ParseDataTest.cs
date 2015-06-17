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
        [Test]
        public void ParseGroceryTest()
        {
            try
            {
                string filePath = @"C:\Users\Smruti\Documents\SMRUTI\STUDIES\Loyola\Open Source Computing\Hw2\Hw2_chicago_data_challenge\Hw2\Hw2\data\testData\Grocery_Stores_2013.csv";
                ParseData pd = new ParseData();
                Hw2.ParseData.Grocery[] groceryData = pd.ParseGrocery(filePath);
                Assert.AreEqual(groceryData[0].licenseID, "980");
            }
            catch (FileNotFoundException ex)
            {
                Assert.Fail("File Not Available");
               // Console.WriteLine("File Not available {0} ", ex.StackTrace);
            }
            catch (Exception e)
            {
                //Console.WriteLine("Problem in parsing data {0} ", e.StackTrace);
                Assert.Fail("Problem in parsing {0}", e.StackTrace);
            }

        }
        [Test]
        public void ParseFoodInspectionTest()
        {
            try
            {
                string filePath = @"C:\Users\Smruti\Documents\SMRUTI\STUDIES\Loyola\Open Source Computing\Hw2\Hw2_chicago_data_challenge\Hw2\Hw2\data\testData\Food_Inspections_Test_Grocery_2013.csv";
                ParseData pd = new ParseData();
                Hw2.ParseData.FoodInspection[] foodInspectionData = pd.ParseFoodInspection(filePath);
                Assert.AreEqual(foodInspectionData[0].storeLicenseID, "2391097");
                                              
               
            }
            catch (FileNotFoundException ex)
            {
                Assert.Fail("File Not Available");
                // Console.WriteLine("File Not available {0} ", ex.StackTrace);
            }
            catch (Exception e)
            {
                //Console.WriteLine("Problem in parsing data {0} ", e.StackTrace);
                Assert.Fail("Problem in parsing {0}", e.StackTrace);
            }

        }
        [Test]
        public void TestForNotGrocery2013()
        {
            try
            {
                string filePath = @"C:\Users\Smruti\Documents\SMRUTI\STUDIES\Loyola\Open Source Computing\Hw2\Hw2_chicago_data_challenge\Hw2\Hw2\data\testData\Food_Inspections_Test_NotGrocery_2013.csv";
                ParseData pd = new ParseData();
                Hw2.ParseData.FoodInspection[] foodInspectionData = pd.ParseFoodInspection(filePath);
                Assert.AreEqual(foodInspectionData[0].storeLicenseID, "2391097");


            }
            catch (FileNotFoundException ex)
            {
                Assert.Fail("File Not Available");
                // Console.WriteLine("File Not available {0} ", ex.StackTrace);
            }
            catch (Exception e)
            {
                //Console.WriteLine("Problem in parsing data {0} ", e.StackTrace);
                Assert.Fail("The parsed data is not a grocery store");
            }

        }
        [Test]
        public void TestForGroceryNot2013()
        {
            try
            {
                string filePath = @"C:\Users\Smruti\Documents\SMRUTI\STUDIES\Loyola\Open Source Computing\Hw2\Hw2_chicago_data_challenge\Hw2\Hw2\data\testData\Food_Inspections_Test_Grocery_Not2013.csv";
                ParseData pd = new ParseData();
                Hw2.ParseData.FoodInspection[] foodInspectionData = pd.ParseFoodInspection(filePath);
                Assert.AreEqual(foodInspectionData[0].storeLicenseID, "2391097");


            }
            catch (FileNotFoundException ex)
            {
                Assert.Fail("File Not Available");
                // Console.WriteLine("File Not available {0} ", ex.StackTrace);
            }
            catch (Exception e)
            {
                //Console.WriteLine("Problem in parsing data {0} ", e.StackTrace);
                Assert.Fail("The Grocery Store was not inspected in 2013");
            }

        }

        [Test]

        public void TestForNotGroceryNot2013()
        {
            try
            {
                string filePath = @"C:\Users\Smruti\Documents\SMRUTI\STUDIES\Loyola\Open Source Computing\Hw2\Hw2_chicago_data_challenge\Hw2\Hw2\data\testData\Food_Inspections_Test_NotGrocery_Not2013.csv";
                ParseData pd = new ParseData();
                Hw2.ParseData.FoodInspection[] foodInspectionData = pd.ParseFoodInspection(filePath);
                Assert.AreEqual(foodInspectionData[0].storeLicenseID, "2391097");


            }
            catch (FileNotFoundException ex)
            {
                Assert.Fail("File Not Available");
                // Console.WriteLine("File Not available {0} ", ex.StackTrace);
            }
            catch (Exception e)
            {
                //Console.WriteLine("Problem in parsing data {0} ", e.StackTrace);
                Assert.Fail("The parsed data is not a Grocery Store and was not inspected in the year 2013");
            }

        }

      
        
        [Test]
        public void ParseBuildingInspectionTest()
        {

            try
            {
                string filePath = @"C:\Users\Smruti\Documents\SMRUTI\STUDIES\Loyola\Open Source Computing\Hw2\Hw2_chicago_data_challenge\Hw2\Hw2\data\testData\Building_Violations.csv";
                ParseData pd = new ParseData();
                Hw2.ParseData.BuildingInspection[] buildingData  = pd.ParseBuildingInspection(filePath);
                Assert.AreEqual(buildingData[0].buildingAddress, "8046 S THROOP ST");


            }
            catch (FileNotFoundException ex)
            {
                Assert.Fail("File Not Available");
                // Console.WriteLine("File Not available {0} ", ex.StackTrace);
            }
            catch (Exception e)
            {
                //Console.WriteLine("Problem in parsing data {0} ", e.StackTrace);
                Assert.Fail("Building Data could not be parsed, Problem in parsing");
            }


        }


        [Test]
        public void AnalysisTestcase1()
        {
            int n=0;
            try
            {
                string filePath1 = @"C:\Users\Smruti\Documents\SMRUTI\STUDIES\Loyola\Open Source Computing\Hw2\Hw2_chicago_data_challenge\Hw2\Hw2\data\testData\Grocery_Stores_Analysis_case1.csv";
                string filePath2 = @"C:\Users\Smruti\Documents\SMRUTI\STUDIES\Loyola\Open Source Computing\Hw2\Hw2_chicago_data_challenge\Hw2\Hw2\data\testData\Food_Inspections_Analysis_case1.csv";
                string filePath3 = @"C:\Users\Smruti\Documents\SMRUTI\STUDIES\Loyola\Open Source Computing\Hw2\Hw2_chicago_data_challenge\Hw2\Hw2\data\testData\Building_Violations_Analysis_case1.csv";
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
                // Console.WriteLine("File Not available {0} ", ex.StackTrace);
            }
            catch (Exception e)
            {
                //Console.WriteLine("Problem in parsing data {0} ", e.StackTrace);
                Assert.Fail("Analysis Failed");
            }


        }

        [Test]
        public void AnalysisTestcase2()
        {
            int n = 0;
            try
            {
                string filePath1 = @"C:\Users\Smruti\Documents\SMRUTI\STUDIES\Loyola\Open Source Computing\Hw2\Hw2_chicago_data_challenge\Hw2\Hw2\data\testData\Grocery_Stores_Analysis_case2.csv";
                string filePath2 = @"C:\Users\Smruti\Documents\SMRUTI\STUDIES\Loyola\Open Source Computing\Hw2\Hw2_chicago_data_challenge\Hw2\Hw2\data\testData\Food_Inspections_Analysis_case2.csv";
                string filePath3 = @"C:\Users\Smruti\Documents\SMRUTI\STUDIES\Loyola\Open Source Computing\Hw2\Hw2_chicago_data_challenge\Hw2\Hw2\data\testData\Building_Violations_Analysis_case2.csv";
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
                // Console.WriteLine("File Not available {0} ", ex.StackTrace);
            }
            catch (Exception e)
            {
                //Console.WriteLine("Problem in parsing data {0} ", e.StackTrace);
                Assert.Fail("Analysis Failed");
            }


        }
        [Test]
        public void AnalysisTestcase3()
        {
            int n = 0;
            try
            {
                string filePath1 = @"C:\Users\Smruti\Documents\SMRUTI\STUDIES\Loyola\Open Source Computing\Hw2\Hw2_chicago_data_challenge\Hw2\Hw2\data\testData\Grocery_Stores_Analysis_case3.csv";
                string filePath2 = @"C:\Users\Smruti\Documents\SMRUTI\STUDIES\Loyola\Open Source Computing\Hw2\Hw2_chicago_data_challenge\Hw2\Hw2\data\testData\Food_Inspections_Analysis_case3.csv";
                string filePath3 = @"C:\Users\Smruti\Documents\SMRUTI\STUDIES\Loyola\Open Source Computing\Hw2\Hw2_chicago_data_challenge\Hw2\Hw2\data\testData\Building_Violations_Analysis_case3.csv";
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
                // Console.WriteLine("File Not available {0} ", ex.StackTrace);
            }
            catch (Exception e)
            {
                //Console.WriteLine("Problem in parsing data {0} ", e.StackTrace);
                Assert.Fail("Data is null");
            }


        }


    }
}
