using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using MusicStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicStoreAPI.Controllers
{
    [Route("api/[controller]")]
    public class AlbumsController : Controller
    {
        // db
        private MusicStoreModel db;

        // Constructor
        public AlbumsController(MusicStoreModel db)
        {
            this.db = db;
        }

        // GET: api/albums
        [HttpGet]
        public IEnumerable<Album> Get()
        {
            return db.Albums.OrderBy(a => a.Title).ToList();
        }

        // GET api/albums/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Album album = await db.Albums.SingleOrDefaultAsync(a => a.AlbumId == id);

            if (album == null)
            {
                return NotFound();
            }

            return Ok(album);
        }

        // POST api/albums
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Albums.Add(album);
            await db.SaveChangesAsync();

            // return 201 - created
            return CreatedAtAction("Get", new { id = album.AlbumId }, album);
        }

        // PUT api/albums/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(album).State = EntityState.Modified;
            await db.SaveChangesAsync();

            // return 202 - updated
            return AcceptedAtAction("Get", new { id = album.AlbumId }, album);
        }

        // DELETE api/albums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Album album = await db.Albums.FirstOrDefaultAsync(a => a.AlbumId == id);

            if (album == null)
            {
                return NotFound();
            }

            db.Albums.Remove(album);
            await db.SaveChangesAsync();

            return Ok(album);
        }
    }
}
