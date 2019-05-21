using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WEB_REST_SEBO.Entities;

namespace WEB_REST_SEBO.Controllers
{
    public class ArticlesByTypeController : ApiController
    {
        private sebocestpasbeauEntities db = new sebocestpasbeauEntities();

        // GET: api/ArticlesByType
        public IQueryable<article> Getarticle()
        {
            return db.article;
        }

        //GET: api/ArticlesByType/pop-rock
        public IQueryable<article> Gettype(string id)
        {
            IQueryable<article> listeArticle = db.article.Where(art => art.genre.type == id);


            return listeArticle;
        }

               

        // PUT: api/ArticlesByType/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putarticle(string id, article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != article.reference)
            {
                return BadRequest();
            }

            db.Entry(article).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!articleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ArticlesByType
        [ResponseType(typeof(article))]
        public async Task<IHttpActionResult> Postarticle(article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.article.Add(article);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (articleExists(article.reference))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = article.reference }, article);
        }

        // DELETE: api/ArticlesByType/5
        [ResponseType(typeof(article))]
        public async Task<IHttpActionResult> Deletearticle(string id)
        {
            article article = await db.article.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            db.article.Remove(article);
            await db.SaveChangesAsync();

            return Ok(article);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool articleExists(string id)
        {
            return db.article.Count(e => e.reference == id) > 0;
        }
    }
}