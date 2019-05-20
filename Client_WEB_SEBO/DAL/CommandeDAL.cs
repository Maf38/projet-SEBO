using Client_WEB_SEBO.Models;
using Client_WEB_SEBO.Models.POCO;
using Newtonsoft.Json;
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


        public static IEnumerable<ligne_de_commande> GetLigneDeCommandes()
        {
            IEnumerable<ligne_de_commande> ldc = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_ADRESS);

                // Called article default GET All records
                //GetAsync to send a GET request   
                // PutAsync to send a PUT request

                var responseTask = client.GetAsync("ligneDeCommande/");
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ligne_de_commande>>();
                    readTask.Wait();

                    ldc = readTask.Result;
                }
                else
                {
                    //Error response received   
                    ldc = Enumerable.Empty<ligne_de_commande>();
                    //ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }


            }
            return ldc;

        }
            public static commande GetCommande(string idCommande)
        {
            commande comm = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_ADRESS);

                // Called article default GET All records
                //GetAsync to send a GET request   
                // PutAsync to send a PUT request

                string uriREST = "commandes/" + idCommande;

              
                var responseTask = client.GetAsync(uriREST);
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<commande>();
                    readTask.Wait();

                    comm = readTask.Result;
                }
                else
                {
                    //Error response received   
                    /*article = new Article();
                    article.auteur = "na";
                    article.description = "na";
                    article.editeur = "na";
                    article.genre = "na";
                    article.idCommande = 0;
                    article.image = "na";
                    article.prix = 0;*/

                    //ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }


            }
            return comm;
        }


        public static commande CreerCommande()
        {
            commande comm = new commande();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_ADRESS);

                // Called article default GET All records
                //GetAsync to send a GET request   
                // PutAsync to send a PUT request

                string uriREST = "commandes/";
              

                HttpContent content = null;
                var responseTask = client.PostAsync(uriREST,content);
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<commande>();
                    readTask.Wait();

                    comm = readTask.Result;
                }
                else
                {
                    //Error response received   
                    /*article = new Article();
                    article.auteur = "na";
                    article.description = "na";
                    article.editeur = "na";
                    article.genre = "na";
                    article.idCommande = 0;
                    article.image = "na";
                    article.prix = 0;*/

                    //ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return comm;
        }

        public static ligne_de_commande AjouterArticle(string idCommande, string referenceArticle, int qty)
        {

            ligne_de_commande ligneDeCommande = null;

            using (var client = new HttpClient())
            {
                //creation de l'URL d'acces au web service
                client.BaseAddress = new Uri(BASE_ADRESS);
                string uriREST = "ligneDeCommande/" + idCommande + "/" + referenceArticle + "/" + qty;

                HttpContent content = null;
                var responseTask = client.PostAsync(uriREST,content);
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ligne_de_commande>();
                    readTask.Wait();

                    ligneDeCommande = readTask.Result;
                }
            }

            return ligneDeCommande;
        }

    }
}