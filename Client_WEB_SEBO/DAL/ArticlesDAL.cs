using Client_WEB_SEBO.Models;
using Client_WEB_SEBO.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Client_WEB_SEBO.DAL
{
    public class ArticlesDAL
    {

        private const string BASE_ADRESS = "http://localhost:53196/api/";


        public static IEnumerable<Article> GetArticles(string genre)
        {
            IEnumerable<Article> articles = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_ADRESS);

                // Called article default GET All records
                //GetAsync to send a GET request   
                // PutAsync to send a PUT request

                string uriREST;

                if (genre == "NA"){//si le genre est spécifié à NA on appelle le WEB service qui charge tous les articles

                    uriREST = "articles";
                }
                else//sinon on charge les articles par genre
                {

                    uriREST = "articlesByGenre/" + genre;
                }
                var responseTask = client.GetAsync(uriREST);
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
                    //ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }


            }
            return articles;
        }

        public static IEnumerable<Article> GetArticles()
        {
            IEnumerable<Article> articles = GetArticles("NA");

            return articles;
        }

            public static IEnumerable<Genre> GetGenres()
        {
            IEnumerable<Genre> genres = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_ADRESS);

                // Called article default GET All records
                //GetAsync to send a GET request   
                // PutAsync to send a PUT request  
                var responseTask = client.GetAsync("genres");
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Genre>>();
                    readTask.Wait();

                    genres = readTask.Result;
                }
                else
                {
                    //Error response received   
                    genres = Enumerable.Empty<Genre>();
                    //ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }


            }
            return genres;
        }

    }
}