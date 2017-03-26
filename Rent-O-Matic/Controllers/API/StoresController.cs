using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Rent_O_Matic.ViewModels;
using Rent_O_Matic.DTOs;
using Rent_O_Matic.Models;

namespace Rent_O_Matic.Controllers.API
{
    public class StoresController : ApiController
    {
        private ApplicationDbContext _context;

        public StoresController()
        {
            _context=new ApplicationDbContext();
        }

        //GET /api/stores
        public IEnumerable<StoreDto> GetStores()
        {
            return _context.Stores.ToList().Select(Mapper.Map<Store, StoreDto>);
        }

        //POST /api/stores
        public IHttpActionResult CreateStore(StoreDto storeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var store = Mapper.Map<StoreDto, Store>(storeDto);
            _context.Stores.Add(store);
            _context.SaveChanges();

            storeDto.Id = store.Id;
            return Created(new Uri(Request.RequestUri + "/" + store.Id), storeDto);
        }

        //GET /api/stores/1
        public IHttpActionResult GetStore(int id)
        {
            var store = _context.Stores.SingleOrDefault(c => c.Id == id);
            if (store == null)
                return NotFound();
            return Ok(Mapper.Map<Store,StoreDto>(store));
        }

        //PUT /api/stores/1
        [HttpPut]
        public void PutStore(int id, StoreDto storeDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var store = _context.Stores.SingleOrDefault(c => c.Id == id);
            if (store == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            Mapper.Map(storeDto, store);
            _context.SaveChanges();
        }

    }
}
