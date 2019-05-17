using Client_WEB_SEBO.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client_WEB_SEBO.Models.ViewModel
{
    public class ViewAllCommandes
    {
        public IEnumerable <commande> commandes { get; set; }
    }
}