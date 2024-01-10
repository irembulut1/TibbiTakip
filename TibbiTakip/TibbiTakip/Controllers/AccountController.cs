using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml.Linq;
using TibbiTakip.Models;

namespace TibbiTakip.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        TakipSistemiEntities5 db = new TakipSistemiEntities5();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Login(Kullanicilar k)
        {
            var bilgiler=db.Kullanicilar.FirstOrDefault(x=>x.TCKimlikNo==k.TCKimlikNo && x.Sifre==k.Sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.TCKimlikNo, false);
                Session["Ad"] = bilgiler.Ad.ToString();
                Session["Soyad"] = bilgiler.Soyad.ToString();
                Session["TCKimlikNo"] = bilgiler.TCKimlikNo.ToString();
                Session["Rol"] = bilgiler.Rol.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.hata = "TC veya şifre hatalı";
            }
                return View();
        }

        public  ActionResult Register() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Kullanicilar k)
        {
            db.Kullanicilar.Add(k);
            k.Rol = 0;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult LogOut() 
        { 
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}