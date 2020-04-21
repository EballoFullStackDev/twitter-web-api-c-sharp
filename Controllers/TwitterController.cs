using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwitterApi.Models;

namespace TwitterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwitterController : ControllerBase
    {
        private readonly TweetContext _context;

        public TwitterController(TweetContext context)
        {
            _context = context;
        }

        // GET: api/Twitter
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TweetItem>>> GetTweetItem()
        {
            return await _context.TweetItem.ToListAsync();
        }

        // GET: api/Twitter/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TweetItem>> GetTweetItem(long id)
        {
            var tweetItem = await _context.TweetItem.FindAsync(id);

            if (tweetItem == null)
            {
                return NotFound();
            }

            return tweetItem;
        }

        // PUT: api/Twitter/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTweetItem(long id, TweetItem tweetItem)
        {
            if (id != tweetItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(tweetItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TweetItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Twitter
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TweetItem>> PostTweetItem(TweetItem tweetItem)
        {
            if(tweetItem.Description.Length <= 50)
            {
            _context.TweetItem.Add(tweetItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTweetItem", new { id = tweetItem.Id }, tweetItem);
            }
            else 
            {
              return Problem("Your tweet was too short. Must be shorter than 50 Characters.",null ,null ,"Too short");
            }
        }

        // DELETE: api/Twitter/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TweetItem>> DeleteTweetItem(long id)
        {
            var tweetItem = await _context.TweetItem.FindAsync(id);
            if (tweetItem == null)
            {
                return NotFound();
            }

            _context.TweetItem.Remove(tweetItem);
            await _context.SaveChangesAsync();

            return tweetItem;
        }

        private bool TweetItemExists(long id)
        {
            return _context.TweetItem.Any(e => e.Id == id);
        }
    }
}
