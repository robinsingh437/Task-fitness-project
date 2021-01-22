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
    public class gearsController : ApiController
    {
        private fitnessProjectEntities db = new fitnessProjectEntities();

        // GET: api/gears
        public IQueryable<gear> Getgears()
        {
            return db.gears;
        }

        // GET: api/gears/5
        [ResponseType(typeof(gear))]
        public async Task<IHttpActionResult> Getgear(int id)
        {
            gear gear = await db.gears.FindAsync(id);
            if (gear == null)
            {
                return NotFound();
            }

            return Ok(gear);
        }

        // PUT: api/gears/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putgear(int id, gear gear)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gear.itemId)
            {
                return BadRequest();
            }

            db.update_gear(id, gear.type, gear.itemDescription, gear.price, gear.imageUrl, gear.quantity);

            return StatusCode(HttpStatusCode.OK);
        }

        // POST: api/gears
        [ResponseType(typeof(gear))]
        public async Task<IHttpActionResult> Postgear(gear gear)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.insert_gear(gear.type, gear.itemDescription, gear.price, gear.imageUrl, gear.quantity);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = gear.itemId }, gear);
        }

        // DELETE: api/gears/5
        [ResponseType(typeof(gear))]
        public async Task<IHttpActionResult> Deletegear(int id)
        {
            gear gear = await db.gears.FindAsync(id);
            if (gear == null)
            {
                return NotFound();
            }

            db.gears.Remove(gear);
            await db.SaveChangesAsync();

            return Ok(gear);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool gearExists(int id)
        {
            return db.gears.Count(e => e.itemId == id) > 0;
        }
    }
}