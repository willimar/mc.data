using System;
using System.Collections.Generic;
using System.Text;

namespace mc.core.domain.register.ToTools
{
    public class MySqlCepImport
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Endereco { get; set; }
        public string EnderecoCompleto { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Estado { get; set; }
        public string Uf { get; set; }
        public string Pais { get; set; }
        public string PaisSigla { get; set; }
    }
}
