using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PNDApi.Models;

namespace PNDApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly PndContext context;

        public AppointmentController(PndContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Appointment>>> GetAppointmentDetails()
        {
            var data = await context.Appointment.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointmentDetailsById(int id)
        {
            var Apt = await context.Appointment.FindAsync(id);
            if (Apt == null)
            {
                return NotFound();
            }
            return Apt;
        }

        [HttpGet("GetPatientAppointmets/{patientId}")]
        public async Task<List<Appointment>> GetAppointmentDetailsByPatientId(int patientId)
        {
            var Apt = await context.Appointment.Where(x => x.PatientID == patientId).ToListAsync();
            if (Apt != null)
            {
                return Apt;
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Appointment>> Create(Appointment Apt)
        {
            await context.Appointment.AddAsync(Apt);
            await context.SaveChangesAsync();
            return Ok(Apt);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Appointment>> UpdateAppointmentDetails(int id, Appointment Apt)
        {
            if (id != Apt.ApID)
            {
                return BadRequest();

            }
           
            context.Entry(Apt).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(Apt);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Appointment>> DeleteAppointmentDetailsById(int id)
        {
            var Apt = await context.Appointment.FindAsync(id);
            if (Apt == null)
            {
                return NotFound();
            }
            context.Appointment.Remove(Apt);
            await context.SaveChangesAsync();
            return Ok(Apt);
        }


    }
}
