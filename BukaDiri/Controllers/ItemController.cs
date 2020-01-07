using BukaDiri.Context;
using BukaDiri.Models;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BukaDiri.Controllers
{
    public class ItemController : Controller
    {   
        //akses db
        BukaDiriContext db = new BukaDiriContext();
        //
        // GET: /Item/
        public ActionResult Index()
        {
            return View();
        }

        //method loaddataItem
        public ActionResult LoadData()
        {
            List<Item> Item = db.Item.ToList<Item>();
            return Json(new { data = Item }, JsonRequestBehavior.AllowGet);
        }

        //method simpandataItem
        public ActionResult SimpanDataItem(Item Item)
        {
            var cek = db.Item.Where(b => b.kode_item.Equals(Item.kode_item));
            int hasil = cek.Count();//cek database, jika ada (1) maka simpan gagal

            if (hasil >= 1)
            {
                return Json(new { success = 2, count = hasil }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                db.Item.Add(Item);
                db.SaveChanges();//setiap ada perubahan di db harus ada saveChanges
                return Json(new { success = 1, count = hasil }, JsonRequestBehavior.AllowGet);
            }

            //db.Item.Add(Item);
            //db.SaveChanges();//setiap ada perubahan di db harus ada saveChanges
            //return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }

        //method lihat data Item
        public ActionResult DetailDataItem(string id)
        {
            //Item Item = db.Item.Find(id);

            var Item =
                    from a in db.Item
                    join b in db.Provinsi on a.kode_provinsi equals b.kode_provinsi
                    join c in db.Pilihan on a.kode_pilihan equals c.kode_pilihan
                    join d in db.Lapak on a.kode_lapak equals d.kode_lapak
                    select new { KodeItem = a.kode_item, NamaItem = a.nama_item, HargaItem = a.harga_item, NamaProv = b.nama_provinsi, NamaPilihan = c.nama_pilihan, NamaLapak = d.nama_lapak}; //produces flat sequence

            return Json(new { data = Item }, JsonRequestBehavior.AllowGet);
        }

        //method edit data Item
        public ActionResult EditDataItem(string id)
        {
            Item Item = db.Item.Find(id);

            return Json(new { data = Item }, JsonRequestBehavior.AllowGet);
        }

        //method ubah data Item
        public ActionResult UbahDataItem(Item Item)
        {

            db.Entry(Item).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();//setiap ada perubahan di db harus ada saveChanges
            return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }

        //method hapus data Item
        public ActionResult HapusDataItem(string id)
        {
            Item Item = db.Item.Find(id);
            db.Item.Remove(Item);
            db.SaveChanges();
            return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportReportItem()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "ItemReport.rpt"));

            ConnectionInfo connectInfo = new ConnectionInfo()
            {
                ServerName = "(local)\\sqlexpress",
                DatabaseName = "BukaDiri",
                UserID = "sa",
                Password = "12345"
            };
            rd.SetDatabaseLogon("sa", "12345");
            foreach (Table tbl in rd.Database.Tables)
            {
                tbl.LogOnInfo.ConnectionInfo = connectInfo;
                tbl.ApplyLogOnInfo(tbl.LogOnInfo);
            }

            //rd.SetDataSource(db.Item.ToList());

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Item.pdf");
            }
            catch
            {
                throw;
            }

        }
	}
}