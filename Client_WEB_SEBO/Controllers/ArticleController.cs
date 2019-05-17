using Client_WEB_SEBO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client_WEB_SEBO.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Reference(string id)
        {
            Models.ViewModel.ViewSingleArticleModel viewArticle = new Models.ViewModel.ViewSingleArticleModel();          
            viewArticle.article = DAL.ArticlesDAL.GetArticle(id);
            viewArticle.genres = DAL.ArticlesDAL.GetGenres();


            return View(viewArticle);
        }
    }
}