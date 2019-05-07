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
    public class GenresController : ApiController
    {
        private sebocestpasbeauEntities1 db = new sebocestpasbeauEntities1();

        public GenresController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Genres
        public IQueryable<genre> Getgenre()
        {
            return db.genre;
        }

        // GET: api/Genres/5
        [ResponseType(typeof(genre))]
        public async Task<IHttpActionResult> Getgenre(string id)
        {
            genre genre = await db.genre.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            return Ok(genre);
        }

        // PUT: api/Genres/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putgenre(string id, genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != genre.type)
            {
                return BadRequest();
            }

            db.Entry(genre).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!genreExists(id))
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

        // POST: api/Genres
        [ResponseType(typeof(genre))]
        public async Task<IHttpActionResult> Postgenre(genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.genre.Add(genre);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (genreExists(genre.type))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = genre.type }, genre);
        }

        // DELETE: api/Genres/5
        [ResponseType(typeof(genre))]
        public async Task<IHttpActionResult> Deletegenre(string id)
        {
            genre genre = await db.genre.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            db.genre.Remove(genre);
            await db.SaveChangesAsync();

            return Ok(genre);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool genreExists(string id)
        {
            return db.genre.Count(e => e.type == id) > 0;
        }
    }
}