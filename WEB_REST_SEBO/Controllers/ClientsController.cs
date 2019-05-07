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
    public class ClientsController : ApiController
    {
        private sebocestpasbeauEntities1 db = new sebocestpasbeauEntities1();

        public ClientsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Clients
        public IQueryable<client> Getclient()
        {
            return db.client;
        }

        // GET: api/Clients/5
        [ResponseType(typeof(client))]
        public async Task<IHttpActionResult> Getclient(string id)
        {
            client client = await db.client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // PUT: api/Clients/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putclient(string id, client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.e_mail)
            {
                return BadRequest();
            }

            db.Entry(client).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!clientExists(id))
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

        // POST: api/Clients
        [ResponseType(typeof(client))]
        public async Task<IHttpActionResult> Postclient(client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.client.Add(client);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (clientExists(client.e_mail))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = client.e_mail }, client);
        }

        // DELETE: api/Clients/5
        [ResponseType(typeof(client))]
        public async Task<IHttpActionResult> Deleteclient(string id)
        {
            client client = await db.client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            db.client.Remove(client);
            await db.SaveChangesAsync();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool clientExists(string id)
        {
            return db.client.Count(e => e.e_mail == id) > 0;
        }
    }
}