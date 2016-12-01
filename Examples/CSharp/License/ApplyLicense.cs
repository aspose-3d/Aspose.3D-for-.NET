using System;
using System.IO;
namespace Aspose._3D.Examples.CSharp.License
{
    class ApplyLicense
    {
        public static void UsingFile()
        {
            // ExStart:ApplyLicenseUsingFile
            Aspose.ThreeD.License license = new Aspose.ThreeD.License();
            license.SetLicense("Aspose._3D.lic");
            // ExEnd:ApplyLicenseUsingFile
        }
        public static void UsingStreamObject()
        {
            // ExStart:ApplyLicenseUsingStreamObject
            Aspose.ThreeD.License license = new Aspose.ThreeD.License();
            FileStream myStream = new FileStream("Aspose._3D.lic", FileMode.Open);
            license.SetLicense(myStream);
            // ExEnd:ApplyLicenseUsingStreamObject
        }
        public static void UsingEmbeddedResource()
        {
            // ExStart:ApplyLicenseUsingEmbeddedResource
            // Instantiate the License class
            Aspose.ThreeD.License license = new Aspose.ThreeD.License();

            // Pass only the name of the license file embedded in the assembly
            license.SetLicense("Aspose._3D.lic");
            // ExEnd:ApplyLicenseUsingEmbeddedResource
        }
    }
}
