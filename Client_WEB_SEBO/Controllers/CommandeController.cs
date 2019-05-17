using Client_WEB_SEBO.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client_WEB_SEBO.Controllers
{
    public class CommandeController : Controller
    {
        // GET: Commande
        public ActionResult Index()
        {
            ViewAllCommandes viewCommandes = new ViewAllCommandes();
                viewCommandes.commandes= DAL.CommandeDAL.GetCommandes();
            
            return View(viewCommandes);
        }
    }
}