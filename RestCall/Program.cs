using System;

namespace RestCall
{
    class Program
    {
        static void Main(string[] args)
        {

            int result = Countries.GetCountries("un", 100090);
            Console.WriteLine(result);

            result = Countries.GetCountries("united", 200);
            Console.WriteLine(result);

            result = Countries.GetCountries("in", 1000000);
            Console.WriteLine(result);          
        }
    }
}
