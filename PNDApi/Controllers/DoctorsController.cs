using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PNDApi.Models;

namespace PNDApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly PndContext context;

        public DoctorsController(PndContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Doctor>>> GetDoctorsDetails()
        {
            var data = await context.Doctors.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctorsDetailsById(int id)
        {
            var doctor= await context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return doctor;
        }

        [HttpPost]
        public async Task<ActionResult<Doctor>> AddDoctor(Doctor dtr)
        {
            await context.Doctors.AddAsync(dtr);
            await context.SaveChangesAsync();
            return Ok(dtr);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Doctor>> UpdateDoctorDetails(int id,Doctor dtr)
        {
            if (id != dtr.Dr_ID)
            {
                return BadRequest();

            }
            context.Entry(dtr).State= EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(dtr);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Doctor>> DeleteDoctorsDetailsById(int id)
        {
            var doctor = await context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            context.Doctors.Remove(doctor);
            await context.SaveChangesAsync();   
            return Ok(doctor);
        }


    }
}
