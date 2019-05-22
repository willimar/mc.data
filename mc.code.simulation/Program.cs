using mc.cript;
using mc.provider.sqlserver.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace mc.code.simulation
{
    class Program
    {
        static void Main(string[] args)
        {
            var value = Cryptographer.Encrypt("Willimar Augusto Rocha", "dGVzdGU=");
            Console.WriteLine(value);
            Console.WriteLine(Cryptographer.Decrypt(value, "dGVzdGU="));

            Console.ReadKey();
        }
    }
}
