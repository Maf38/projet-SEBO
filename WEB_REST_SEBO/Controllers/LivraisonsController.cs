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
    public class LivraisonsController : ApiController
    {
        private sebocestpasbeauEntities1 db = new sebocestpasbeauEntities1();

        public LivraisonsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Livraisons
        public IQueryable<livraison> Getlivraison()
        {
            return db.livraison;
        }

        // GET: api/Livraisons/5
        [ResponseType(typeof(livraison))]
        public async Task<IHttpActionResult> Getlivraison(int id)
        {
            livraison livraison = await db.livraison.FindAsync(id);
            if (livraison == null)
            {
                return NotFound();
            }

            return Ok(livraison);
        }

        // PUT: api/Livraisons/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putlivraison(int id, livraison livraison)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != livraison.idLivraison)
            {
                return BadRequest();
            }

            db.Entry(livraison).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!livraisonExists(id))
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

        // POST: api/Livraisons
        [ResponseType(typeof(livraison))]
        public async Task<IHttpActionResult> Postlivraison(livraison livraison)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.livraison.Add(livraison);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = livraison.idLivraison }, livraison);
        }

        // DELETE: api/Livraisons/5
        [ResponseType(typeof(livraison))]
        public async Task<IHttpActionResult> Deletelivraison(int id)
        {
            livraison livraison = await db.livraison.FindAsync(id);
            if (livraison == null)
            {
                return NotFound();
            }

            db.livraison.Remove(livraison);
            await db.SaveChangesAsync();

            return Ok(livraison);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool livraisonExists(int id)
        {
            return db.livraison.Count(e => e.idLivraison == id) > 0;
        }
    }
}