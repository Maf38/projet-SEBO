using Client_WEB_SEBO.Models;
using Client_WEB_SEBO.Models.POCO;
using Client_WEB_SEBO.Models.ViewModel;
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

            ViewArticleModel viewArticles = new ViewArticleModel();
            viewArticles.articles = DAL.ArticlesDAL.GetArticles();
            viewArticles.genres = DAL.ArticlesDAL.GetGenres();
        

        return View(viewArticles);
        }

        public ActionResult ArticleByGenre(string id)
        {

            ViewArticleModel viewArticles = new ViewArticleModel();
            viewArticles.articles = DAL.ArticlesDAL.GetArticles(id);
            viewArticles.genres = DAL.ArticlesDAL.GetGenres();


            return View(viewArticles);
        }
    }
}