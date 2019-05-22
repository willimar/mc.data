using mc.core.domain.register.Entity;
using mc.core.domain.register.Entity.Person;
using mc.provider.mysql.Context;
using mc.repository.cep;
using mc.repository.person;
using System;
using System.Linq;

namespace mc.crawler.cep
{
    class Program
    {
        static void Main()
        {
            using (var providerMySql = new DataContext(3306, "localhost", "mcdata", "userTest", "userTest"))
            {
                using (var source = new MySqlCepImportRepository(providerMySql))
                {
                    using (var providerMongo = new mc.provider.mongo.Context.DataContext(27017, "localhost", "mctest", string.Empty, string.Empty))
                    {
                        using (var target = new AddressRepository(providerMongo))
                        {
                            var ceps = source.GetData(x => true);

                            if (! ceps.Any())
                            {
                                return;
                            }
                            foreach (var item in ceps)
                            {
                                Console.WriteLine($"CEP: {item.Cep} - {item.EnderecoCompleto}");

                                using (var targetEntity = new Address()
                                {
                                    PublicPlace = item.Logradouro,
                                    PostalCode = item.Cep,
                                    FullStreeName = item.EnderecoCompleto,
                                    StreetName = item.Endereco,
                                    Complement = null,
                                    District = item.Bairro,
                                    Number = null,
                                    Status = core.domain.Entity.Status.Active,
                                    City = new City()
                                    {
                                        Status = core.domain.Entity.Status.Active,
                                        Code = null,
                                        Initials = null,
                                        Name = item.Cidade,
                                        State = new State()
                                        {
                                            Code = null,
                                            Initials = item.Uf,
                                            Name = item.Estado,
                                            Status = core.domain.Entity.Status.Active,
                                            Country = new Country()
                                            {
                                                Code = null,
                                                Initials = item.PaisSigla,
                                                Name = item.Pais,
                                                Status = core.domain.Entity.Status.Active
                                            }
                                        }
                                    }
                                })
                                {
                                    target.AppenData(targetEntity);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
