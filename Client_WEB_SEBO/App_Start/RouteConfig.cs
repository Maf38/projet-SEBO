using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Client_WEB_SEBO
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

                      
            routes.MapRoute(
               name: "AjoutPanier",
               url: "{controller}/{action}/{idCommande}/{referenceArticle}/{qty}",
               defaults: new { controller = "Acceuil", action = "AjouterArticle",idCommande = "2",referenceArticle="CD1" , qty= "1"}
           );


            routes.MapRoute(
               name: "Panier",
               url: "Panier/{numCommande}",
               defaults: new { controller = "Commande", action = "Panier", numCommande = "1" }
           );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Accueil", action = "Index", id = UrlParameter.Optional }
            );

           
        }
    }
}
