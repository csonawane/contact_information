using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace ContactAPI.Controllers
{
    //[RoutePrefix("api/Contacts")]
    public class ContactsController : ApiController
    {
        OnlinepolingsystemEntities dbc = new OnlinepolingsystemEntities();
       
        // GET api/<controller>
        [HttpGet]
        [Route("api/Contacts/AllContacts")]
        public IEnumerable<tblContact> Get()
        {
            return dbc.tblContacts.ToList();
        }

        [HttpGet]
        [Route("api/Contacts/GetContact")]
        public tblContact GetContact(int id)
        {
            tblContact Contact = dbc.tblContacts.FirstOrDefault(x => x.Id == id);
            //return Contact;
            return Contact;
        }


        [HttpPost]
        [Route("api/Contacts/AddContact")]
        public IEnumerable<tblContact> post(tblContact tblcontact)
        {
            dbc.tblContacts.Add(tblcontact);
            dbc.SaveChanges();
            return dbc.tblContacts.ToList();

            //dbc.tblContacts.Add(new tblContact()
            //{
            //    FirstName= tblcontact.FirstName,
            //    LastName=tblcontact.LastName,
            //    Email = tblcontact.Email,
            //    PhoneNumber = tblcontact.PhoneNumber,
            //    Status = tblcontact.Status

            //});
            //dbc.SaveChanges();
            //return dbc.tblContacts.ToList();
        }

        // PUT api/<controller>/5
        [HttpPost]
        [Route("api/Contacts/UpdateContact")]
        public IEnumerable<tblContact> UpdateContact(tblContact tblcontact)
        {
           
            var contact = dbc.tblContacts.Where(x => x.Id == tblcontact.Id).FirstOrDefault();
            contact.FirstName = tblcontact.FirstName;
            contact.LastName = tblcontact.LastName;
            contact.Email = tblcontact.Email;
            contact.PhoneNumber = tblcontact.PhoneNumber;
            contact.Status = tblcontact.Status;
            dbc.SaveChanges();
            return dbc.tblContacts.ToList();

        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("api/Contacts/DeleteContact")]
        public void DeleteContact(int id)
        {
            var contact = dbc.tblContacts.Where(x => x.Id == id).FirstOrDefault();
            dbc.tblContacts.Remove(contact);
            dbc.SaveChanges();

        }
    }
}