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
    }
}