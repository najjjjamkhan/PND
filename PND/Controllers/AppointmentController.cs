using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PND.Models;
using System.Text;

namespace PND.Controllers
{
    public class AppointmentController : Controller
    {
        private string url = "https://localhost:7022/api/Appointment/";
        private string url1 = "https://localhost:7022/api/Patient/";
        private HttpClient client = new HttpClient();

        [HttpGet]
        public IActionResult Index()
        {
            List<Appointment> appointments = new List<Appointment>();
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Appointment>>(result);
                if (data != null)
                {
                    appointments = data;
                }
            }
            return View(appointments);
        }

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}
        [HttpGet]
        public IActionResult Create(int id)
        {
            Patient Apt = new Patient();
            Appointment appointment = new Appointment();
            HttpResponseMessage response = client.GetAsync(url1 + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Patient>(result);
                if (data != null)
                {
                ViewBag.MyData = data;
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(Appointment Apt)
        {
            string data = JsonConvert.SerializeObject(Apt);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["insert_message"] = "Appointment Added ..";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Appointment Apt = new Appointment();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Appointment>(result);
                if (data != null)
                {
                    Apt = data;
                }
            }
            return View(Apt);
        }
        [HttpPost]
        public IActionResult Edit(Appointment Apt)
        {
            string data = JsonConvert.SerializeObject(Apt);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(url + Apt.ApID, content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["Update_message"] = "Appointment Updated ..";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            Appointment Apt = new Appointment();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Appointment>(result);
                if (data != null)
                {

                    Apt = data;
                }
            }
            return View(Apt);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Appointment Apt = new Appointment();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Appointment>(result);
                if (data != null)
                {
                    Apt = data;
                }
            }
            return View(Apt);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {

            HttpResponseMessage response = client.DeleteAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["Delete_message"] = "Appointment Deleted ..";
                return RedirectToAction("Index");
            }
            return View();
        }



    }
}
