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

            //gestion du panier
            HttpCookie cookie = Request.Cookies["userInfo"];
          

            if (cookie == null)//cas où le visiteur vient pour la premiere fois ou que le cookie a expiré
            {
                cookie = new HttpCookie("userInfo");
                cookie.Expires = DateTime.Now.AddHours(1);//Expiration du cookie
                

                viewArticles.commande = DAL.CommandeDAL.CreerCommande();

                if (viewArticles.commande != null)//si la création d'une commande s'est bien passé
                {
                    cookie["idCommande"] = viewArticles.commande.id.ToString();

                }
                else
                {
                   cookie.Value = "2";//par convention 2 est la commande qui gere les problemes (utile????)

                }

                Response.Cookies.Add(cookie);//sauvegarde du cookie

            }
            else// on recupere la commande associée au numero de commande du cookie dans le modele
            {


                string idCommande = string.Empty;
                idCommande = cookie["idCommande"].ToString();

                viewArticles.commande = DAL.CommandeDAL.GetCommande(idCommande);
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

        [HttpGet]
        public ActionResult AjouterArticle()
        {
            ViewPanier panier = new ViewPanier();


            //on recupere la commande courante
            HttpCookie cookie = Request.Cookies["userInfo"];
            string idCommande = string.Empty;
            if (cookie != null)
            {
               
                idCommande = cookie["idCommande"].ToString();
            }

            if (idCommande == null || idCommande=="0")
            {
                idCommande ="1";
            }

            panier.commande = DAL.CommandeDAL.GetCommande(idCommande);

          

            //on recupere les lignes de commande de la commande
            panier.commande.ligne_de_commande= DAL.CommandeDAL.GetLigneDeCommandes().Where(c => c.idCommande == int.Parse(idCommande));
            

            //on a pas rajouté d'article au panier
            panier.ajout = false;

            //Attention!!!pour l'instant on ne recupère pas les articles pour le viewModel

            return PartialView(panier);
        }
        [Route("Accueil/AjouterArticle/{numCommande}/{referenceArticle}/{qty}")]
        [HttpPost]
        public ActionResult AjouterArticle(string numCommande, string referenceArticle, int qty)
        {

            ViewPanier panier = new ViewPanier();

            //on ajoute les lignes de commande grace à la methode du DAL
            ligne_de_commande ldc = DAL.CommandeDAL.AjouterArticle(numCommande, referenceArticle, qty);

            //on verifie si le traitement s'est bien passé en testant la nullité
            if (ldc == null)
            {
                ldc = new ligne_de_commande();
                ldc.reference = "CD1";
                ldc.idCommande = 2;
                ldc.qte = 1;
            }

            //on a ajouté un article au panier
            panier.ajout = true;
            panier.qtyLastArticle = qty;
            panier.refLastArticle = referenceArticle;

            //on met à jout la commande du view model
            panier.commande = DAL.CommandeDAL.GetCommande(numCommande);

            //on met à jour la ligne de commande envoyé à la vue 
            panier.commande.ligne_de_commande = DAL.CommandeDAL.GetLigneDeCommandes().Where(c => c.idCommande == int.Parse(numCommande));


            return PartialView(panier);
        }

    }
}