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
    public class AdressesController : ApiController
    {
        private sebocestpasbeauEntities1 db = new sebocestpasbeauEntities1();

        public AdressesController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Adresses
        public IQueryable<adresse> Getadresse()
        {
            return db.adresse;
        }

        // GET: api/Adresses/5
        [ResponseType(typeof(adresse))]
        public async Task<IHttpActionResult> Getadresse(int id)
        {
            adresse adresse = await db.adresse.FindAsync(id);
            if (adresse == null)
            {
                return NotFound();
            }

            return Ok(adresse);
        }

        // PUT: api/Adresses/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putadresse(int id, adresse adresse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != adresse.ID)
            {
                return BadRequest();
            }

            db.Entry(adresse).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!adresseExists(id))
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

        // POST: api/Adresses
        [ResponseType(typeof(adresse))]
        public async Task<IHttpActionResult> Postadresse(adresse adresse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.adresse.Add(adresse);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = adresse.ID }, adresse);
        }

        // DELETE: api/Adresses/5
        [ResponseType(typeof(adresse))]
        public async Task<IHttpActionResult> Deleteadresse(int id)
        {
            adresse adresse = await db.adresse.FindAsync(id);
            if (adresse == null)
            {
                return NotFound();
            }

            db.adresse.Remove(adresse);
            await db.SaveChangesAsync();

            return Ok(adresse);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool adresseExists(int id)
        {
            return db.adresse.Count(e => e.ID == id) > 0;
        }
    }
}