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

    class Program
    {
        static void Main(string[] args)
        {
            Grocery[] groceryData = new Grocery[6000];
            FoodInspection[] foodInspectionData = new FoodInspection[40000];
            FinalAnalysis[] finalAnalysis = new FinalAnalysis[20000];
            BuildingInspection[] buildingData = new BuildingInspection[200000];
            TempRecordMatch[] record_match = new TempRecordMatch[10];

            int i = 0;
            int gro_num = 0;
            long food_num = 0;
            string dataFolderName = "";

            bool testMode = false;

            if (testMode)
                dataFolderName = "testData";
            else
                dataFolderName = "actualData";

            string filePath = "..\\..\\data\\" + dataFolderName + "\\";

            var reader = new StreamReader(File.OpenRead(@filePath + "Grocery_Stores_2013.csv"));
            var line0 = reader.ReadLine();
            Console.WriteLine("\n Store Name  License ID \n ");
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                groceryData[i].storeName = values[0];
                groceryData[i].licenseID = values[1];
                Console.WriteLine("\n {0} {1} ", groceryData[i].storeName, groceryData[i].licenseID);
                i++;
            }
            gro_num = i;

            Console.WriteLine("********************************************************************");
            
            long j = 0;
            long num = 0;
            var reader1 = new StreamReader(File.OpenRead(@filePath + "Food_Inspections_2014.csv"));
            var line0_new = reader1.ReadLine();
            Console.WriteLine("\n License ID  Status \n");

            while (!reader1.EndOfStream)
            {
                var line = reader1.ReadLine();
                var values = line.Split(',');

                bool chk = long.TryParse(values[0], out num);
                
                if (chk && num > 9999)
                {
                    string type = values[4].ToUpper();
                    string name = "GROCERY";
                    bool flag = type.Contains(name);
                    string date = values[10];
                    bool timeFlag = date.Contains("2013");

                    if (flag && timeFlag)
                    {

                        foodInspectionData[j].storeLicenseID = values[3];
                        foodInspectionData[j].foodInspectionStatus = values[12];
                        foodInspectionData[j].inspectionDate = values[10];
                        Console.WriteLine("\n {0}  {1} {2} ", foodInspectionData[j].storeLicenseID, foodInspectionData[j].foodInspectionStatus, foodInspectionData[j].inspectionDate);
                        j++;
                    }

                }

            }
            food_num = j;

            long x = 0;
            long bldg_num = 0;
            long num1 = 0;
            var reader3 = new StreamReader(File.OpenRead(@filePath + "Building_Violations.csv"));

            var line_first = reader3.ReadLine();
            Console.WriteLine("\n Inspection Status  Address \n ");
                        
            while (!reader3.EndOfStream)
            {

                var line = reader3.ReadLine();
                var values = line.Split(new string[] { "#@" }, StringSplitOptions.None);
                bool linechk = long.TryParse(values[0], out num1);

                buildingData[x].buildingID = values[0];
                buildingData[x].buildingAddress = values[16];

                //Console.WriteLine("\n {0}  {1} ", bldg[x].ID, bldg[x].Address);

                x++;
            }
            bldg_num = x;

            int n = 0;
            int c = 0;
            long m = 0;
            DateTime maxDate = DateTime.MinValue;

            for (long k = 0; k < gro_num; k++)
            {
                bool isInspcted = false;
                for (m = 0; m < food_num; m++)
                {
                    isInspcted = true;
                    if (string.Compare(groceryData[k].licenseID, foodInspectionData[m].storeLicenseID) == 0)
                    {
                        record_match[c].licenseID = groceryData[k].licenseID;
                        record_match[c].date = Convert.ToDateTime(foodInspectionData[m].inspectionDate);
                        record_match[c].status = foodInspectionData[m].foodInspectionStatus;
                    }
                    c++;
                }
                
                foreach (var r in record_match)
                {
                    if (maxDate < r.date)
                    {
                        maxDate = r.date;
                    }
                    //Console.WriteLine("\n MAX DATE IS === {0} ", maxDate);
                }

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
                }

                if (!isInspcted)
                {
                    finalAnalysis[n].storeInspectionStatus = "NOT INSPECTED";
                    finalAnalysis[n].storeName = groceryData[k].storeName;
                    finalAnalysis[n].storeLicenceID = groceryData[k].licenseID;
                    n++;
                }
            }

            Console.Write("\n analysis data");

            for (i = 0; i < n; i++)
            {
                Console.Write("\n {0} {1} {2} ", finalAnalysis[i].storeName, finalAnalysis[i].storeInspectionStatus, finalAnalysis[i].storeLicenceID);
            }

        }
    }
}
