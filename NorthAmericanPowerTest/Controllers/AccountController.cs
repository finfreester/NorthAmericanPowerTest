using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NorthAmericanPower.Domain;
using System.Text;

namespace NorthAmericanPower.Web.Applications.Controllers
{
    public class AccountController : Controller
    {

        protected HttpClient _client;

        //The URL of the WEB API Service
        private string url = "http://localhost:50598/";

        public AccountController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: All Accounts
        public async Task<ActionResult> Index()
        {

            HttpResponseMessage response = await _client.GetAsync("api/Account/"); //API controller name
            if (response.IsSuccessStatusCode)
            {

                var responseData = response.Content.ReadAsStringAsync().Result;
                var Accounts = JsonConvert.DeserializeObject<List<UserAccount>>(responseData);

                return View(Accounts);
            }
            return View("_Error");

        }

        // Get: Account
        public ActionResult Create()
        {
            return View(new UserAccount());
        }

        // Post: Account
        [HttpPost]
        public async Task<ActionResult> Create(UserAccount value)
        {
            if (ModelState.IsValid)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync("api/Account/", content); //API controller name
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            else
            {
                return View(value);
            }

            return View("_Error");

        }

        // Get: Account/5
        public async Task<ActionResult> Edit(int id)
        {

            HttpResponseMessage response = await _client.GetAsync(string.Format("api/Account/{0}/", id)); //API controller name
            if (response.IsSuccessStatusCode)
            {

                var responseData = response.Content.ReadAsStringAsync().Result;
                var Account = JsonConvert.DeserializeObject<UserAccount>(responseData);

                return View(Account);
            }
            return View("_Error");

        }

        // Update: Account/5
        [HttpPut]
        public async Task<ActionResult> Edit(int id, UserAccount value)
        {

            if (ModelState.IsValid)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PutAsync(string.Format("api/Account/{0}/",id), content); //API controller name
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            else
            {
                return View(value);
            }

            return View("_Error");

        }

        // ReadOnly View: Account/5
        public async Task<ActionResult> Details(int id)
        {

            HttpResponseMessage response = await _client.GetAsync(string.Format("api/Account/{0}/", id)); //API controller name
            if (response.IsSuccessStatusCode)
            {

                var responseData = response.Content.ReadAsStringAsync().Result;
                var Account = JsonConvert.DeserializeObject<UserAccount>(responseData);

                return View(Account);
            }
            return View("_Error");

        }

        // Delete: api/Account/5
        public async Task<ActionResult> Delete(int id)
        {

            HttpResponseMessage response = await _client.DeleteAsync(string.Format("api/Account/{0}/", id)); //API controller name
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("_Error");

        }
        
    }
}