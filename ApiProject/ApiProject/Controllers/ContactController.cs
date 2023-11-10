using ApiProject.Models;
using ApiProject.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private ContactRepository contactRepository;

        public ContactController(IMemoryCache memoryCache)
        {
            this.contactRepository = new ContactRepository(memoryCache);
        }

        public Contact[] Get()
        {
            return contactRepository.GetAllContacts();
        }
    }
}