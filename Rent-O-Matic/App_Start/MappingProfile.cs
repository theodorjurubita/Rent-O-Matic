using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Rent_O_Matic.DTOs;
using Rent_O_Matic.Models;
using Rent_O_Matic.ViewModels;

namespace Rent_O_Matic.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {

            //Domain to Dto
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<Store, StoreDto>();
            Mapper.CreateMap<Car, CarDto>();

            //Dto to domain
            Mapper.CreateMap<CustomerDto, Customer>()
                .ForMember(c=>c.Id,opt=>opt.Ignore());
            Mapper.CreateMap<StoreDto, Store>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            Mapper.CreateMap<CarDto, Car>()
                .ForMember(c => c.Id, opt => opt.Ignore());
        }
    }
}