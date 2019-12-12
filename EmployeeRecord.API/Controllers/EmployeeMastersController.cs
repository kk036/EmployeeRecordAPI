using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using EmployeeRecord.API.Models;

namespace EmployeeRecord.API.Controllers
{
    public class EmployeeMastersController : ApiController
    {
        private EmployeeRecordApiEntities db = new EmployeeRecordApiEntities();

        // GET: api/EmployeeMasters
        public IQueryable<EmployeeMaster> GetEmployeeMasters()
        {
            return db.EmployeeMasters;
        }

        // GET: api/EmployeeMasters/5
        [ResponseType(typeof(EmployeeMaster))]
        public IHttpActionResult GetEmployeeMaster(int id)
        {
            EmployeeMaster employeeMaster = db.EmployeeMasters.Find(id);
            if (employeeMaster == null)
            {
                return NotFound();
            }

            return Ok(employeeMaster);
        }

        // PUT: api/EmployeeMasters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployeeMaster(int id, EmployeeMaster employeeMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeMaster.ID)
            {
                return BadRequest();
            }

            db.Entry(employeeMaster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeMasterExists(id))
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

        // POST: api/EmployeeMasters
        [ResponseType(typeof(EmployeeMaster))]
        public IHttpActionResult PostEmployeeMaster(EmployeeMaster employeeMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmployeeMasters.Add(employeeMaster);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmployeeMasterExists(employeeMaster.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = employeeMaster.ID }, employeeMaster);
        }

        // DELETE: api/EmployeeMasters/5
        [ResponseType(typeof(EmployeeMaster))]
        public IHttpActionResult DeleteEmployeeMaster(int id)
        {
            EmployeeMaster employeeMaster = db.EmployeeMasters.Find(id);
            if (employeeMaster == null)
            {
                return NotFound();
            }

            db.EmployeeMasters.Remove(employeeMaster);
            db.SaveChanges();

            return Ok(employeeMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeMasterExists(int id)
        {
            return db.EmployeeMasters.Count(e => e.ID == id) > 0;
        }
    }
}