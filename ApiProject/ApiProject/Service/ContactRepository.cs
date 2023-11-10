using ApiProject.Models;
using System.Web;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace ApiProject.Service
{
    public class ContactRepository
    {
        private const string CacheKey = "ContactStore";
        private readonly IMemoryCache _memoryCache;

        public ContactRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;

            if (!_memoryCache.TryGetValue(CacheKey, out List<Contact> contacts))
            {
                contacts = new List<Contact>
                {
                    new Contact { Id = 1, Name = "Glenn Block" },
                    new Contact { Id = 2, Name = "Dan Roth" }
                };

                _memoryCache.Set(CacheKey, contacts);
            }
        }

        public Contact[] GetAllContacts()
        {
            if (_memoryCache.TryGetValue(CacheKey, out List<Contact> contacts))
            {
                return contacts.ToArray();
            }

            return new Contact[]
            {
                new Contact { Id = 0, Name = "Placeholder" }
            };
        }

        public bool SaveContact(Contact contact)
        {
            if (_memoryCache.TryGetValue(CacheKey, out List<Contact> contacts))
            {
                try
                {
                    contacts.Add(contact);
                    _memoryCache.Set(CacheKey, contacts);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

            return false;
        }
    }
}