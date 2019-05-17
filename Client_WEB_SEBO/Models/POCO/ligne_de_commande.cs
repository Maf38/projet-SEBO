using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client_WEB_SEBO.Models.POCO
{
    public class ligne_de_commande
    {
        
            public int idCommande { get; set; }
            public string reference { get; set; }
            public int qte { get; set; }
            public Article article { get; set; }
            public commande commande { get; set; }
       
    }
}