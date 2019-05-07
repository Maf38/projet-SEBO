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
using Web_API_SEBO.Entities;

namespace Web_API_SEBO.Controllers
{
    public class CategoriesController : ApiController
    {
        private sebocestpasbeauEntities1 db = new sebocestpasbeauEntities1();

        public CategoriesController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }        

        // GET: api/Categories
        public IQueryable<categorie> Getcategorie()
        {
            return db.categorie;
        }

        // GET: api/Categories/5
        [ResponseType(typeof(categorie))]
        public async Task<IHttpActionResult> Getcategorie(string id)
        {
            categorie categorie = await db.categorie.FindAsync(id);
            if (categorie == null)
            {
                return NotFound();
            }

            return Ok(categorie);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcategorie(string id, categorie categorie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categorie.NOM)
            {
                return BadRequest();
            }

            db.Entry(categorie).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!categorieExists(id))
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

        // POST: api/Categories
        [ResponseType(typeof(categorie))]
        public async Task<IHttpActionResult> Postcategorie(categorie categorie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.categorie.Add(categorie);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (categorieExists(categorie.NOM))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = categorie.NOM }, categorie);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(categorie))]
        public async Task<IHttpActionResult> Deletecategorie(string id)
        {
            categorie categorie = await db.categorie.FindAsync(id);
            if (categorie == null)
            {
                return NotFound();
            }

            db.categorie.Remove(categorie);
            await db.SaveChangesAsync();

            return Ok(categorie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool categorieExists(string id)
        {
            return db.categorie.Count(e => e.NOM == id) > 0;
        }
    }
}