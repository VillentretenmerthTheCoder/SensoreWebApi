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
using SensoreWebApi.Models;
using System.Web.Http.Cors;

namespace SensoreWebApi.Controllers
{
    public class SensorDatasController : ApiController
    {
        private SensorRestService20191117043106_dbEntities db = new SensorRestService20191117043106_dbEntities();

        // GET: api/SensorDatas
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IQueryable<SensorData> GetSensorDatas()
        {
            return db.SensorDatas;
        }

        // GET: api/SensorDatas/5
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [ResponseType(typeof(SensorData))]
        public IHttpActionResult GetSensorData(int id)
        {
            SensorData sensorData = db.SensorDatas.Find(id);
            if (sensorData == null)
            {
                return NotFound();
            }

            return Ok(sensorData);
        }

        // PUT: api/SensorDatas/5
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSensorData(int id, SensorData sensorData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sensorData.id)
            {
                return BadRequest();
            }

            db.Entry(sensorData).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SensorDataExists(id))
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

        // POST: api/SensorDatas
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [ResponseType(typeof(SensorData))]
        public IHttpActionResult PostSensorData(SensorData sensorData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SensorDatas.Add(sensorData);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sensorData.id }, sensorData);
        }

        // DELETE: api/SensorDatas/5
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [ResponseType(typeof(SensorData))]
        public IHttpActionResult DeleteSensorData(int id)
        {
            SensorData sensorData = db.SensorDatas.Find(id);
            if (sensorData == null)
            {
                return NotFound();
            }

            db.SensorDatas.Remove(sensorData);
            db.SaveChanges();

            return Ok(sensorData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SensorDataExists(int id)
        {
            return db.SensorDatas.Count(e => e.id == id) > 0;
        }
    }
}