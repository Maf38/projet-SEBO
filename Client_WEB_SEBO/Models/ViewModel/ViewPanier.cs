using Client_WEB_SEBO.Models.POCO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client_WEB_SEBO.Models.ViewModel
{
    public class ViewPanier
    {
        public commande commande { get; set; }
        public IEnumerable <Article> articles { get; set; }
        public bool ajout { get; set; } //Booleen qui sert à savoir si on effectue un ajout ou un update du panier (methode avec HttpGET ou POST en fait)

        public string refLastArticle;

        public int qtyLastArticle;
    }
}