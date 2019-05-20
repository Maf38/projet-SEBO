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
           

            //gestion du panier
            HttpCookie reqCookies = Request.Cookies["userInfo"];

            if (reqCookies == null)//cas où le visiteur vient pour la premiere fois ou que le cookie a expiré
            {
                HttpCookie userInfo = new HttpCookie("userInfo");
                userInfo.Expires = DateTime.Now.AddHours(1);//Expiration du cookie

                viewArticles.commande = DAL.CommandeDAL.CreerCommande();

                if (viewArticles.commande != null)//si la création d'une commande s'est bien passé
                {
                    Response.Cookies["numCommande"].Value = viewArticles.commande.id.ToString();

                }
                else
                {
                    Response.Cookies["numCommande"].Value = "131313131313";//par convention 131313131313 est la commande qui gere les problemes (utile????)

                }

                Response.Cookies.Add(userInfo);//sauvegarde du cookie

            }
            else// on recupere la commande associée au numero de commande du cookie dans le modele
            {


                viewArticles.commande = DAL.CommandeDAL.GetCommande(reqCookies["numCommande"].ToString());
            }



            return View(viewArticles);
        }

        public ActionResult AfficheNbArticlePanier(panier p)
        {
            
            return PartialView();
        }

        
        public ActionResult AfficheTableau(ViewArticleModel viewArticleModel)
        {
            //viewArticleModel.panier.AddArticle("CD2",1);
            return PartialView(viewArticleModel);
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