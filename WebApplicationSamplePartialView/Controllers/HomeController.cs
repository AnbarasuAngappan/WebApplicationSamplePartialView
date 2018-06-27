using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationSamplePartialView.Controllers
{
    public class HomeController : Controller
    {
        private SamplePartialViewDataEntities entities = new SamplePartialViewDataEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult List()
        {
            List<PartialView> partialViews = entities.PartialViews.ToList();
            return View(partialViews);

        }

        public ActionResult Returnpartialview()
        {
            List<PartialView> partialViews1 = entities.PartialViews.ToList();
            return PartialView("Returnpartialview", partialViews1);
        }

        public ActionResult Jquerry()
        {
            // PartialView partialView = new PartialView();
            //  partialView.JavascriptToRun = "ShowErrorPopup()";

            //partialView.ID = 02;
            //partialView.Name = "Anbu";
            //partialView.Gender = "Male";
            //partialView.City = "Tup";
            //partialView.JavascriptToRun = "ShowErrorPopup()";

            return View();//partialView
        }
      
        public ActionResult Postdata()
        {
            ViewBag.Message = "Welcome to my demo!";
            PartialView partialView = new PartialView();
            partialView.partialviewDBItems = entities.PartialViews.ToList();
            return View(partialView);
        }

        [HttpPost]
        public ActionResult Postdata(PartialView partialView)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Postdata", "Home", new { });
            try
            {
                if (ModelState.IsValid)
                {
                    entities.PartialViews.Add(partialView);
                    entities.SaveChanges();
                    partialView.partialviewDBItems = entities.PartialViews.ToList();
                    //return RedirectToAction("Postdata");
                   // return View(partialView);                  
                    //return Json(new { Url = redirectUrl });
                }

                 //return View();
            }
            catch (Exception ex)
            {
                return Json(new { Url = redirectUrl, status = "Error" });

                throw new Exception(ex.Message);
            }

            return Json(new { Url = redirectUrl, status = "OK" });
        }

//        [HttpPost]
//        public ActionResult Cancel( //parameters here )
//{
//            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "user", new { });

//            try
//            {
//                // perform some action
//            }
//            catch (Exception)
//            {
//                return Json(new { Url = redirectUrl, status = "Error" });
//            }

//            return Json(new { Url = redirectUrl, status = "OK" });
//        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartialView partialView = entities.PartialViews.Find(id);
            if (partialView == null)
            {
                return HttpNotFound();
            }
            return View(partialView);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            PartialView partialView = entities.PartialViews.Find(id);
            entities.PartialViews.Remove(partialView);
            entities.SaveChanges();            
            return RedirectToAction("Postdata");
        }


        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PartialView partialView = entities.PartialViews.Find(id);
                if (partialView == null)
                {
                    return HttpNotFound();
                }
                return View(partialView);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PartialView partialView)//[Bind(Include = "CustomerID,CustomerName,ContactMidName,PhoneNumber,EmailID,Password,ConfirmPassword")]Customer customer
        {
            if (ModelState.IsValid)
            {
                //PartialView partialView = new PartialView();
                UpdateModel(partialView);//UpdateModel<Customer>(customers);
                entities.Entry(partialView).State = EntityState.Modified;
                entities.SaveChanges();
                partialView.partialviewDBItems = entities.PartialViews.ToList();
                return View("Details");
            }
            return View();
        }

    }
}