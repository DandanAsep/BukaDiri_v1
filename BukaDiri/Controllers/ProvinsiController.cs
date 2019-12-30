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
    public class ProvinsiController : Controller
    {
        //
        // GET: /Provinsi/
        BukaDiriContext db = new BukaDiriContext();//akses db

        public ActionResult Index()
        {
            return View();
        }

        //method loaddataprovinsi
        public ActionResult LoadData()
        {
            List<Provinsi> prov = db.Provinsi.ToList<Provinsi>();
            return Json(new { data = prov }, JsonRequestBehavior.AllowGet);
        }
        
        //method simpandataprovinsi
        public ActionResult SimpanDataProv(Provinsi prov)
        {
            var cek = db.Provinsi.Where(b => b.kode_provinsi.Equals(prov.kode_provinsi));
            int hasil = cek.Count();//cek database, jika ada (1) maka simpan gagal

            if (hasil >= 1)
            {
                return Json(new { success = 2, count = hasil }, JsonRequestBehavior.AllowGet);
            }
            else {
                db.Provinsi.Add(prov);
                db.SaveChanges();//setiap ada perubahan di db harus ada saveChanges
                return Json(new { success = 1, count = hasil }, JsonRequestBehavior.AllowGet);
            }

            //db.Provinsi.Add(prov);
            //db.SaveChanges();//setiap ada perubahan di db harus ada saveChanges
            //return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }

        //method lihat data provinsi
        public ActionResult DetailDataProv(string id)
        {
            Provinsi prov = db.Provinsi.Find(id);

            return Json(new { data = prov }, JsonRequestBehavior.AllowGet);
        }

        //method edit data provinsi
        public ActionResult EditDataProv(string id)
        {
            Provinsi prov = db.Provinsi.Find(id);

            return Json(new { data = prov }, JsonRequestBehavior.AllowGet);
        }

        //method ubah data provinsi
        public ActionResult UbahDataProv(Provinsi prov)
        {
            //db.Biodata.Add(bio);
            db.Entry(prov).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();//setiap ada perubahan di db harus ada saveChanges
            return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }

        //method hapus data provinsi
        public ActionResult HapusDataProv(string id)
        {
            Provinsi prov = db.Provinsi.Find(id);
            db.Provinsi.Remove(prov);
            db.SaveChanges();
            return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportReportProv()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "ProvReport.rpt"));
            rd.SetDataSource(db.Provinsi.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Provinsi.pdf");
            }
            catch
            {
                throw;
            }
            
        }
	}
}