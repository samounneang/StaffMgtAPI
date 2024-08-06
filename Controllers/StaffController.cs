using Microsoft.AspNetCore.Mvc;
using StaffMgtApi.Data;
using StaffMgtApi.Models;
using Microsoft.EntityFrameworkCore;

namespace StaffMgtApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly StaffMgtApiContext db;

        public StaffController(StaffMgtApiContext context)
        {
            db = context;
        }
        
        // GET: api/<StaffController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffModel>>> Get()
        {
            return await db.StaffModel.ToListAsync();
        }

        // GET api/<StaffController>/5
        [HttpGet("{StaffId}")]
        public async Task<ActionResult<StaffModel>> GetStaff(string StaffId)
        {
            var staff = await db.StaffModel.FirstOrDefaultAsync(s => s.StaffId == StaffId);
            if (staff == null)
            {
                return NotFound();
            }

            return Ok(staff);
        }


        // POST api/<StaffController>
        [HttpPost]
        public async Task<ActionResult<StaffModel>> CreateStaff(StaffModel staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingStaff = await db.StaffModel.FirstOrDefaultAsync(s => s.StaffId == staff.StaffId);
            if (existingStaff != null)
            {
                return Conflict(new { message = "Staff ID already exists." });
            }

            try
            {
                db.StaffModel.Add(staff);
                await db.SaveChangesAsync();

                return CreatedAtAction(nameof(GetStaff), new { staffid = staff.StaffId }, staff);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // PUT api/<StaffController>/5
        [HttpPatch("{StaffId}")]
        public async Task<IActionResult> UpdateStaff(string StaffId, StaffModel staff)
        {
            if (StaffId != staff.StaffId)
            {
                return BadRequest("Staff ID mismatch.");
            }

            // Check if the staff record exists
            var existingStaff =  db.StaffModel.FirstOrDefault(s => s.StaffId.Equals(StaffId));
            if (existingStaff == null)
            {
                return NotFound("Staff not found.");
            }

            existingStaff.FullName = staff.FullName;
            existingStaff.Birthday = staff.Birthday;
            existingStaff.Gender = staff.Gender;

            try
            {
                db.Entry(existingStaff).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(StaffId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return NoContent();
        }

        private bool StaffExists(string id)
        {
            return db.StaffModel.Any(e => e.StaffId == id);
        }


        // DELETE api/<StaffController>/5
        [HttpDelete("{StaffId}")]
        public async Task<IActionResult> DeleteStaff(string StaffId)
        {
            var staff = await db.StaffModel.FirstOrDefaultAsync(s => s.StaffId.Equals(StaffId));
            if (staff == null)
            {
                return NotFound();
            }

            db.StaffModel.Remove(staff);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return NoContent();
        }

    }
}