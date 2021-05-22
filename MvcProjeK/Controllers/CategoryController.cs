using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeK.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetCategoryList()
        {
            //değişken neden var türünde?; Çünkü göndereceğim tabloda sayısal veriler,karakterler,metin türünde olabilir. var hepsini kapsar
            var categoryvalues = cm.GetList();  //categoryvalues değişkenin içerisine category tablomda yer alan veriler gelecek.
            return View(categoryvalues);  //categoryvaluesteki değerler view'e aktarılır.
        }

        [HttpGet]  //Sayfa yüklendiği zaman çalışır.
        public ActionResult AddCategory()
        {
            return View(); //sadece sayfayı geri döndür.
        }

        [HttpPost]  //HttpPost attribute devreye girdiğinde çalışacaksın.Yani sayfada bir butona tıkladığımızda sayfada bir şey post edildiği zaman çalışacak 
        public ActionResult AddCategory(Category p)
        {
            //cm.CategoryAddBL(p);
            CategoryValidator categoryValidatior = new CategoryValidator();
            ValidationResult results = categoryValidatior.Validate(p);
            if (results.IsValid) //sonuç(result) geçerli ise
            {
                cm.CategoryAdd(p);
                return RedirectToAction("GetCategoryList");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View(); //Ekleme işlemini gerçekleştirdikten sonra bizi GetCategoryList'e yönlendirir
        }
    }
}