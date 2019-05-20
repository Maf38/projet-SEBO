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
    public class ligne_de_commandeController : ApiController
    {
        private sebocestpasbeauEntities db = new sebocestpasbeauEntities();

        public ligne_de_commandeController()
        {
            // db.Configuration.ProxyCreationEnabled=false;
            db.Configuration.ProxyCreationEnabled = true;
            db.Configuration.LazyLoadingEnabled = true;

        }
        // GET: api/ligneDeCommande/
        [Route("api/ligneDeCommande/")]
        public IQueryable<ligne_de_commande> Getligne_de_commande()
        {
            return db.ligne_de_commande;
        }

        

        // GET: api/ligneDeCommande/5
        [Route("api/ligneDeCommande/{id}/{reference}")]
        [ResponseType(typeof(ligne_de_commande))]
        public async Task<IHttpActionResult> Getligne_de_commande(int id,string reference)
        {

            
            ligne_de_commande ligne_de_commande = await db.ligne_de_commande.FirstOrDefaultAsync(c=> c.idCommande.Equals(id) && c.reference.Equals(reference,StringComparison.OrdinalIgnoreCase));
            if (ligne_de_commande == null)
            {
                return NotFound();
            }

            return Ok(ligne_de_commande);
        }

        // PUT: api/ligne_de_commande/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putligne_de_commande(int id, ligne_de_commande ligne_de_commande)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ligne_de_commande.idCommande)
            {
                return BadRequest();
            }

            db.Entry(ligne_de_commande).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ligne_de_commandeExists(id))
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

        // POST: api/ligneDeCommande/{id}/{reference}/{qty}
        [Route("api/ligneDeCommande/{id}/{reference}/{qty}")]
        [ResponseType(typeof(ligne_de_commande))]
        public async Task<IHttpActionResult> Postligne_de_commande(int id,string reference, int qty )
        {

            //recherche des différents éléments nous permettant de construire(ou non ) la ligne de commande
            ligne_de_commande ligne_de_commande = await db.ligne_de_commande.FirstOrDefaultAsync(c => c.idCommande.Equals(id) && c.reference.Equals(reference, StringComparison.OrdinalIgnoreCase));
            article art = await db.article.FindAsync(reference);
            commande comm = await db.commande.FindAsync(id);

            if (art == null || comm == null)// si l'idCommande ou reference article n'existe pas il va y avoir des conflits de clé etrangères donc on fait rien
            {

                return NotFound();
            }
            else
            {

                if (ligne_de_commande == null)//cas où la ligne de commande n'existe pas
                {
                    ligne_de_commande = new ligne_de_commande();
                    ligne_de_commande.idCommande = id;
                    ligne_de_commande.reference = reference;
                    ligne_de_commande.qte = qty;
                    db.ligne_de_commande.Add(ligne_de_commande);
                }
                else
                {
                    ligne_de_commande.qte = ligne_de_commande.qte + Math.Abs(qty);

                }


                await db.SaveChangesAsync();



                return Ok(ligne_de_commande);
            }
        }

        // DELETE: api/ligne_de_commande/5
        [ResponseType(typeof(ligne_de_commande))]
        public async Task<IHttpActionResult> Deleteligne_de_commande(int id)
        {
            ligne_de_commande ligne_de_commande = await db.ligne_de_commande.FindAsync(id);
            if (ligne_de_commande == null)
            {
                return NotFound();
            }

            db.ligne_de_commande.Remove(ligne_de_commande);
            await db.SaveChangesAsync();

            return Ok(ligne_de_commande);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ligne_de_commandeExists(int id)
        {
            return db.ligne_de_commande.Count(e => e.idCommande == id) > 0;
        }
    }
}