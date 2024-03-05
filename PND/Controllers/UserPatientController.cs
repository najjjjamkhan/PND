using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PND.Areas.Identity.Data;
using PND.Models;
using System.Text;


namespace PND.Controllers
{
    public class UserPatientController : Controller
    {
        private readonly SignInManager<PNDUser> signInManager;
        private readonly UserManager<PNDUser> userManager;
        private string url = "https://localhost:7022/api/Patient/";
        private string appointmentUrl = "https://localhost:7022/api/Appointment/";


        private HttpClient client = new HttpClient();

        public UserPatientController(SignInManager<PNDUser> SignInManager, UserManager<PNDUser> UserManager)
        {
            signInManager = SignInManager;
            userManager = UserManager;
        }

        [HttpGet]
        public IActionResult Details()
        
        {
            var mail = userManager.GetUserName(User);
            var patientDetails = new PatientViewModel();
            HttpResponseMessage response = client.GetAsync(url+ "GetPatientDetailsBymail/" + mail ).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Patient>(result);

                if (data != null)
                {
                    var appointments = new List<Appointment>();
                    var dd = appointmentUrl + "GetPatientAppointmets/" + data.PatientID;
                    HttpResponseMessage appointmentResponse = client.GetAsync(dd).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string appointmentResult = appointmentResponse.Content.ReadAsStringAsync().Result;
                        var appointmentData = JsonConvert.DeserializeObject<List<Appointment>>(appointmentResult);

                        if (appointmentData != null)
                        {
                            appointments = appointmentData;
                        }
                    }
                    patientDetails.Patient = data;
                    patientDetails.Appointments = appointments;
                    //pct = data;
                }
            }

            return View(patientDetails);
        }

        [HttpPost]
        public IActionResult GetPatient(int patientId)
        {
            var patientDetails = new PatientViewModel();
            HttpResponseMessage response = client.GetAsync(url + patientId).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Patient>(result);

                if (data != null)
                {
                    var appointments = new List<Appointment>();
                    var dd = appointmentUrl + "GetPatientAppointmets/" + data.PatientID;
                    HttpResponseMessage appointmentResponse = client.GetAsync(dd).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string appointmentResult = appointmentResponse.Content.ReadAsStringAsync().Result;
                        var appointmentData = JsonConvert.DeserializeObject<List<Appointment>>(appointmentResult);
                          
                        if (appointmentData != null)
                        {
                            appointments = appointmentData;
                        }
                    }
                    patientDetails.Patient = data;
                    patientDetails.Appointments = appointments;
                        //pct = data;
                }
            }
            return View("Details", patientDetails);
        }

    }
}
