using BukaDiri.Context;
using BukaDiri.Models;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BukaDiri.Controllers
{
    public class LapakController : Controller
    {
        //akses db
        BukaDiriContext db = new BukaDiriContext();
        //
        // GET: /Lapak/
        public ActionResult Index()
        {
            return View();
        }

        //method loaddataLapak
        public ActionResult LoadData()
        {
            List<Lapak> lapak = db.Lapak.ToList<Lapak>();
            return Json(new { data = lapak }, JsonRequestBehavior.AllowGet);
        }

        //method simpandataLapak
        public ActionResult SimpanDataLapak(Lapak lapak)
        {
            var cek = db.Lapak.Where(b => b.kode_lapak.Equals(lapak.kode_lapak));
            int hasil = cek.Count();//cek database, jika ada (1) maka simpan gagal

            if (hasil >= 1)
            {
                return Json(new { success = 2, count = hasil }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                db.Lapak.Add(lapak);
                db.SaveChanges();//setiap ada perubahan di db harus ada saveChanges
                return Json(new { success = 1, count = hasil }, JsonRequestBehavior.AllowGet);
            }

            //db.Lapak.Add(lapak);
            //db.SaveChanges();//setiap ada perubahan di db harus ada saveChanges
            //return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }

        //method lihat data Lapak
        public ActionResult DetailDataLapak(string id)
        {
            Lapak lapak = db.Lapak.Find(id);

            return Json(new { data = lapak }, JsonRequestBehavior.AllowGet);
        }

        //method edit data Lapak
        public ActionResult EditDataLapak(string id)
        {
            Lapak lapak = db.Lapak.Find(id);

            return Json(new { data = lapak }, JsonRequestBehavior.AllowGet);
        }

        //method ubah data Lapak
        public ActionResult UbahDataLapak(Lapak lapak)
        {

            db.Entry(lapak).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();//setiap ada perubahan di db harus ada saveChanges
            return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }

        //method hapus data Lapak
        public ActionResult HapusDataLapak(string id)
        {
            Lapak lapak = db.Lapak.Find(id);
            db.Lapak.Remove(lapak);
            db.SaveChanges();
            return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }

        //export lapak pdf
        public ActionResult ExportReportLapak()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "LapakReport.rpt"));
            rd.SetDataSource(db.Lapak.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Lapak.pdf");
            }
            catch
            {
                throw;
            }

        }
	}
}