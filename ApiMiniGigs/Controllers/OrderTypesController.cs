using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiMiniGigs.Models;

namespace ApiMiniGigs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderTypesController : ControllerBase
    {
        private readonly MiniGigsDBContext _context;

        public OrderTypesController(MiniGigsDBContext context)
        {
            _context = context;
        }

        // GET: api/OrderTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderType>>> GetOrderTypes()
        {
          if (_context.OrderTypes == null)
          {
              return NotFound();
          }
            return await _context.OrderTypes.ToListAsync();
        }

        // GET: api/OrderTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderType>> GetOrderType(int id)
        {
          if (_context.OrderTypes == null)
          {
              return NotFound();
          }
            var orderType = await _context.OrderTypes.FindAsync(id);

            if (orderType == null)
            {
                return NotFound();
            }

            return orderType;
        }

        // PUT: api/OrderTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderType(int id, OrderType orderType)
        {
            if (id != orderType.IdOrderType)
            {
                return BadRequest();
            }

            _context.Entry(orderType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderTypeExists(id))
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

        // POST: api/OrderTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderType>> PostOrderType(OrderType orderType)
        {
          if (_context.OrderTypes == null)
          {
              return Problem("Entity set 'MiniGigsDBContext.OrderTypes'  is null.");
          }
            _context.OrderTypes.Add(orderType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderTypeExists(orderType.IdOrderType))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrderType", new { id = orderType.IdOrderType }, orderType);
        }

        // DELETE: api/OrderTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderType(int id)
        {
            if (_context.OrderTypes == null)
            {
                return NotFound();
            }
            var orderType = await _context.OrderTypes.FindAsync(id);
            if (orderType == null)
            {
                return NotFound();
            }

            _context.OrderTypes.Remove(orderType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderTypeExists(int id)
        {
            return (_context.OrderTypes?.Any(e => e.IdOrderType == id)).GetValueOrDefault();
        }
    }
}
