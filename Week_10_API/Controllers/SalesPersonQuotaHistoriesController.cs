#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Week_10_API.Data;
using Week_10_API.Models;

namespace Week_10_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesPersonQuotaHistoriesController : ControllerBase
    {
        private readonly Adventureworks2016Context _context;

        public SalesPersonQuotaHistoriesController(Adventureworks2016Context context)
        {
            _context = context;
        }

        // GET: api/SalesPersonQuotaHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesPersonQuotaHistory>>> GetSalesPersonQuotaHistories()
        {
            return await _context.SalesPersonQuotaHistories.ToListAsync();
        }

        // GET: api/SalesPersonQuotaHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesPersonQuotaHistory>> GetSalesPersonQuotaHistory(int id)
        {
            var salesPersonQuotaHistory = await _context.SalesPersonQuotaHistories.FindAsync(id);

            if (salesPersonQuotaHistory == null)
            {
                return NotFound();
            }

            return salesPersonQuotaHistory;
        }

        // PUT: api/SalesPersonQuotaHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesPersonQuotaHistory(int id, SalesPersonQuotaHistory salesPersonQuotaHistory)
        {
            if (id != salesPersonQuotaHistory.BusinessEntityId)
            {
                return BadRequest();
            }

            _context.Entry(salesPersonQuotaHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesPersonQuotaHistoryExists(id))
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

        // POST: api/SalesPersonQuotaHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SalesPersonQuotaHistory>> PostSalesPersonQuotaHistory(SalesPersonQuotaHistory salesPersonQuotaHistory)
        {
            _context.SalesPersonQuotaHistories.Add(salesPersonQuotaHistory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SalesPersonQuotaHistoryExists(salesPersonQuotaHistory.BusinessEntityId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSalesPersonQuotaHistory", new { id = salesPersonQuotaHistory.BusinessEntityId }, salesPersonQuotaHistory);
        }

        // DELETE: api/SalesPersonQuotaHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesPersonQuotaHistory(int id)
        {
            var salesPersonQuotaHistory = await _context.SalesPersonQuotaHistories.FindAsync(id);
            if (salesPersonQuotaHistory == null)
            {
                return NotFound();
            }

            _context.SalesPersonQuotaHistories.Remove(salesPersonQuotaHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalesPersonQuotaHistoryExists(int id)
        {
            return _context.SalesPersonQuotaHistories.Any(e => e.BusinessEntityId == id);
        }
    }
}
