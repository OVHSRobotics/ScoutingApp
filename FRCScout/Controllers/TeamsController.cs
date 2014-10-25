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
using FRCScout.Models;

namespace FRCScout.Controllers
{
    public class TeamsController : ApiController
    {
        private ScoutContext db = new ScoutContext();

        // GET: api/Teams
        public IEnumerable<Team> GetTeams()
        {
            return db.Teams.ToList<Team>();
        }

        // GET: api/Teams/5
        [ResponseType(typeof(Team))]
        public async Task<IHttpActionResult> GetTeam(int id)
        {
            //Team team = await ((DbSet<Team>)db.Teams).FindAsync(id);
            Team team = db.Teams.Include(t => t.Robots).Include("Robots.Pictures").FirstOrDefault(t => t.Number == id);
            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        // PUT: api/Teams/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTeam(int id, Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != team.Number)
            {
                return BadRequest();
            }

            db.Entry(team).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.TeamExists(id))
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

        // PUT: api/Teams/Robots/5
        [ResponseType(typeof(void))]
        [Route("api/teams/robots/{id}")]
        public async Task<IHttpActionResult> PutTeamRobot(int id, Robot robot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != robot.RobotId)
            {
                return BadRequest();
            }

            foreach (Picture picture in robot.Pictures)
            {
                if (picture.PictureId == 0)
                {
                    picture.Robot = robot;
                    db.Pictures.Add(picture);
                }
            }

            db.Entry(robot).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.RobotExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return this.Ok<Robot>(robot);
        }

        // POST: api/Teams
        [ResponseType(typeof(Team))]
        public async Task<IHttpActionResult> PostTeam(Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Teams.Add(team);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = team.Number }, team);
        }

        // POST: api/Teams/Robot
        [ResponseType(typeof(Robot))]
        [HttpPost]
        [Route("api/teams/robots")]
        public async Task<IHttpActionResult> PostTeamRobot(Robot robot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Robots.Add(robot);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { controller = "teams", id = robot.RobotId }, robot);
        }

        // DELETE: api/Teams/5
        [ResponseType(typeof(Team))]
        public async Task<IHttpActionResult> DeleteTeam(int id)
        {
            Team team = await ((DbSet<Team>)db.Teams).FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            db.Teams.Remove(team);
            await db.SaveChangesAsync();

            return Ok(team);
        }

        // DELETE: api/Teams/Robots/5
        [ResponseType(typeof(Robot))]
        [HttpDelete]
        [Route("api/teams/robots/{id}")]
        public async Task<IHttpActionResult> DeleteTeamRobot(int id)
        {
            Robot robot = await ((DbSet<Robot>)db.Robots).FindAsync(id);
            if (robot == null)
            {
                return NotFound();
            }

            IEnumerable<Picture> pictures = db.Pictures.Where(p => p.Robot.RobotId == robot.RobotId);
            UploadController uploadController = new UploadController();
            foreach (Picture picture in pictures)
            {
                string path = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/"), picture.FileName);
                if (System.IO.File.Exists(path))
                {
                    await Task.Run(() => System.IO.File.Delete(path));
                }

                db.Pictures.Remove(picture);
            }

            db.Robots.Remove(robot);
            Task dbTask = db.SaveChangesAsync();
            await dbTask;

            return Ok(robot);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamExists(int id)
        {
            return db.Teams.Count(e => e.Number == id) > 0;
        }

        private bool RobotExists(int id)
        {
            return db.Robots.Count(e => e.RobotId == id) > 0;
        }
    }
}