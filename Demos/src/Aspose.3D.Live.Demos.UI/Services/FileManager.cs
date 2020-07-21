using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.IO;
using System.Diagnostics;
using System.Net.Http.Headers;
using Aspose.ThreeD.Live.Demos.UI.Config;
using Aspose.ThreeD.Live.Demos.UI.Models;
using System.Net;
using System.Web.Http;

namespace Aspose.ThreeD.Live.Demos.UI.Services
{
    public static class FileManager
    {
		
		

		public static FileUploadResult UploadFile( System.Web.HttpPostedFileBase postedFile)
		{
			FileUploadResult uploadResult = null;
			string fn = "";
			try
			{
				string folderName = Guid.NewGuid().ToString();

				// Prepare a path in which the result file will be
				string uploadPath = Configuration.WorkingDirectory + "\\" + folderName;

				// Check directroy already exist or not
				if (!Directory.Exists(uploadPath))
					Directory.CreateDirectory(uploadPath);

				// Check if File is available.
				if (postedFile != null && postedFile.ContentLength > 0)
				{
					fn = System.IO.Path.GetFileName(postedFile.FileName);

					postedFile.SaveAs(uploadPath + "\\" + fn);
				}

				// Create response
				return new FileUploadResult
				{
					FileName = fn,
					FolderId = folderName
				};
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
			return uploadResult;
		}
		public static FileUploadResult UploadFile(System.Web.UI.HtmlControls.HtmlInputFile UploadFile)
		{
			FileUploadResult uploadResult = null;
			string fn = "";
			try
			{
				string folderName = Guid.NewGuid().ToString();

				// Prepare a path in which the result file will be
				string uploadPath = Configuration.WorkingDirectory + "\\" + folderName;

				// Check directroy already exist or not
				if (!Directory.Exists(uploadPath))
					Directory.CreateDirectory(uploadPath);

				// Check if File is available.
				if (UploadFile.PostedFile != null && UploadFile.PostedFile.ContentLength > 0)
				{
					 fn = System.IO.Path.GetFileName(UploadFile.PostedFile.FileName);

					UploadFile.PostedFile.SaveAs(uploadPath + "\\" + fn);
				}

					// Create response
					return new FileUploadResult
				{
					FileName = fn,					
					FolderId = folderName
				};
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
			return uploadResult;
		}
		
       
		
	}
}
