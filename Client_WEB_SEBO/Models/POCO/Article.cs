using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client_WEB_SEBO.Models
{
    public class Article
    {
        public string reference { get; set; }
        public string titre { get; set; }
        public string editeur { get; set; }
        public string auteur { get; set; }
        public string description { get; set; }
        public double prix { get; set; }
        public string image { get; set; }        
        public bool reassort { get; set; }
        public int qteStock { get; set; }
        public int qteCommande { get; set; }
        public string type { get; set; }
        public object NomPromo { get; set; }
        public object idCommande { get; set; }
        public object genre { get; set; }
        public object promo { get; set; }
        public List<object> ligne_de_commande { get; set; }
    }
}