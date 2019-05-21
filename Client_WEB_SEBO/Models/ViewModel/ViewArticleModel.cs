using Client_WEB_SEBO.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client_WEB_SEBO.Models.ViewModel
{
    public class ViewArticleModel
    {
        public IEnumerable <Article> articles { get; set; }
        public IEnumerable <genre> genres { get; set; }
        public commande commande { get; set; } 

        public bool bottomBar { get; set; } //booleen qui me sert à afficher ou non la bar de filtre du bas
        public string searchWord { get; set; } //mot cherché dans la checkbox
    }
}