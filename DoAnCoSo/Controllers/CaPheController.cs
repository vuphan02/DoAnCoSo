using DoAnCoSo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;

namespace DoAnCoSo.Controllers
{
    public class CaPheController : Controller
    {
        // GET: CaPhe
        DataClasses1DataContext data = new DataClasses1DataContext();
        public ActionResult ListCaPhe()
        {
            var all_coffee = from cf in data.CaPhes select cf;
            return View(all_coffee);
        }

        public ActionResult Details(int id)
        {
            var TT_caphe = data.CaPhes.Where(m => m.macaphe == id).First();
            return View(TT_caphe);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Create(FormCollection collection, CaPhe cp)
        {
            var C_tencaphe = collection["tencaphe"];
            var C_hinh = collection["hinh"];
            var C_giaban = Convert.ToDecimal(collection["giaban"]);
            var C_ngaycapnhat = Convert.ToDateTime(collection["ngaycapnhat"]);
            var C_soluongton = Convert.ToInt32(collection["soluongton"]);
            if(string.IsNullOrEmpty(C_tencaphe))
            {
                ViewData["Error"] = "Không được để trống";
            }
            else
            {
                cp.tencaphe = C_tencaphe.ToString();
                cp.hinh = C_hinh.ToString();
                cp.giaban = C_giaban;
                cp.ngaycapnhat = C_ngaycapnhat;
                cp.soluongton = C_soluongton;
                data.CaPhes.InsertOnSubmit(cp);
                data.SubmitChanges();
                return RedirectToAction("ListCaPhe");
            }
            return this.Create();
        }

        public ActionResult Edit(int id)
        {
            var E_caphe = data.CaPhes.First(m => m.macaphe == id);
            return View(E_caphe);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_caphe = data.CaPhes.First(m => m.macaphe == id);
            var E_tencaphe = collection["tencaphe"];
            var E_hinh = collection["hinh"];
            var E_giaban = Convert.ToDecimal(collection["giaban"]);
            var E_ngaycapnhat = Convert.ToDateTime(collection["ngaycapnhat"]);
            var E_soluongton = Convert.ToInt32(collection["soluongton"]);
            E_caphe.macaphe = id;
            if(string.IsNullOrEmpty(E_tencaphe))
            {
                ViewData["Error"] = "Không được để trống";
            }
            else
            {
                E_caphe.tencaphe = E_tencaphe;
                E_caphe.hinh = E_hinh;
                E_caphe.giaban = E_giaban;
                E_caphe.ngaycapnhat = E_ngaycapnhat;
                E_caphe.soluongton = E_soluongton;
                UpdateModel(E_caphe);
                data.SubmitChanges();
                return RedirectToAction("ListCaPhe");
            }
            return this.Edit(id);
        }

        public ActionResult Delete(int id)
        {
            var D_caphe = data.CaPhes.First(m => m.macaphe == id);
            return View(D_caphe);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_caphe = data.CaPhes.Where(m => m.macaphe == id).First();
            data.CaPhes.DeleteOnSubmit(D_caphe);
            data.SubmitChanges();
            return RedirectToAction("ListCaPhe");
        }
    }
}