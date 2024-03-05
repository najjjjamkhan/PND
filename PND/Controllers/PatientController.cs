using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PND.Models;
using System.Text;

namespace PND.Controllers
{
    [Authorize(Roles = "admin")]
    public class PatientController : Controller
    {
        
            private string url = "https://localhost:7022/api/Patient/";

        


        private HttpClient client = new HttpClient();

       
        [HttpGet]
            public IActionResult PIndex()
            {
                List<Patient> Patients = new List<Patient>();
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<List<Patient>>(result);
                    if (data != null)
                    {
                        Patients = data;
                    }
                }
                return View(Patients);
            }


        [Authorize(Roles = "admin")]
        [HttpGet]
            public IActionResult AddPatient()
            {
                return View();
            }

            [HttpPost]
            public IActionResult AddPatient(Patient pct)
            {
                string data = JsonConvert.SerializeObject(pct);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["insert_message"] = "Patient Added ..";
                    return RedirectToAction("PIndex");
                }
                return View();
            }
        [Authorize(Roles = "admin")]
        [HttpGet]
            public IActionResult Edit(int id)
            {
                Patient pct = new Patient();
                HttpResponseMessage response = client.GetAsync(url + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Patient>(result);
                    if (data != null)
                    {
                        pct = data;
                    }
                }
                return View(pct);
            }
        [Authorize(Roles = "admin")]
        [HttpPost]
            public IActionResult Edit(Patient pct)
            {
                string data = JsonConvert.SerializeObject(pct);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(url + pct.PatientID, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["Update_message"] = "Patient Updated ..";
                    return RedirectToAction("PIndex");
                }
                return View();
            }
        

        [HttpGet]
        public IActionResult Details(int id)
        {
            Patient pct = new Patient();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Patient>(result);
                if (data != null)
                {
                    pct = data;
                }
            }
            return View(pct);
        }
    
            [Authorize(Roles = "admin")]
        [HttpGet]
            public IActionResult Delete(int id)
            {
                Patient pct = new Patient();
                HttpResponseMessage response = client.GetAsync(url + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Patient>(result);
                    if (data != null)
                    {
                        pct = data;
                    }
                }
                return View(pct);
            }
        [Authorize(Roles = "admin")]

        [HttpPost, ActionName("Delete")]
            public IActionResult DeleteConfirmed(int id)
            {

                HttpResponseMessage response = client.DeleteAsync(url + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["Delete_message"] = "Patient Deleted ..";
                    return RedirectToAction("PIndex");
                }
                return View();
            }




        }
    }

