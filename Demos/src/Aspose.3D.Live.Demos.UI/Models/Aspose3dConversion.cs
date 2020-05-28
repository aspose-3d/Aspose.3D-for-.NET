
using System.Threading.Tasks;
using System.IO;
using System.Web.Http;
using Aspose.ThreeD.Live.Demos.UI.Models;
using Aspose.ThreeD;
using Aspose.ThreeD.Formats;
using System.Diagnostics;

namespace Aspose.ThreeD.Live.Demos.UI.Models
{
	///<Summary>
	/// Aspose 3d Conversion App methods to convert 3d file to other format
	///</Summary>
	
	public class Aspose3dConversion : ThreeDBase
    {        
        private Response ProcessTask(string fileName, string folderName, string outFileExtension, bool createZip,  bool checkNumberofPages, ActionDelegate action)
        {
            License.SetAspose3dLicense();            			
            return  Process(this.GetType().Name, fileName, folderName, outFileExtension, createZip, checkNumberofPages,  (new StackTrace()).GetFrame(5).GetMethod().Name, action);

		}
		///<Summary>
		/// Convert3dToFormat method to convert 3d to other format
		///</Summary>
		public Response Convert3dToFormat(string fileName, string folderName, string outputType)
        {
			SaveOptions saveOptions = null;
			bool foundSaveOption = true;
			bool createZip = false;

			switch (outputType)
			{
				case "fbx":
					saveOptions = new FBXSaveOptions(Aspose.ThreeD.FileFormat.FBX7500Binary);
					break;
				case "obj":
					saveOptions = new ObjSaveOptions();
					break;
				case "3ds":
					saveOptions = new Discreet3DSSaveOptions();
					break;
				case "drc":
					saveOptions = new DracoSaveOptions();
					break;
				
				case "amf":
					saveOptions = new AMFSaveOptions();
					break;
				case "rvm":
					saveOptions = new RvmSaveOptions();
					break;
				case "dae":
					saveOptions = new ColladaSaveOptions();
					break;
				case "gltf":
					saveOptions = new GLTFSaveOptions(FileContentType.ASCII);
					createZip = true;
					break;
				case "glb":
					saveOptions = new GLTFSaveOptions(FileContentType.Binary);
					break;
				case "pdf":
					saveOptions = new PdfSaveOptions();
					break;
				case "html":
					saveOptions = new HTML5SaveOptions();
					createZip = true;
					break;
				case "ply":
					saveOptions = new PlySaveOptions();
					break;
				case "stl":
					saveOptions = new STLSaveOptions();
					break;
				case "u3d":
					saveOptions = new U3DSaveOptions();
					break;
				case "att":
					RvmSaveOptions att = new RvmSaveOptions();
					att.ExportAttributes = true;
					saveOptions = att;
					break;
				default:
					foundSaveOption = false;
					break;
			}

			if(foundSaveOption)
			{
				return  ProcessTask(fileName, folderName, "." + outputType, createZip, false, delegate (string inFilePath, string outPath, string zipOutFolder)
				{
					Scene scene = new Scene(inFilePath);
					scene.Save(outPath, saveOptions);
				});
			}
			else
			{
				return new Response
				{
					FileName = null,
					Status = "Output type not found",
					StatusCode = 500
				};
			}
        }
		///<Summary>
		/// ConvertFile
		///</Summary>
		public Response ConvertFile(string fileName, string folderName, string outputType)
        {
            
            outputType = outputType.ToLower();

			string strOutputTypes = "START,fbx,obj,3ds,drc,amf,rvm,dae,gltf,glb,pdf,html,ply,stl,u3d,att,END";


			if (strOutputTypes.IndexOf(","+outputType+ ",")>0)
            {
                return  Convert3dToFormat(fileName, folderName, outputType);
            }            

            return new Response
            {
                FileName = null,
                Status = "Output type not found",
                StatusCode = 500
            };
        }                
    }
}
