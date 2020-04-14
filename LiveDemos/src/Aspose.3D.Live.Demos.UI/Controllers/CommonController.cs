using Aspose.ThreeD.Live.Demos.UI.Models;
using Aspose.ThreeD.Live.Demos.UI.Services;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Aspose.ThreeD.Live.Demos.UI.Controllers
{
	/// <summary>
	/// Common  API controller.
	/// </summary>
	public  class CommonController : BaseController
	{
		public override string Product => (string)RouteData.Values["product"];
		/// <summary>
		/// Sends back specified file from specified folder inside OutputDirectory.
		/// </summary>
		/// <param name="folder">Folder inside OutputDirectory.</param>
		/// <param name="file">File.</param>
		/// <returns>HTTP response with file.</returns>


		public FileResult DownloadFile(string file, string folder)
		{
			var pathProcessor = new PathProcessor(folder, file: file);
			
			return File(pathProcessor.DefaultOutFile, "application/octet-stream", file);
		}
		
		
	
	
		

		
	}
}
