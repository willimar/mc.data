using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mc.api.user.Services
{
    public static class Consts
    {
        public const string PASS_PHRASE = "FALARNISSOVOUTECONTAR";
    }
    
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }
}
