#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

#endregion

namespace UpdateFamilies
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            // set variables for folder paths

            string pathElectrical = @"S:\Shared Folders\Lifestyle USA Design\Library 2023\Electrical";
            string pathKitchen = @"S:\Shared Folders\Lifestyle USA Design\Library 2023\Kitchen";
            string pathLighting = @"S:\Shared Folders\Lifestyle USA Design\Library 2023\Lighting";
            string pathShelving = @"S:\Shared Folders\Lifestyle USA Design\Library 2023\Shelf";

            // create lists for families to update

            List<string> famListElectrical = new List<string> { "EL-No Base.rfa", "EL-Wall Base.rfa" };
            List<string> famListKitchen = new List<string> { "--Kitchen Counter--.rfa" };
            List<string> famListLighting = new List<string> { "LT-No Base.rfa" };
            List<string> famListShelving = new List<string> { "Rod and Shelf.rfa", "Shelving.rfa" };

            FamilyLoadOptions familyLoadOptions = new FamilyLoadOptions();

            Family family = null;

            using (Transaction t = new Transaction(doc))
            {
                t.Start("Update Families");

                foreach (string curFamString in famListElectrical)
                {
                    string famPath = (pathElectrical + @"\" + curFamString);

                    //load the family

                    doc.LoadFamily(famPath, familyLoadOptions, out family);
                }

                foreach (string curFamString in famListKitchen)
                {
                    string famPath = (pathKitchen + @"\" + curFamString);

                    // load the family

                    doc.LoadFamily(famPath, familyLoadOptions, out family);
                }

                foreach (string curFamString in famListLighting)
                {
                    string famPath = (pathLighting + @"\" + curFamString);

                    // load the family

                    doc.LoadFamily(famPath, familyLoadOptions, out family);
                }

                foreach (string curFamString in famListShelving)
                {
                    string famPath = (pathShelving + @"\" + curFamString);

                    // load the family

                    doc.LoadFamily(famPath, familyLoadOptions, out family);
                }

                t.Commit();

            }
    
            return Result.Succeeded;
        }

        public static String GetMethod()
        {
            var method = MethodBase.GetCurrentMethod().DeclaringType?.FullName;
            return method;
        }

        public class FamilyLoadOptions : IFamilyLoadOptions
        {
            #region IFamilyLoadOptions Members

            public bool OnFamilyFound(bool familyInUse, out bool overwriteParameterValues)
            {
                if (!familyInUse)
                {
                    //TaskDialog.Show("SampleFamilyLoadOptions", "The family has not been in use and will keep loading.");

                    overwriteParameterValues = true;
                    return true;
                }
                else
                {
                    //TaskDialog.Show("SampleFamilyLoadOptions", "The family has been in use but will still be loaded with existing parameters overwritten.");

                    overwriteParameterValues = true;
                    return true;
                }
            }

            public bool OnSharedFamilyFound(Family sharedFamily, bool familyInUse, out FamilySource source, out bool overwriteParameterValues)
            {
                if (!familyInUse)
                {
                    //TaskDialog.Show("SampleFamilyLoadOptions", "The shared family has not been in use and will keep loading.");

                    source = FamilySource.Family;
                    overwriteParameterValues = true;
                    return true;
                }
                else
                {
                    //TaskDialog.Show("SampleFamilyLoadOptions", "The shared family has been in use but will still be loaded from the FamilySource with existing parameters overwritten.");

                    source = FamilySource.Family;
                    overwriteParameterValues = true;
                    return true;
                }
            }

            #endregion
        }
    }
}
