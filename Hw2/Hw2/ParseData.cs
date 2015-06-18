////////////////////////////////////////////////////////////////////////
// ParseData.cs - Program for defining methods to parse different data//
//                and analyze them                                    //
// Language:    C#, .Net Framework 4.0                                //
// Application: Open Source Computing, HW#2, Summer 2015              //
// Author:      SMRUTI TATAVARTHY, COMP 412, Loyola University        //
//              statavarthy@luc.edu                                   //
////////////////////////////////////////////////////////////////////////
/*Summary
 * This program contain methods for parsing data.
 * The different methods help in populating the gorcery, food Inspection
 * and Building violations data.
 * The Analysis method helps in identifying the correlation between the grocery
 * and food inspection data alongwith bulding violation.
 * 
 
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw2
{
    public class ParseData
    {
        public struct Grocery
        {
            public string storeName;
            public string licenseID;
            public string groceryAddress;
        };

        public struct BuildingInspection
        {
            public string buildingInspectionStatus;
            public string buildingAddress;
            public string violation;
        };

        public struct FoodInspection
        {
            public string foodInspectionStatus;
            public string storeLicenseID;
            public string inspectionDate;
        };

        public struct FinalAnalysis
        {
            public string storeInspectionStatus;
            public string storeName;
            public string storeLicenceID;
            public string addressFailedGrocery;
            public string violation;
        };

        public struct TempRecordMatch
        {
            public string status;
            public string licenseID;
            public DateTime date;
        }
            
        Grocery[] groceryData = new Grocery[6000];
        FoodInspection[] foodInspectionData = new FoodInspection[40000];
        FinalAnalysis[] finalAnalysis = new FinalAnalysis[20000];
        BuildingInspection[] buildingData = new BuildingInspection[200000];
        TempRecordMatch[] record_match = new TempRecordMatch[1000];

        int groceryStoreCnt = 0;
        long foodInspectionCnt = 0;
        long bldg_num = 0;
        
        /**
         */
        public Grocery[] ParseGrocery(String filePath)
        {
            int i = 0;    
            // STEP - 1 : Parse the "Grocery Stores" data            
            //try
            //{


                var reader = new StreamReader(File.OpenRead(@filePath));
                var line0 = reader.ReadLine();
                // Populating groceryData
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(new string[] { "#@" }, StringSplitOptions.None);
                    groceryData[i].storeName = values[0];
                    groceryData[i].licenseID = values[1];
                    groceryData[i].groceryAddress = values[5];                    
                    i++;
                }

                groceryStoreCnt = i;
            //}                
            //catch (FileNotFoundException fnfe)
            //{
            //    Console.WriteLine("\n Grocery File not Found {0}", fnfe.StackTrace);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("\n Exception while parsing the grocery file {0}", e.StackTrace);
            //}

            return groceryData;
        }

        public FoodInspection[] ParseFoodInspection(String filePath)
        {
            long j = 0;
            long num = 0;
            //try
            //{


                // STEP - 2 : Parse the Food Inspections data
                var reader1 = new StreamReader(File.OpenRead(@filePath));
                var line0_new = reader1.ReadLine();

                while (!reader1.EndOfStream)
                {
                    var line = reader1.ReadLine();
                    var values = line.Split(new string[] { "#@" }, StringSplitOptions.None);
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
                            j++;
                        }
                    }
                }
                foodInspectionCnt = j;
            //}
            //catch (FileNotFoundException fnfe)
            //{
            //    Console.WriteLine("\n Food Inspection File not Found {0}", fnfe.StackTrace);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("\n Exception while parsing the Food Inpsection file {0}", e.StackTrace);
            //}
            return foodInspectionData;
        }
        public BuildingInspection[] ParseBuildingInspection(String filePath)
        {
            long x = 0;            
            long num1 = 0;
            try
            {


                // STEP - 3 : Parse the Building Violations data
                var reader3 = new StreamReader(File.OpenRead(@filePath));

                var line_first = reader3.ReadLine();
                // Populating Building Violations data           
                while (!reader3.EndOfStream)
                {
                    var line = reader3.ReadLine();
                    var values = line.Split(new string[] { "#@" }, StringSplitOptions.None);
                    buildingData[x].violation = values[6];
                    buildingData[x].buildingAddress = values[16];
                    x++;
                }
                bldg_num = x;
            }
            catch (FileNotFoundException fnfe)
            {
                Console.WriteLine("\n Building Inspection File not Found {0}", fnfe.StackTrace);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n Exception while parsing the Building Inspection file {0}", e.StackTrace);
            }
            return buildingData;
        
        }

        public FinalAnalysis[] AnalysisGroceryFood(Grocery[] groceryData, FoodInspection[] foodInspectionData, BuildingInspection[] buildingInspectionData, ref int n)
        {
            
            int c = 0;
            long m = 0;
            DateTime maxDate = DateTime.MinValue;            
            // Analysis between Grocery Stores and Food Inspection
            for (long k = 0; k < groceryStoreCnt; k++)
            {
                bool isInspcted = false;
                c = 0;
                maxDate = DateTime.MinValue;
                for (m = 0; m < foodInspectionCnt; m++)
                {

                    //comparing the license ID of grocery stores with the stores in food inspections data
                    if (string.Compare(groceryData[k].licenseID, foodInspectionData[m].storeLicenseID) == 0)
                    {
                        isInspcted = true;
                        record_match[c].licenseID = groceryData[k].licenseID;
                        record_match[c].date = Convert.ToDateTime(foodInspectionData[m].inspectionDate);
                        record_match[c].status = foodInspectionData[m].foodInspectionStatus;
                        c++;
                    }

                }

                // populating the grocery stores that are not matched (not been inspected)
                if (!isInspcted)
                {
                    finalAnalysis[n].storeInspectionStatus = "NOT INSPECTED";
                    finalAnalysis[n].storeName = groceryData[k].storeName;
                    finalAnalysis[n].storeLicenceID = groceryData[k].licenseID;
                    finalAnalysis[n].addressFailedGrocery = groceryData[k].groceryAddress;

                    // populating the grocery stores not inspected and having building violations
                    for (int g = 0; g < bldg_num; g++)
                    {

                        //comparing grocery store address with building address to check for violation
                        if (string.Compare(finalAnalysis[n].addressFailedGrocery, buildingData[g].buildingAddress) == 0)
                        {
                            finalAnalysis[n].violation = buildingData[g].violation;
                            break;
                        }
                        else
                        {
                            finalAnalysis[n].violation = "No Building Violation";
                        }
                    }
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

                            // Comparing the status to find only the failed records
                            if (record_match[q].status != null && string.Compare(record_match[q].status.ToUpper(), "FAIL") == 0)
                            {
                                finalAnalysis[n].storeInspectionStatus = record_match[q].status;
                                finalAnalysis[n].storeName = groceryData[k].storeName;
                                finalAnalysis[n].storeLicenceID = record_match[q].licenseID;
                                finalAnalysis[n].addressFailedGrocery = groceryData[k].groceryAddress;

                                // populating the grocery stores that have failed food inspection and have building violations
                                for (int g = 0; g < bldg_num; g++)
                                {

                                    //comparing grocery store address with building address to check for violation
                                    if (string.Compare(finalAnalysis[n].addressFailedGrocery, buildingData[g].buildingAddress) == 0)
                                    {
                                        finalAnalysis[n].violation = buildingData[g].violation;
                                        break;
                                    }
                                    else
                                    {
                                        finalAnalysis[n].violation = "No Building Violation";
                                    }
                                }
                                n++;
                            }
                        }
                    }

                    // Clearing the temporary records for next iteration
                    for (int u = 0; u < record_match.Length; u++)
                    {
                        record_match[u].date = DateTime.MinValue;
                        record_match[u].licenseID = null;
                        record_match[u].status = null;
                    }
                }


            }

            
            return finalAnalysis;
           
        }
        public void displayData(FinalAnalysis[] finalAnalysis,ref int n)
        {
            int i = 0;
            //Display records for the user by giving options about the different analysis
            string choice;
            string answer = "Y";

            Console.WriteLine("\n\n*************************** Chicago Grocery Stores Database*************************************************");

            Console.WriteLine("\n\nThe Following Database will give you information about grocery stores that have been inspected in chicago");
            Console.WriteLine("It will display the grocery stores that have failed food inspections,have building violations, and that have not been inspected for food.");
            Console.WriteLine("This database has been compiled considering the records of 2013 and shows only the FAILED records");

            while (string.Compare(answer.ToUpper(), "Y") == 0)
            {
                Console.WriteLine("\n\n******************************************MENU*********************************************************");
                Console.WriteLine("\n 1. Grocery Stores that have failed the Inpsection");
                Console.WriteLine("\n 2. Grocery Stores that have not undergone the Inpsection");
                Console.WriteLine("\n 3. Grocery Stores with failed Food Inspection and Building Violations");
                Console.WriteLine("\n 4. Grocery Stores that have not undergone the Inpsection and have Building violations");
                Console.Write("\n Enter your choice :");
                choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("Grocery Stores that have failed Food Inspection :");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("{0,-15}  {1,-40}  {2,20}  ", "License ID", "Store Name", "Inspection Status");
                    for (i = 0; i < n; i++)
                    {
                        if (string.Compare(finalAnalysis[i].storeInspectionStatus.ToUpper(), "FAIL") == 0)
                            Console.WriteLine(string.Format("{0,-15}  {1,-40}  {2,20} ", finalAnalysis[i].storeLicenceID, finalAnalysis[i].storeName, finalAnalysis[i].storeInspectionStatus));

                    }

                }

                if (choice == "2")
                {
                    Console.WriteLine("Grocery Stores that have not undergone Food Inspection :");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("{0,-15}  {1,-40}  {2,20}  ", "License ID", "Store Name", "Inspection Status");
                    for (i = 0; i < n; i++)
                    {
                        if (string.Compare(finalAnalysis[i].storeInspectionStatus.ToUpper(), "NOT INSPECTED") == 0)
                            Console.WriteLine(string.Format("{0,-15}  {1,-40}  {2,20} ", finalAnalysis[i].storeLicenceID, finalAnalysis[i].storeName, finalAnalysis[i].storeInspectionStatus));

                    }

                }

                if (choice == "3")
                {
                    Console.WriteLine("Grocery Stores that have failed Food Inspection and have building violations :");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("{0,-15}  {1,-40}  {2,20} {3,40} ", "License ID", "Store Name", "Inspection Status", " Building Violation");
                    for (i = 0; i < n; i++)
                    {
                        if ((string.Compare(finalAnalysis[i].storeInspectionStatus.ToUpper(), "FAIL") == 0) && (string.Compare(finalAnalysis[i].violation.ToUpper(), "NO BUILDING VIOLATION") != 0))
                            Console.WriteLine(string.Format("{0,-15}  {1,-40}  {2,20}  {3,40}", finalAnalysis[i].storeLicenceID, finalAnalysis[i].storeName, finalAnalysis[i].storeInspectionStatus, finalAnalysis[i].violation));
                    }

                }

                if (choice == "4")
                {
                    Console.WriteLine("Grocery Stores that have failed Food Inspection and have building violations :");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("{0,-15}  {1,-40}  {2,20}  {3,40}", "License ID", "Store Name", "Inspection Status", " Building Violation");
                    for (i = 0; i < n; i++)
                    {
                        if ((string.Compare(finalAnalysis[i].storeInspectionStatus.ToUpper(), "NOT INSPECTED") == 0) && (string.Compare(finalAnalysis[i].violation.ToUpper(), "NO BUILDING VIOLATION") == 1))
                            Console.WriteLine(string.Format("{0,-15}  {1,-40}  {2,20}  {3,40}", finalAnalysis[i].storeLicenceID, finalAnalysis[i].storeName, finalAnalysis[i].storeInspectionStatus, finalAnalysis[i].violation));
                    }

                }

                Console.Write("\n Do you wish to continue ? (Y/N) :");
                answer = Console.ReadLine();
            }


            //printing the final result of correlation
            //Console.WriteLine("Final analysis data :");
            //Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------");            
            //Console.WriteLine("{0,-15}  {1,-40}  {2,20} {3,40} ", "License ID", "Store Name",  "Inspection Status","Violation");
            //Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------");            


            //for (i = 0; i < n; i++)
            //{
            //    Console.WriteLine(string.Format("{0,-15}  {1,-40}  {2,20} {3,40}", finalAnalysis[i].storeLicenceID, finalAnalysis[i].storeName, finalAnalysis[i].storeInspectionStatus, finalAnalysis[i].violation));

            //}

        }

    }
}
