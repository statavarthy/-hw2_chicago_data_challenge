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
 * 
 * 
 * ver 2.1 : 15 Jun 2015
 * -Added Display module with user interface
 * 
 * ver 2.0 : 15 Jun 2015
 * - Printed the actual violations that were found for Failed and Not Inspected grocery stores
 * 
 * ver 1.9 : 15 Jun 2015
 * - Found Building violations for grocery stores that have not been inspected
 * - Printed the result on console
 * 
 * ver 1.8 : 15 Jun 2015
 * - Found correlation between grocery stores failed in inspection and have building violation
 * - Printed the result on console
 * 
 * ver 1.7 : 14 Jun 2015
 * -Fixed errors in correlation testing. Output now shows correct results
 * -Formatted the output display in tabular format
 * 
 * ver 1.6 : 14 Jun 2015
 * -Fixed issues while testing with original datasets. Fixed issues related to out of bound Exceptions.
 * 
 * ver 1.5 : 14 Jun 2015
 * - Fixed the logic for correlating grocery stores with food inspections
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

    public class HW2DataChallenge
    {
        
        static void Main(string[] args)
        {
            string filePath = "..\\..\\data\\" + "actualData" + "\\";
            

            
            ParseData pd = new ParseData();
            Hw2.ParseData.Grocery[] groceryData = pd.ParseGrocery(filePath+"Grocery_Stores_2013.csv");
            Hw2.ParseData.FoodInspection[] foodInspectionData = pd.ParseFoodInspection(filePath+"Food_Inspections_2013.csv");
            Hw2.ParseData.BuildingInspection[] buildingInspectionData = pd.ParseBuildingInspection(filePath + "Building_Violations.csv");
            Hw2.ParseData.FinalAnalysis[] finalAnalysis=pd.AnalysisGroceryFood(groceryData, foodInspectionData, buildingInspectionData);
            pd.displayData(finalAnalysis);
            
        }
    }
}
