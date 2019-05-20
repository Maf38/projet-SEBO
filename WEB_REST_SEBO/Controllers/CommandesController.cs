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
    public class CommandesController : ApiController
    {
        private sebocestpasbeauEntities db = new sebocestpasbeauEntities();

        public CommandesController()
        {
            // db.Configuration.ProxyCreationEnabled=false;
            db.Configuration.ProxyCreationEnabled = true;
            db.Configuration.LazyLoadingEnabled = true;
        }

        // GET: api/commandes
        public IQueryable<commande> Getcommande()
        {
            return db.commande;
        }

        // GET: api/commandes/5
        [ResponseType(typeof(commande))]
        public async Task<IHttpActionResult> Getcommande(int id)
        {
            commande commande = await db.commande.FindAsync(id);
            if (commande == null)
            {
                return NotFound();
            }

            return Ok(commande);
        }

        // PUT: api/commandes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcommande(int id, commande commande)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != commande.id)
            {
                return BadRequest();
            }

            db.Entry(commande).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!commandeExists(id))
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

        // POST: api/commandes
        [ResponseType(typeof(commande))]
        public async Task<IHttpActionResult> Postcommande()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            commande comm = new commande();
            comm.etat = "panier";
            comm.datePanier = System.DateTime.Now;
            comm.dateCommande = System.DateTime.Now;
            comm.e_mail="visiteur@sebo.com";
            db.commande.Add(comm);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = comm.id }, comm);
        }

        // DELETE: api/commandes/5
        [ResponseType(typeof(commande))]
        public async Task<IHttpActionResult> Deletecommande(int id)
        {
            commande commande = await db.commande.FindAsync(id);
            if (commande == null)
            {
                return NotFound();
            }

            db.commande.Remove(commande);
            await db.SaveChangesAsync();

            return Ok(commande);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool commandeExists(int id)
        {
            return db.commande.Count(e => e.id == id) > 0;
        }
    }
}