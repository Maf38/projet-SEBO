using Client_WEB_SEBO.Models;
using Client_WEB_SEBO.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Client_WEB_SEBO.Controllers
{
    public class AccueilController : Controller { 
    
        // GET: Accueil
        public ActionResult Index()
        {
            IEnumerable<Article> articles = DAL.DAL.GetArticles();
            IEnumerable<Genre> genres = DAL.DAL.GetGenres();

        return View(articles);
        }
    }
}