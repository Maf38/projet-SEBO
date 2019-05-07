using Client_WEB_SEBO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Client_WEB_SEBO.Controllers
{
    public class CatalogueController : Controller
    {
        // GET: Catalogue
        public ActionResult GetArticles()
        {
            IEnumerable <Article> articles = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53196/api/");

                // Called article default GET All records
                //GetAsync to send a GET request   
                // PutAsync to send a PUT request  
                var responseTask = client.GetAsync("articles");
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Article>>();
                    readTask.Wait();

                    articles = readTask.Result;
                }
                else
                {
                    //Error response received   
                    articles = Enumerable.Empty<Article>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }


            }
                return View(articles);
        }
    }
}