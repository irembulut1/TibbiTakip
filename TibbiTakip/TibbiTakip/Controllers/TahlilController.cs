using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TibbiTakip.Models;

namespace TibbiTakip.Controllers
{
    public class TahlilController : Controller
    {
        TakipSistemiEntities5 db = new TakipSistemiEntities5();

        [Authorize]
        public ActionResult Index(string ara)
        {
            var list = db.TahlilBilgisi.ToList();
            if (!string.IsNullOrEmpty(ara))
            {
                list = list.Where(x => x.TCKimlikNo.Contains(ara) || x.Kullanicilar.Ad.Contains(ara) || x.TahlilAdi.Contains(ara)).ToList();

            }
            return View(list);
        }

        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(TahlilBilgisi Data, HttpPostedFileBase File)
        {
            //string path-Path.Combine(~/Content/Image" + File.FileName);
            //File.SaveAs(Server MapPath(path));
            //Data.Resim - File.FileName.ToString();
            db.TahlilBilgisi.Add(Data);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Sil(int id)
        {
            var tahlil = db.TahlilBilgisi.Where(x => x.TahlilID == id).FirstOrDefault();
            db.TahlilBilgisi.Remove(tahlil);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Guncelle(int id)
        {
            var guncelle = db.TahlilBilgisi.Where(x => x.TahlilID == id).FirstOrDefault();
            return View(guncelle);
        }
        [HttpPost]
        public ActionResult Guncelle(TahlilBilgisi model, HttpPostedFileBase file)
        {
            var hastalik = db.TahlilBilgisi.Find(model.TahlilID);
            hastalik.TahlilID = model.TahlilID;
            hastalik.TCKimlikNo = model.TCKimlikNo;
            hastalik.TahlilAdi = model.TahlilAdi;
            hastalik.TahlilTarihi = model.TahlilTarihi;
            hastalik.Sonuc = model.Sonuc;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}