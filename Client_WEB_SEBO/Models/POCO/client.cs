using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client_WEB_SEBO.Models.POCO
{
    public class client
    {
        public string e_mail { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string mdp { get; set; }
        public int IDAdresse { get; set; }
    }
}