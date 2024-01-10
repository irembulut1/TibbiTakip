using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TibbiTakip.Models;

namespace TibbiTakip.Controllers
{
    public class HastalikController : Controller
    {
        // GET: Hastalik
        TakipSistemiEntities5 db = new TakipSistemiEntities5();

        [Authorize] 
        public ActionResult Index(string ara)
        {
            var list=db.HastalikBilgisi.ToList();
            if(!string.IsNullOrEmpty(ara))
            {
                list = list.Where(x => x.TCKimlikNo.Contains(ara)|| x.Kullanicilar.Ad.Contains(ara)|| x.HastalikAdi.Contains(ara)).ToList();

            }
            return View(list);
        }

        public ActionResult Ekle()
        {    
             return View();
        }
     
        [HttpPost]   
        public ActionResult Ekle(HastalikBilgisi Data, HttpPostedFileBase File)
        {
           //string path-Path.Combine(~/Content/Image" + File.FileName);
           //File.SaveAs(Server MapPath(path));
           //Data.Resim - File.FileName.ToString();
           db.HastalikBilgisi.Add(Data);
           db.SaveChanges();
           return RedirectToAction("Index");

        }
        public ActionResult Sil(int id)
        {
            var hastalik= db.HastalikBilgisi.Where(x=>x.HastalikID==id).FirstOrDefault();
            db.HastalikBilgisi.Remove(hastalik);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            var guncelle = db.HastalikBilgisi.Where(x => x.HastalikID == id).FirstOrDefault();
            return View(guncelle);
        }
        [HttpPost]
        public ActionResult Guncelle(HastalikBilgisi model,HttpPostedFileBase file)
        {
            var hastalik = db.HastalikBilgisi.Find(model.HastalikID);
            hastalik.HastalikID = model.HastalikID;
            hastalik.TCKimlikNo = model.TCKimlikNo;
            hastalik.HastalikAdi=model.HastalikAdi;
            hastalik.TeşhisTarihi = model.TeşhisTarihi;
            hastalik.Tedavi=model.Tedavi;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}