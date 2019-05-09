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
        public IEnumerable <Genre> genres { get; set; }
    }
}