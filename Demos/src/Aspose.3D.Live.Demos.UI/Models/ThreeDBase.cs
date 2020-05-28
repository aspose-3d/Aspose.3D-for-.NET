using System;
using System.IO;
using System.Web.Http;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Drawing;
using System.Drawing.Imaging;

namespace  Aspose.ThreeD.Live.Demos.UI.Models
{
	///<Summary>
	/// ApiControllerBase class to have base methods
	///</Summary>

	public abstract class ThreeDBase : ApiController
    {
		///<Summary>
		/// ActionDelegate
		///</Summary>
		protected delegate void ActionDelegate(string inFilePath, string outPath, string zipOutFolder);
		///<Summary>
		/// inFileActionDelegate
		///</Summary>
		protected delegate void inFileActionDelegate(string inFilePath);
		///<Summary>
		/// Get File extension
		///</Summary>
		protected string GetoutFileExtension(string fileName, string folderName)
        {
			string sourceFolder = Config.Configuration.WorkingDirectory + folderName;
            fileName = sourceFolder + "\\" + fileName;
            return Path.GetExtension(fileName);
        }
		
        protected Response Process(string modelName, string fileName, string folderName, string outFileExtension, bool createZip, bool checkNumberofPages,  string methodName, ActionDelegate action,
      bool deleteSourceFolder = true, string zipFileName = null)
        {
            
            string guid = Guid.NewGuid().ToString();
            string outFolder = "";
			string sourceFolder = Config.Configuration.WorkingDirectory + folderName;			
            fileName = sourceFolder + "\\" + fileName;

            string fileExtension = Path.GetExtension(fileName).ToLower();
           
			string outfileName = Path.GetFileNameWithoutExtension(fileName) + outFileExtension;
            string outPath = "";

			string zipOutFolder = Config.Configuration.OutputDirectory + guid;
            string zipOutfileName, zipOutPath;
            if (string.IsNullOrEmpty(zipFileName))
            {
                zipOutfileName = guid + ".zip";
				zipOutPath = Config.Configuration.OutputDirectory + zipOutfileName;
            }
            else
            {
                var guid2 = Guid.NewGuid().ToString();
                outFolder = guid2;
                zipOutfileName = zipFileName + ".zip";
				zipOutPath = Config.Configuration.OutputDirectory + guid2;
                Directory.CreateDirectory(zipOutPath);
                zipOutPath += "/" + zipOutfileName;
            }

            if (createZip)
            {
                outfileName = Path.GetFileNameWithoutExtension(fileName) + outFileExtension;
                outPath = zipOutFolder + "/" + outfileName;
                Directory.CreateDirectory(zipOutFolder);
            }
            else
            {
                outFolder = guid;
				outPath = Config.Configuration.OutputDirectory +  outFolder;
                Directory.CreateDirectory(outPath);

                outPath += "/" + outfileName;
            }

            string statusValue = "OK";
            int statusCodeValue = 200;

            try
            {
                action(fileName, outPath, zipOutFolder);

                if (createZip)
                {
                    ZipFile.CreateFromDirectory(zipOutFolder, zipOutPath);
                    Directory.Delete(zipOutFolder, true);
                    outfileName = zipOutfileName;
                }

				if (deleteSourceFolder)
				{
					System.GC.Collect();
					System.GC.WaitForPendingFinalizers();
					Directory.Delete(sourceFolder, true);
				}

            }
            catch (Exception ex)
            {
                statusCodeValue = 500;
                statusValue = "500 " + ex.Message;
               
            }
            return new Response
            {
				FileName = outfileName,
				FolderName = outFolder,
				Status = statusValue,
				StatusCode = statusCodeValue,
			};
        }
		///<Summary>
		/// Process
		///</Summary>
		/// <param name="controllerName"></param>
		/// <param name="fileName"></param>
		/// <param name="folderName"></param>
		/// <param name="productName"></param>
		/// <param name="productFamily"></param>
		/// <param name="methodName"></param>
		/// <param name="action"></param>
		protected async Task<Response> Process(string controllerName, string fileName, string folderName, string productName, string productFamily, string methodName, inFileActionDelegate action)
        {           
            string tempFileName = fileName;
			string sourceFolder = Config.Configuration.WorkingDirectory + folderName;
            fileName = sourceFolder + "/" + fileName;

            string statusValue = "OK";
            int statusCodeValue = 200;

            try
            {
                action(fileName);

                //Directory.Delete(sourceFolder, true);                

            }
            catch (Exception ex)
            {
                statusCodeValue = 500;
                statusValue = "500 " + ex.Message;
               
            }
            return await Task.FromResult(new Response
            {
                Status = statusValue,
                StatusCode = statusCodeValue,
            });
        }
		
    }
}
