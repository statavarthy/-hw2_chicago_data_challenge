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
        public string LicenseID;
    };
   

    struct FoodInsp
    {
        public string status;
        public string LicenseID;
    };

    struct analysis
    {
        public string InspectionStatus;
        public string storeName;
        public string LicenseID;
    };
   
    class Program
    {
       
        static void Main(string[] args)
        {
            Grocery[] data = new Grocery[6000];
            FoodInsp[] data1 = new FoodInsp[40000];
            analysis[] final = new analysis[20000];
            
            int i = 0;
            int gro_num = 0;
            long food_num = 0;

            var reader = new StreamReader(File.OpenRead(@"C:\Grocery_Stores_2013.csv"));

            var line0 = reader.ReadLine();
             Console.WriteLine("\n Store Name  License ID \n ");
            while (!reader.EndOfStream)
            {

                var line = reader.ReadLine();
                var values = line.Split(',');

                data[i].storeName = values[0];
                data[i].LicenseID = values[1];

                Console.WriteLine("\n {0} {1} ", data[i].storeName,data[i].LicenseID);

                i++;

            }
            gro_num = i;

            Console.WriteLine("********************************************************************");


            
            long j = 0;
            long num = 0;
            var reader1 = new StreamReader(File.OpenRead(@"C:\Food_Inspections.csv"));
            var line0_new = reader1.ReadLine();
            Console.WriteLine("\n License ID  Status \n");
            
            while (!reader1.EndOfStream)
            {
                
                var line = reader1.ReadLine();
                var values = line.Split(',');
               
                bool chk = long.TryParse(values[0], out num);
                               
               
                if(chk&&num>9999)
                {
                   
                    string type = values[4].ToUpper();
                    string name = "GROCERY";
                    bool flag = type.Contains(name);
                    string date = values[10];
                    bool timeFlag=date.Contains("2014");
                    
                    if (flag&&timeFlag)
                    {
                         
                        data1[j].LicenseID = values[3];
                        data1[j].status = values[12];
                        Console.WriteLine("\n {0} {1} ",data1[j].LicenseID,data1[j].status);
                        j++;
                    }

                }
                               
            }
            food_num = j;

            Console.Write("------------------------------------------------------------------------------------------------");
             int n=0;
            
                 

                 for (long k = 0; k < gro_num; k++)
                 {
                     bool found = false;
                     for (long m = 0; m < food_num; m++)
                     {
                         int res = string.Compare(data[k].LicenseID, "1042888");
                         if(res==0)
                         {
                             int g = 9;
                         }
                        

                         if (string.Compare(data[k].LicenseID,data1[m].LicenseID)==0)
                         {
                             found = true;
                             if (string.Compare(data1[m].status.ToUpper(), "FAIL")==0)
                             {
                                 
                                 final[n].InspectionStatus = data1[m].status;
                                 final[n].storeName = data[k].storeName;
                                 final[n].LicenseID = data1[m].LicenseID;
                                 n++;
                             }

                         }
                         
                     }
                     if(!found)
                     {
                         final[n].InspectionStatus = "NOT INSPECTED";
                         final[n].storeName = data[k].storeName;
                         final[n].LicenseID = data[k].LicenseID;
                         n++;
                     }
                     

                 }
             
            Console.Write("\n analysis data");

            for(i=0;i<n;i++)
            {
                Console.Write("\n {0} {1} {2} ", final[i].storeName, final[i].InspectionStatus, final[i].LicenseID);
            }
                
        }
    }
}
