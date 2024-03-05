using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PND.Models;
using System.Text;

namespace PND.Controllers
{
    public class DoctorController : Controller
    {
        private string url = "https://localhost:7022/api/Doctors/";
        private HttpClient client = new HttpClient();

        [HttpGet]
        public IActionResult Index()
        {
            List<Doctor> Doctors = new List<Doctor>(); 
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Doctor>>(result);
                if(data != null)
                {
                    Doctors=data;
                }
            }
            return View(Doctors);
        }

        [HttpGet]
        public IActionResult AddDoctor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDoctor(Doctor dtr)
        {
            string data = JsonConvert.SerializeObject(dtr);
            StringContent content = new StringContent(data,Encoding.UTF8,"application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["insert_message"] = "Doctor Added ..";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Doctor dtr = new Doctor();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode) 
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Doctor>(result);
                if (data != null)
                {
                    dtr = data;
                }
            }
            return View(dtr);
        }
        [HttpPost]
        public IActionResult Edit(Doctor dtr)
        {
            string data = JsonConvert.SerializeObject(dtr);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(url + dtr.Dr_ID, content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["Update_message"] = "Doctor Updated ..";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            Doctor dtr = new Doctor();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Doctor>(result);
                if (data != null)
                {
                    dtr = data;
                }
            }
            return View(dtr);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Doctor dtr = new Doctor();
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Doctor>(result);
                if (data != null)
                {
                    dtr = data;
                }
            }
            return View(dtr);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            
            HttpResponseMessage response = client.DeleteAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["Delete_message"] = "Doctor Deleted ..";
                return RedirectToAction("Index");
            }
            return View();
        }



    }
}
