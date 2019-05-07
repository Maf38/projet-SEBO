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
    public class PromosController : ApiController
    {
        private sebocestpasbeauEntities1 db = new sebocestpasbeauEntities1();

        public PromosController()
        {
            db.Configuration.ProxyCreationEnabled = false;

        }

        // GET: api/Promos
        public IQueryable<promo> Getpromo()
        {
            return db.promo;
        }

        // GET: api/Promos/5
        [ResponseType(typeof(promo))]
        public async Task<IHttpActionResult> Getpromo(string id)
        {
            promo promo = await db.promo.FindAsync(id);
            if (promo == null)
            {
                return NotFound();
            }

            return Ok(promo);
        }

        // PUT: api/Promos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putpromo(string id, promo promo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != promo.nom)
            {
                return BadRequest();
            }

            db.Entry(promo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!promoExists(id))
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

        // POST: api/Promos
        [ResponseType(typeof(promo))]
        public async Task<IHttpActionResult> Postpromo(promo promo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.promo.Add(promo);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (promoExists(promo.nom))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = promo.nom }, promo);
        }

        // DELETE: api/Promos/5
        [ResponseType(typeof(promo))]
        public async Task<IHttpActionResult> Deletepromo(string id)
        {
            promo promo = await db.promo.FindAsync(id);
            if (promo == null)
            {
                return NotFound();
            }

            db.promo.Remove(promo);
            await db.SaveChangesAsync();

            return Ok(promo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool promoExists(string id)
        {
            return db.promo.Count(e => e.nom == id) > 0;
        }
    }
}