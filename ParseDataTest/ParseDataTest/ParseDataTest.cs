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
                string filePath = @"C:\Users\Smruti\Documents\SMRUTI\STUDIES\Loyola\Open Source Computing\Hw2\Hw2_chicago_data_challenge\Hw2\Hw2\data\testData\";
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

        public void ParseFoodInspectionTest()
        {

        }

        public void ParseBuildingInspectionTest()
        {

        }


    }
}
