using EvolentAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Formatting;

namespace EvolentAssignment.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        string Baseurl = System.Configuration.ConfigurationManager.AppSettings["ContactApiUrl"];

        [Route("Contact/Index")]
        public async Task<ActionResult> Index()
        {
            List<Contact> EmpInfo = new List<Contact>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/Contacts/AllContacts");

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    EmpInfo = JsonConvert.DeserializeObject<List<Contact>>(EmpResponse);

                }

            }
            return View(EmpInfo);
        }
        public ActionResult SaveContact(Contact contact)
        {
            return View();
        }
        [HttpPost]
        [Route("Contact/UpdateContact")]
        public ActionResult UpdateContact(tblContact contact)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ContactApiUrl"]);

                //HTTP POST
                //var postTask = client.PostAsJsonAsync<tblContact>("AddContact", cnt);
                var postTask = client.PostAsync<tblContact>("UpdateContact", contact, new JsonMediaTypeFormatter());
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public ActionResult DeleteContact(int contactId)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(Contact contact)
        {

            var cnt = new tblContact();
            cnt.FirstName = contact.FirstName;
            cnt.LastName = contact.LastName;
            cnt.Status = contact.Status;
            cnt.Email = contact.Email;
            cnt.PhoneNumber = contact.PhoneNumber;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ContactApiUrl"]);

                //HTTP POST
                //var postTask = client.PostAsJsonAsync<tblContact>("AddContact", cnt);
                var postTask = client.PostAsync<tblContact>("AddContact", cnt, new JsonMediaTypeFormatter());
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(contact);
        }


        public ActionResult Edit(int id)
        {
            tblContact contact = null;

            using (var client = new HttpClient())
            {
                //object val = System.Configuration.ConfigurationManager.AppSettings["ContactApiUrl"];
                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ContactApiUrl"]);
                //HTTP GET
                var responseTask = client.GetAsync("GetContact?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<tblContact>();
                    readTask.Wait();

                    contact = readTask.Result;
                }
            }

            return View(contact);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ContactApiUrl"]);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("DeleteContact?id=" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}