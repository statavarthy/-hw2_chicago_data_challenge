///////////////////////////////////////////////////////////////////////
// HW2DataChallenge.cs - Program for HW2 Data Challenge              //
// Language:    C#, .Net Framework 4.0                               //
// Application: Open Source Computing, HW#2, Summer 2015             //
// Author:      SMRUTI TATAVARTHY, COMP 412, Loyola University       //
//              statavarthy@luc.edu                                  //
///////////////////////////////////////////////////////////////////////
/*Summary
 * 
 * Analysing the grocery stores in Chicago to see if they have undergone food inspections.
 * Populating the list of grocery stores to see the ones that have failed the inspection.
 * Populating the list of grocery stores to see the ones that have not undergone any food inspection.
 * Verifying if the same grocery stores also have building violations by populating the building violation data.
 * All the data is considered for the year 2013.
 * 
 * Maintenance History:
 * --------------------
 * ver 1.5 : 14 Jun 2015
 * - Fixed the logic for correlating  grcoery stores with food inspections
 * 
 * ver 1.4 : 14 Jun 2015
 * - Added maintaineance history, Summary and comments 
 *  
 * ver 1.3 : 14 Jun 2015
 * - Added the testMode boolean to take partial data for test/debug purposes 
 * 
 * ver 1.2 : 14 Jun 2015
 * - Modified the correlation logic between the Grocery Stores and Food Inspection
 * 
 * ver 1.1 : 14 Jun 2015
 * - Modified the hardcoded path and used relative path
 * 
 * ver 1.0 : 13 Jun 2015
 * - Initial version
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw2
{
    struct Grocery
    {
        public string storeName;
        public string licenseID;
    };

    struct BuildingInspection
    {
        public string buildingInspectionStatus;
        public string buildingAddress;
        public string buildingID;
    };

    struct FoodInspection
    {
        public string foodInspectionStatus;
        public string storeLicenseID;
        public string inspectionDate;
    };

    struct FinalAnalysis
    {
        public string storeInspectionStatus;
        public string storeName;
        public string storeLicenceID;
    };

    struct TempRecordMatch
    {
        public string status;
        public string licenseID;
        public DateTime date;
    }

    class HW2DataChallenge
    {
        static void Main(string[] args)
        {
            Grocery[] groceryData = new Grocery[6000];
            FoodInspection[] foodInspectionData = new FoodInspection[40000];
            FinalAnalysis[] finalAnalysis = new FinalAnalysis[20000];
            BuildingInspection[] buildingData = new BuildingInspection[200000];
            TempRecordMatch[] record_match = new TempRecordMatch[10];

            int i = 0;
            int groceryStoreCnt = 0;
            long foodInspectionCnt = 0;
            string dataFolderName = "";
            
            // boolean to identify the data folder whether test or actual
            // testMode = true --> take the data from testData folder
            // testMode = false --> take the data from actualData folder
            bool testMode = true;

            // specify the folder from which data is to be taken
            if (testMode)
                dataFolderName = "testData";
            else
                dataFolderName = "actualData";

            // build the full path of the data files
            string filePath = "..\\..\\data\\" + dataFolderName + "\\";

            // STEP - 1 : Parse the "Grocery Stores" data
            var reader = new StreamReader(File.OpenRead(@filePath + "Grocery_Stores_2013.csv"));
            var line0 = reader.ReadLine();

            // Populating groceryData
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                groceryData[i].storeName = values[0];
                groceryData[i].licenseID = values[1];
                if (testMode)
                   // Console.WriteLine("\n {0} {1} ", groceryData[i].storeName, groceryData[i].licenseID);
                i++;
            }

            groceryStoreCnt = i;            
            long j = 0;
            long num = 0;

            // STEP - 2 : Parse the Food Inspections data
            var reader1 = new StreamReader(File.OpenRead(@filePath + "Food_Inspections_2014.csv"));
            var line0_new = reader1.ReadLine();           

            while (!reader1.EndOfStream)
            {
                var line = reader1.ReadLine();
                var values = line.Split(',');
                //checking the start of each line
                bool chk = long.TryParse(values[0], out num); 
              
                if (chk && num > 9999)
                {
                    string facilityType = values[4].ToUpper();
                    bool isGroceryStore = facilityType.Contains("GROCERY");
                    string inspectionDate = values[10];
                    //Considering records for 2013 only
                    bool is2013Record = inspectionDate.Contains("2013");

                    // Populating foodInspection data
                    if (isGroceryStore && is2013Record)
                    {
                        foodInspectionData[j].storeLicenseID = values[3];
                        foodInspectionData[j].foodInspectionStatus = values[12];
                        foodInspectionData[j].inspectionDate = values[10];
                        if (testMode)
                           // Console.WriteLine("\n {0}  {1} {2} ", foodInspectionData[j].storeLicenseID, foodInspectionData[j].foodInspectionStatus, foodInspectionData[j].inspectionDate);
                        j++;
                    }

                }

            }
            foodInspectionCnt = j;

            long x = 0;
            long bldg_num = 0;
            long num1 = 0;

            // STEP - 3 : Parse the Building Violations data
            var reader3 = new StreamReader(File.OpenRead(@filePath + "Building_Violations.csv"));

            var line_first = reader3.ReadLine();
            // Populating foodInspection data           
            while (!reader3.EndOfStream)
            {
                var line = reader3.ReadLine();
                var values = line.Split(new string[] { "#@" }, StringSplitOptions.None);
                buildingData[x].buildingID = values[0];
                buildingData[x].buildingAddress = values[16];
                
                if (testMode)
                    //Console.WriteLine("\n {0}  {1} ", buildingData[x].buildingID, buildingData[x].buildingAddress);

                x++;
            }
            bldg_num = x;

            int n = 0;
            int c = 0;
            long m = 0;            
            DateTime maxDate = DateTime.MinValue;

            // Analysis between Grocery Stores and Food Inspection
            for (long k = 0; k < groceryStoreCnt; k++)
            {
                bool isInspcted = false;
                for (m = 0; m < foodInspectionCnt; m++)
                {
                    if (string.Compare(groceryData[k].licenseID, foodInspectionData[m].storeLicenseID) == 0)
                    {
                        isInspcted = true;
                        record_match[c].licenseID = groceryData[k].licenseID;
                        record_match[c].date = Convert.ToDateTime(foodInspectionData[m].inspectionDate);
                        record_match[c].status = foodInspectionData[m].foodInspectionStatus;
                    }
                    c++;
                }

                // populating the grocery stores that are not matched (not been inspected)
                if (!isInspcted)
                {
                    finalAnalysis[n].storeInspectionStatus = "NOT INSPECTED";
                    finalAnalysis[n].storeName = groceryData[k].storeName;
                    finalAnalysis[n].storeLicenceID = groceryData[k].licenseID;
                    n++;
                }
                
                // Considering the latest (max) inspection date for all the matched records
                if (isInspcted)
                {
                    foreach (var r in record_match)
                    {
                        if (maxDate < r.date)
                        {
                            maxDate = r.date;
                        }
                    }

                    // considering only failed inpsection records
                    for (int q = 0; q < record_match.Length; q++)
                    {
                        if (record_match[q].date == maxDate)
                        {
                            if (record_match[q].status != null && string.Compare(record_match[q].status.ToUpper(), "FAIL") == 0)
                            {
                                finalAnalysis[n].storeInspectionStatus = record_match[q].status;
                                finalAnalysis[n].storeName = groceryData[k].storeName;
                                finalAnalysis[n].storeLicenceID = record_match[q].licenseID;
                                n++;
                            }
                        }

                        /*
                        for (int u = 0; u < record_match.Length; u++)
                        {
                            record_match[u].date = DateTime.MinValue;
                            record_match[u].licenseID = null;
                            record_match[u].status = null;
                        }
                         */
                    }
                }

                
            }

            // printing the final result of correlation
            Console.WriteLine("Final analysis data :");
            Console.WriteLine("******************************************************************");            
            Console.WriteLine("License ID \t\t Store Name \t\t Inspection Status");


            for (i = 0; i < n; i++)
            {               
                Console.WriteLine("{0} \t\t\t {1} \t\t {2}", finalAnalysis[i].storeLicenceID, finalAnalysis[i].storeName, finalAnalysis[i].storeInspectionStatus);

            }

        }
    }
}
