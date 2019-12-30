using BukaDiri.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BukaDiri.Controllers
{
    public class HomeController : Controller
    {
        //akses db
        BukaDiriContext db = new BukaDiriContext();
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        //method loaddataItem
        public ActionResult LoadData()
        {
            var DataHome =
                   from a in db.Item
                   join b in db.Provinsi on a.kode_provinsi equals b.kode_provinsi
                   join c in db.Pilihan on a.kode_pilihan equals c.kode_pilihan
                   join d in db.Lapak on a.kode_lapak equals d.kode_lapak
                   select new { KodeItem = a.kode_item, NamaItem = a.nama_item, HargaItem = a.harga_item, NamaProv = b.nama_provinsi, NamaPilihan = c.nama_pilihan, NamaLapak = d.nama_lapak }; //produces flat sequence

            return Json(new { data = DataHome }, JsonRequestBehavior.AllowGet);
        }
	}
}