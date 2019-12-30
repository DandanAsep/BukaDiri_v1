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
    public class PilihanController : Controller
    {
        //akses db
        BukaDiriContext db = new BukaDiriContext();
        //
        // GET: /Pilihan/
        public ActionResult Index()
        {
            return View();
        }

        //method loaddatapilihan
        public ActionResult LoadData()
        {
            List<Pilihan> pil = db.Pilihan.ToList<Pilihan>();
            return Json(new { data =  pil}, JsonRequestBehavior.AllowGet);
        }

        //method simpandatapilihan
        public ActionResult SimpanDataPilihan(Pilihan pil)
        {
            var cek = db.Pilihan.Where(b => b.kode_pilihan.Equals(pil.kode_pilihan));
            int hasil = cek.Count();//cek database, jika ada (1) maka simpan gagal

            if (hasil >= 1)
            {
                return Json(new { success = 2, count = hasil }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                db.Pilihan.Add(pil);
                db.SaveChanges();//setiap ada perubahan di db harus ada saveChanges
                return Json(new { success = 1, count = hasil }, JsonRequestBehavior.AllowGet);
            }

            //db.Pilihan.Add(pil);
            //db.SaveChanges();//setiap ada perubahan di db harus ada saveChanges
            //return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }

        //method lihat data pilihan
        public ActionResult DetailDataPilihan(string id)
        {
            Pilihan pil = db.Pilihan.Find(id);

            return Json(new { data = pil }, JsonRequestBehavior.AllowGet);
        }

        //method edit data pilihan
        public ActionResult EditDataPilihan(string id)
        {
            Pilihan pil = db.Pilihan.Find(id);

            return Json(new { data = pil }, JsonRequestBehavior.AllowGet);
        }

        //method ubah data pilihan
        public ActionResult UbahDataPilihan(Pilihan pil)
        {
          
            db.Entry(pil).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();//setiap ada perubahan di db harus ada saveChanges
            return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }

        //method hapus data pilihan
        public ActionResult HapusDataPilihan(string id)
        {
            Pilihan pil = db.Pilihan.Find(id);
            db.Pilihan.Remove(pil);
            db.SaveChanges();
            return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }

        //Export pdf pilihan
        public ActionResult ExportReportPilihan()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "PilihanReport.rpt"));
            rd.SetDataSource(db.Pilihan.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Pilihan.pdf");
            }
            catch
            {
                throw;
            }

        }
	}
}