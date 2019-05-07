using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WEB_REST_SEBO.Entities;

namespace WEB_REST_SEBO.Controllers
{
    public class ArticlesController : ApiController
    {
        private sebocestpasbeauEntities db = new sebocestpasbeauEntities();

        public ArticlesController()
        {
            db.Configuration.ProxyCreationEnabled=false;
            
        }

        // GET: api/Articles
        public IQueryable<article> Getarticle()
        {
            return db.article;
        }

        // GET: api/Articles/5
        [ResponseType(typeof(article))]
        public IHttpActionResult Getarticle(string id)
        {
            article article = db.article.Find(id);
            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }

        // PUT: api/Articles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putarticle(string id, article article)
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
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
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

        // POST: api/Articles
        [ResponseType(typeof(article))]
        public IHttpActionResult Postarticle(article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.article.Add(article);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ArticleExists(article.reference))
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

        // DELETE: api/Articles/5
        [ResponseType(typeof(article))]
        public IHttpActionResult Deletearticle(string id)
        {
            article article = db.article.Find(id);
            if (article == null)
            {
                return NotFound();
            }

            db.article.Remove(article);
            db.SaveChanges();

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

        private bool ArticleExists(string id)
        {
            return db.article.Count(e => e.reference == id) > 0;
        }
    }
}