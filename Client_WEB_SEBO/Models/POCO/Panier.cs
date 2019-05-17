using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Client_WEB_SEBO.Models.POCO
{
    public class panier
    {
        public Hashtable listeArticle { get; set; }

        public panier()
        {
            listeArticle = new Hashtable();

        }

        public bool AddArticle(string idArticle, int qt)
        {
            bool result = false;


            if (qt >0  && listeArticle != null)
            {
                
                if (listeArticle.ContainsKey(idArticle))
                {
                    int qtPanier = (int)listeArticle[idArticle];
                    qtPanier = qtPanier + qt;
                    listeArticle[idArticle] = qtPanier;

                }
                else
                {
                    listeArticle.Add(idArticle, qt);
                }
                result = true;
            }
                       
            return result;
        }

        public bool RemoveArticle(string idArticle, int qt)
        {
            bool result = false;


            if (qt > 0)
            {
                if (listeArticle.ContainsKey(idArticle))
                {
                    int qtPanier = (int)listeArticle[idArticle];
                    if (qtPanier > qt)
                    {
                        qtPanier = qtPanier - qt;
                        listeArticle[idArticle] = qtPanier;
                        result = true;
                    }
                    if (qtPanier == qt)
                    {
                        listeArticle.Remove(idArticle);
                    }

                }
               
                result = true;
            }

            return result;
        }

        public int ArticleCount()
        {
            int result = 0;
            if (listeArticle != null)
            {
                foreach (var nbArticle in listeArticle.Values)
                {
                    result = result + (int)nbArticle;
                }
            }
            return result;
        }

        // This will convert the passed Panier object to JSON string
        public string Serialize()
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(this);
        }

        // This will convert the passed JSON string back to XYZ object
        public  static panier Deserialize(string data)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<panier>(data);
        }

    }
}