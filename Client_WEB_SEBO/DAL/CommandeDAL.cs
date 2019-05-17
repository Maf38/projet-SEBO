using Client_WEB_SEBO.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Client_WEB_SEBO.DAL
{
    public class CommandeDAL
    {
        

            private const string BASE_ADRESS = "http://localhost:53196/api/";


            public static IEnumerable <commande> GetCommandes()
            {
                IEnumerable <commande> commandes = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BASE_ADRESS);

                    // Called article default GET All records
                    //GetAsync to send a GET request   
                    // PutAsync to send a PUT request

                    var responseTask = client.GetAsync("commandes");
                    responseTask.Wait();

                    //To store result of web api response.   
                    var result = responseTask.Result;

                    //If success received   
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<commande>>();
                        readTask.Wait();

                        commandes = readTask.Result;
                    }
                    else
                    {
                        //Error response received   
                        commandes = Enumerable.Empty<commande>();
                        //ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }


                }
                return commandes;
            }

/*
        public static commande GetCommande(string idCommande)
        {
            commande commCherche = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_ADRESS);

                // Called article default GET All records
                //GetAsync to send a GET request   
                // PutAsync to send a PUT request

                string uriREST = "articles/" + idCommande;

                var responseTask = client.GetAsync(uriREST);
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Article>();
                    readTask.Wait();

                    article = readTask.Result;
                }
                else
                {
                    //Error response received   
                    article = new Article();
                    article.auteur = "na";
                    article.description = "na";
                    article.editeur = "na";
                    article.genre = "na";
                    article.idCommande = 0;
                    article.image = "na";
                    article.prix = 0;

                    //ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }


            }
            return article;
        }*/
    }
}