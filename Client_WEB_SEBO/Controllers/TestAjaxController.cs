using Client_WEB_SEBO.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client_WEB_SEBO.Controllers
{
    public class TestAjaxController : Controller
    {
        // GET: TestAjax

    
        public ActionResult Index()
        {
            panier panier = new panier();
            return View(panier);
        }

        [HttpGet]
        public ActionResult AffichePanier()
        {
            
            return PartialView();
        }


        [HttpPost]
        public ActionResult AffichePanier(string panierSerialise )
        {
            panier panier = panier.Deserialize(panierSerialise);

            Random rnd = new Random();
            int nb = rnd.Next(1, 100);

            string randomArticle = "CD" + nb;
            panier.AddArticle(randomArticle, rnd.Next(1,5));
            panier.AddArticle("CD33", 3);
            panier.AddArticle("DVD 54", 8);
            panier.AddArticle(panierSerialise, 100);
            ViewBag.modelePanier = panier;


            return PartialView(panier);
        }

        [ChildActionOnly]
        public ActionResult PriceRange()
        {
            ViewBag.MaxPrice = 963;
            ViewBag.MinPrice = 354;
            return PartialView();
        }

        //
        // POST: /Home/Ajax
        [HttpPost]
        public PartialViewResult Ajax()
        {
            // use partial view so we're not bringing the entire page's theme
            // back in the response. We're simply returning the content within
            // ~/Views/Home/Ajax.cshtml
            return PartialView();
        }


    }
}