using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TibbiTakip.Models;

namespace TibbiTakip.Controllers
{
    public class IlacController : Controller
    {
        // GET: Ilac
        // GET: Hastalik
        TakipSistemiEntities5 db = new TakipSistemiEntities5();

        [Authorize]
        public ActionResult Index(string ara)
        {
            var list = db.IlacBilgisi.ToList();
            if (!string.IsNullOrEmpty(ara))
            {
                list = list.Where(x => x.TCKimlikNo.Contains(ara) || x.Kullanicilar.Ad.Contains(ara) || x.IlacAdi.Contains(ara)).ToList();

            }
            return View(list);
        }

        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(IlacBilgisi Data, HttpPostedFileBase File)
        {
            //string path-Path.Combine(~/Content/Image" + File.FileName);
            //File.SaveAs(Server MapPath(path));
            //Data.Resim - File.FileName.ToString();
            db.IlacBilgisi.Add(Data);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Sil(int id)
        {
            var ilac = db.IlacBilgisi.Where(x => x.IlacID == id).FirstOrDefault();
            db.IlacBilgisi.Remove(ilac);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Guncelle(int id)
        {
            var guncelle = db.IlacBilgisi.Where(x => x.IlacID == id).FirstOrDefault();
            return View(guncelle);
        }
        [HttpPost]
        public ActionResult Guncelle(IlacBilgisi model, HttpPostedFileBase file)
        {
            var ilac = db.IlacBilgisi.Find(model.IlacID);
            ilac.IlacID = model.IlacID;
            ilac.TCKimlikNo = model.TCKimlikNo;
            ilac.IlacAdi = model.IlacAdi;
            ilac.Dozaj = model.Dozaj;
            ilac.Frekans = model.Frekans;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}