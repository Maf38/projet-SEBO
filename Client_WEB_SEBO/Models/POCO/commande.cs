using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client_WEB_SEBO.Models.POCO
{
    public class commande
    {

    
        public int id { get; set; }
        public System.DateTime dateCommande { get; set; }
        public string etat { get; set; }
        public System.DateTime datePanier { get; set; }
        public string e_mail { get; set; }
        public  client client { get; set; }       
        public virtual ICollection <ligne_de_commande> ligne_de_commande { get; set; }
      

    }
}