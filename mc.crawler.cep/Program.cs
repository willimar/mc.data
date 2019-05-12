using mc.cep.service;
using mc.cep.service.Providers;
using mc.core.data.Repository.Register;
using mc.core.domain.register.Entity.Person;
using mc.core.mongo.Context;
using mc.core.mongo.Repository.Register;
using mc.core.service.Register;
using mc.navigator;
using mc.navigator.domain.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Linq;

namespace mc.crawler.cep
{
    class Program
    {
        static void Main(string[] args)
        {
            var navigator = new NavigatorService();
            var correios = new Viacep();
            var cepService = new CepService<Address>(navigator, correios);
            var sqlServerContext = new mc.core.data.Context.DataContext();
            var cepPieces = new CepPieces(new CityRepository(sqlServerContext), 
                    new StateRepository(sqlServerContext), 
                    new CountryRepository(sqlServerContext));
            var crawler = new ExtractCep(cepService, cepPieces);
            var mongoRepository = new AddressRepository(new DataContext(27017, "localhost", "mctest", string.Empty, string.Empty));
            var addressService = new AddressService(mongoRepository);

            for (int i = 1000; i < 10000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    var cep = string.Concat(i.ToString("00000"), j.ToString("000"));
                    Console.WriteLine(string.Format("CEP: {0}", cep));

                    var address = crawler.GetAddress(cep);
                    crawler.AdjustFieldValues(address);

                    //check there is in mongodb
                    var mongoAddress = addressService.GetData(e => e.Equals(address));

                    if (!mongoAddress.Any())
                    {
                        addressService.AppenData(address);
                    }
                }
            }
        }
    }
}
