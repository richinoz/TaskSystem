using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ionic.Zip;
using TaskSystem.Helpers;

namespace TaskSystem.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Task System";

            return View();
        }


        public ActionResult About()
        {
            return View();
        }

        public ActionResult Download()
        {

            string file = Server.MapPath("~/Assets/" + "test.jpg");
            if (System.IO.File.Exists(file))
            {
                var zipFile = Zip(file);
                return File(zipFile, "zip", "downloadTest.zip");
            }
            else
                return RedirectToAction("Download");
        }

        private string Zip(string path)
        {
            string dest = Server.MapPath("~/Assets/" + "downLoad.zip");


            using (var zip = new ZipFile())
            {
                // add this map file into the "images" directory in the zip archive
                zip.AddFile(path, "images");
                zip.AddFile(path, "images2");
                // add the report into a different directory in the archive
                // zip.AddFile("c:\\Reports\\2008-Regional-Sales-Report.pdf", "files");
                zip.AddFile(path);
                zip.Save(dest);
            }

            return dest;
        }
    }
}
