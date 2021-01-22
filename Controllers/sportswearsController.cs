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
using System.Web.Http.Cors;
using System.Web.Http.Description;
using FittyProject.Models;

namespace FittyProject.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class sportswearsController : ApiController
    {
        private fitnessProjectEntities db = new fitnessProjectEntities();

        // GET: api/sportswears
        public IQueryable<sportswear> Getsportswears()
        {
            return db.sportswears;
        }

        // GET: api/sportswears/5
        [ResponseType(typeof(sportswear))]
        public async Task<IHttpActionResult> Getsportswear(int id)
        {
            sportswear sportswear = await db.sportswears.FindAsync(id);
            if (sportswear == null)
            {
                return NotFound();
            }

            return Ok(sportswear);
        }

        // PUT: api/sportswears/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putsportswear(int id, sportswear sportswear)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sportswear.itemId)
            {
                return BadRequest();
            }

            //db.Entry(sportswear).State = EntityState.Modified;
            db.update_sportswear(id, sportswear.gender, sportswear.size, sportswear.type,
                sportswear.itemDescription, sportswear.price, sportswear.imageUrl, sportswear.quantity);

            return StatusCode(HttpStatusCode.OK);
        }

        // POST: api/sportswears
        [ResponseType(typeof(sportswear))]
        public async Task<IHttpActionResult> Postsportswear(sportswear sportswear)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.insert_sportswear(sportswear.gender, sportswear.type, sportswear.size, sportswear.itemDescription,
                sportswear.price, sportswear.imageUrl, sportswear.quantity);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = sportswear.itemId }, sportswear);
        }

        // DELETE: api/sportswears/5
        [ResponseType(typeof(sportswear))]
        public async Task<IHttpActionResult> Deletesportswear(int id)
        {
            sportswear sportswear = await db.sportswears.FindAsync(id);
            if (sportswear == null)
            {
                return NotFound();
            }

            db.sportswears.Remove(sportswear);
            await db.SaveChangesAsync();

            return Ok(sportswear);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool sportswearExists(int id)
        {
            return db.sportswears.Count(e => e.itemId == id) > 0;
        }
    }
}