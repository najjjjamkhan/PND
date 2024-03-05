using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PNDApi.Models;

namespace PNDApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PndContext context;

        public PatientController(PndContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<patient>>> GetPatientDetails()
        {
            var data = await context.Patients.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<patient>> GetPatientDetailsById(int id)
        {
            var pct = await context.Patients.FindAsync(id);
            if (pct == null)
            {
                return NotFound();
            }
            return pct;
        }
        
        [HttpGet("GetPatientDetailsBymail/{mail}")]
        public async Task<ActionResult<patient>> GetPatientDetailsBymail(string mail)
        {
            var pct =  context.Patients.Where(p => p.EmailAddress == mail).FirstOrDefault();
            if (pct == null)
            {
                return NotFound();
            }
            return pct;
        }

        [HttpPost]
        public async Task<ActionResult<patient>> AddPatient(patient pct)
        {
            await context.Patients.AddAsync(pct);
            await context.SaveChangesAsync();
            return Ok(pct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<patient>> UpdatePatientDetails(int id, patient pct)
        {
            if (id != pct.PatientID)
            {
                return BadRequest();

            }
            context.Entry(pct).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(pct);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<patient>> DeletePatientDetailsById(int id)
        {
            var pct = await context.Patients.FindAsync(id);
            if (pct == null)
            {
                return NotFound();
            }
            context.Patients.Remove(pct);
            await context.SaveChangesAsync();
            return Ok(pct);
        }


    }
}

    

