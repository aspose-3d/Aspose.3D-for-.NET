using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aspose.ThreeD.Live.Demos.UI.Models.Common
{
	public class InputFile
	{
		private string _fileName = string.Empty;
		private string _folderName = string.Empty;
		


		public InputFile(string fileName, string folderName)
		{
			_fileName = fileName;
			_folderName = folderName;
		}
		
		public string FileName
		{
			get { return _fileName; }
			
		}
		public string FolderName
		{
			get { return _folderName; }
			
		}
		
	}

	public class InputFiles : List<InputFile>
	{

	}
}