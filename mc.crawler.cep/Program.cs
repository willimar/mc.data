namespace mc.crawler.cep
{
    class Program
    {
        static void Main()
        {
            //var navigator = new NavigatorService();
            //var correios = new Correios();
            //var cepService = new CepService<Address>(navigator, correios);
            //var sqlServerContext = new mc.core.data.Context.DataContext();
            //var cepPieces = new CepPieces(new CityRepository(sqlServerContext), 
            //        new StateRepository(sqlServerContext), 
            //        new CountryRepository(sqlServerContext));
            //var crawler = new ExtractCep(cepService, cepPieces);
            //var mongoRepository = new AddressRepository(new DataContext(27017, "localhost", "mctest", string.Empty, string.Empty));
            //var addressService = new AddressService(mongoRepository);
            //var flag = 0;

            //for (int i = 1000; i < 10000; i++)
            //{
            //    for (int j = 0; j < 1000; j++)
            //    {
            //        if (flag.Equals(25))
            //        {
            //            flag = 0;
            //            Thread.Sleep(20000);
            //        }
            //        else
            //        {
            //            flag++;
            //            Thread.Sleep(10000);
            //        }

            //        var cep = string.Concat(i.ToString("00000"), j.ToString("000"));

            //        correios.Cep = cep;

            //        var address = crawler.GetAddress(cep);

            //        if (address is null)
            //        {
            //            Console.WriteLine(string.Format("CEP: {0} => Not found", cep));
            //            continue;
            //        }

            //        crawler.AdjustFieldValues(address);

            //        //check there is in mongodb
            //        var mongoAddress = addressService.GetData(e => e.PostalCode.Equals(address.PostalCode));
                    
            //        if ((mongoAddress is null) || !mongoAddress.Any())
            //        {
            //            if (!(address.PostalCode is null))
            //            {
            //                addressService.AppenData(address);
            //                Console.WriteLine(string.Format("CEP: {0} => Inserted", cep));
            //            }
            //            else
            //            {
            //                Console.WriteLine(string.Format("CEP: {0} => Not found", cep));
            //            }
            //        }
            //    }
            //}
        }
    }
}
