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
    public class MatchesController : ApiController
    {
        private ScoutContext db = new ScoutContext();

        // GET: api/Matches
        public IEnumerable<Match> GetMatches()
        {
            List<Match> matches = db.Matches.Include(m => m.Participants).ToList<Match>();

            return matches;
        }

        // GET: api/Matches/5
        [ResponseType(typeof(Match))]
        public async Task<IHttpActionResult> GetMatch(int id)
        {
            //Match match = await ((DbSet<Match>)db.Matches.Include(m => m.Participants)).FindAsync(id);
            Match match = db.Matches.Include(m => m.Participants).FirstOrDefault(m => m.MatchId == id);
            if (match == null)
            {
                return NotFound();
            }

            return Ok(match);
        }

        // PUT: api/Matches/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMatch(int id, Match match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != match.MatchId)
            {
                return BadRequest();
            }

            db.Entry(match).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.MatchExists(id))
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

        // PUT: api/Matches/Participants/5
        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("api/matches/participants/{id}")]
        public async Task<IHttpActionResult> PutMatchParticipant(int id, MatchParticipant matchParticipant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != matchParticipant.MatchParticipantId)
            {
                return BadRequest();
            }

            db.Entry(matchParticipant).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.MatchParticipantExists(id))
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

        // POST: api/Matches
        [ResponseType(typeof(Match))]
        public async Task<IHttpActionResult> PostMatch(Match match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Matches.Add(match);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = match.MatchId }, match);
        }

        // DELETE: api/Matches/5
        [ResponseType(typeof(Match))]
        public async Task<IHttpActionResult> DeleteMatch(int id)
        {
            Match match = await ((DbSet<Match>)db.Matches).FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            db.Matches.Remove(match);
            await db.SaveChangesAsync();

            return Ok(match);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MatchExists(int id)
        {
            return db.Matches.Count(e => e.MatchId == id) > 0;
        }

        private bool MatchParticipantExists(int id)
        {
            return db.MatchParticipants.Count(e => e.MatchParticipantId == id) > 0;
        }
    }
}