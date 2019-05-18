﻿using HtmlAgilityPack;
using mc.core.domain.register.Entity;
using mc.core.domain.register.Entity.Person;
using mc.navigator.domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace mc.cep.service.Providers
{
    public class Correios : IProviderService<Address>
    {
        public string Cep { get; set; }

        public Method Method => Method.post;
        public Dictionary<string, string> Form { get { return GetForm(); } }

        public Correios()
        {
            //this._cep = cep;
        }

        public string GetUrl(params string[] values)
        {
            return @"http://www.buscacep.correios.com.br/sistemas/buscacep/resultadoBuscaEndereco.cfm";
        }

        private Dictionary<string, string> GetForm()
        {
            var result = new Dictionary<string, string>(1);
            result["CEP"] = this.Cep;
            return result;
        }

        public Address Parser(string value)
        {
            if (value.Contains("<p>CEP NAO ENCONTRADO</p>"))
            {
                return null;
            }

            HtmlDocument doc = new HtmlDocument();
           
            doc.LoadHtml(value);
            var nodes = doc.DocumentNode.SelectNodes("//table[contains(@class,'tmptabela')]");
            if (nodes is null)
            {
                return null;
            }
            else
            {
                var trs = nodes[0].SelectNodes("//tr[contains(@class,'')]");
                if (trs is null)
                {
                    return null;
                }
                else
                {
                    var resultNode = trs[1];
                    var rua = HttpUtility.HtmlDecode(resultNode.ChildNodes[1].InnerText).Trim();
                    var bairro = HttpUtility.HtmlDecode(resultNode.ChildNodes[3].InnerText).Trim();
                    var cidadeInfo = HttpUtility.HtmlDecode(resultNode.ChildNodes[5].InnerText).Trim();
                    var resultCep = HttpUtility.HtmlDecode(resultNode.ChildNodes[7].InnerText).Trim();
                    var city_estado = cidadeInfo.Split("/");

                    if (resultCep.Replace("-", "").Equals(this.Cep.Replace("-", "")))
                    {
                        return new Address()
                        {
                            PostalCode = this.Cep,
                            PublicPlace = rua,
                            District = bairro,
                            City = new City()
                            {
                                Name = city_estado[0],
                                State = new State()
                                {
                                    Initials = city_estado[1]
                                }
                            }
                        };
                    }
                }
            }
            return null;
        }
    }
}
