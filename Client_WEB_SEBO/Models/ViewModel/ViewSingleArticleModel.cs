using Client_WEB_SEBO.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client_WEB_SEBO.Models.ViewModel
{
    public class ViewSingleArticleModel
    {
        public Article article { get; set; }
        public IEnumerable <genre> genres { get; set; }


    }
}