using Aspose.ThreeD.Live.Demos.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aspose.ThreeD.Live.Demos.UI.Controllers
{
	public class HomeController : BaseController
	{
	
		public override string Product => (string)RouteData.Values["productname"];
		

		

		public ActionResult Default()
		{
			ViewBag.PageTitle = "Aspose.3D Live Demos - Free App Solutions for 3D files manipulation";
			ViewBag.MetaDescription = "Free online App to Convert 3D files including FBX, STL, OBJ, 3DS, GLTF, DRC, RVM, PDF, X, JT, DXF, PLY, 3MF &amp; ASE. Transform them to FBX, OBJ, 3DS, DRC formats.";
			var model = new LandingPageModel(this);

			return View(model);
		}
	}
}
