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
    public class menSportswearController : ApiController
    {
        private fitnessProjectEntities db = new fitnessProjectEntities();

        // GET: api/menSportswear
        public IQueryable<sportswear> Getsportswears()
        {
            var res = from s in db.sportswears
                      where s.gender == "M"
                      select s;
            return res;
        }

        // GET: api/menSportswear/5
        public IQueryable<sportswear> Getsportswear(string id)
        {
            var res = from s in db.sportswears
                      where s.gender == "M" && s.type==id
                      select s;
            return res;
        }

        // PUT: api/menSportswear/5
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

            db.Entry(sportswear).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!sportswearExists(id))
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

        // POST: api/menSportswear
        [ResponseType(typeof(sportswear))]
        public async Task<IHttpActionResult> Postsportswear(sportswear sportswear)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sportswears.Add(sportswear);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = sportswear.itemId }, sportswear);
        }

        // DELETE: api/menSportswear/5
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