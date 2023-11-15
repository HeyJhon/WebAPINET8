using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPINET8.Database;
using WebAPINET8.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPINET8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _context.Customers.ToListAsync();
            if(result==null)
                return NotFound();
            return Ok(result);
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult>  Get(int id)
        {
            var result = await _context.Customers.SingleOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return Ok(customer);
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer customer)
        {
            var customerInfo = _context.Customers.SingleOrDefault(x => x.Id == id);
            if(customerInfo == null)
                return NotFound();

            customerInfo.Name = customer.Name;
            customerInfo.Email = customer.Email;
            _context.Attach(customerInfo);
            await _context.SaveChangesAsync();

            return Ok(customerInfo);
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customerInfo = _context.Customers.SingleOrDefault(x => x.Id == id);
            if (customerInfo == null)
                return NotFound();
            _context.Customers.Remove(customerInfo);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
