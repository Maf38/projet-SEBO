using Client_WEB_SEBO.DAL;
using Client_WEB_SEBO.Models;
using Client_WEB_SEBO.Models.POCO;
using Client_WEB_SEBO.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client_WEB_SEBO.Controllers
{
    public class CommandeController : Controller
    {
        // GET: Commande
        public ActionResult Index()
        {
            ViewAllCommandes viewCommandes = new ViewAllCommandes();
            viewCommandes.commandes = DAL.CommandeDAL.GetCommandes();

            return View(viewCommandes);
        }

        public ActionResult GetCommande(string id)
        {
            commande com = DAL.CommandeDAL.GetCommande(id);

            return View(com);
        }

        public ActionResult CreerCommande()
        {
            commande com = DAL.CommandeDAL.CreerCommande();

            return View(com);
        }

        
        public ActionResult Panier(string numCommande)
        {
            ViewPanier panier = new ViewPanier();
            panier.commande= DAL.CommandeDAL.GetCommande(numCommande);

            IEnumerable<ligne_de_commande> ttesLesLignes = CommandeDAL.GetLigneDeCommandes();
            panier.commande.ligne_de_commande = ttesLesLignes.Where(ldc => ldc.idCommande==int.Parse(numCommande));


            panier.articles = DAL.ArticlesDAL.GetArticles();
                                 
            return View(panier);
        }

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

        /*
         * Ancienne Version!!!!
         * 
         * public ActionResult AjouterArticle(string idCommande, string referenceArticle, int qty)
        {
            //on ajoute les lignes de commande grace à la methode du DAL
            ligne_de_commande ldc = DAL.CommandeDAL.AjouterArticle(idCommande, referenceArticle, qty);

            //on verifie si le traitement s'est bien passé en testant la nullité
            if (ldc == null)
            {
                ldc = new ligne_de_commande();
                ldc.reference = "CD1";
                ldc.idCommande = 2;
                ldc.qte = 1;
            }

            //on met à jour la ligne de commande envoyé à la vue 
            IEnumerable <ligne_de_commande> ligneDeCommandeUpdate =  DAL.CommandeDAL.GetLigneDeCommandes().Where(c => c.idCommande == int.Parse(idCommande));


            return PartialView(ligneDeCommandeUpdate);
        }
        */
        /*
[Route (" Commande/Panier")]
public ActionResult Panier()
{
return View("Error");
}

*/
    }
}