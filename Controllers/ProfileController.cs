using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
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

    public class ProfileController : ApiController
    {
        private fitnessProjectEntities db = new fitnessProjectEntities();

        // GET: api/Profile
        public IQueryable<Cart> GetCarts()
        {
            return db.Carts;
        }

        //get: api/profile/5
        [ResponseType(typeof(profile_display_Result))]
        public ObjectResult<profile_display_Result> getcart(string id)
        {
            return db.profile_display(id);
        }

        // PUT: api/Profile/5
        public User PutCart(string id)
        {
            return db.Users.Find(id);
        }

        // POST: api/Profile
        [ResponseType(typeof(Cart))]
        public async Task<IHttpActionResult> PostCart(Cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Carts.Add(cart);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cart.cartId }, cart);
        }

        // DELETE: api/Profile/5
        [ResponseType(typeof(Cart))]
        public async Task<IHttpActionResult> DeleteCart(int id)
        {
            Cart cart = await db.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            db.Carts.Remove(cart);
            await db.SaveChangesAsync();

            return Ok(cart);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CartExists(int id)
        {
            return db.Carts.Count(e => e.cartId == id) > 0;
        }
    }
}