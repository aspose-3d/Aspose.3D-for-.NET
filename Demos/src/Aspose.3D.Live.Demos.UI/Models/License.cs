using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aspose.ThreeD.Live.Demos.UI.Models
{
	///<Summary>
	/// License class to set apose products license
	///</Summary>
	public static class License
	{
		private static string _licenseFileName = "Aspose.Total.lic";		
		
		///<Summary>
		/// SetAspose3dLicense method to Aspose.ThreeD License
		///</Summary>
		public static void SetAspose3dLicense()
		{
			try
			{

				Aspose.ThreeD.License lic = new Aspose.ThreeD.License();
				lic.SetLicense(_licenseFileName);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}	
		
	}
}
