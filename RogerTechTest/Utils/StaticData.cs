using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogerTechTest.Utils
{
    public class StaticData
    {
        /// <summary>
        /// Najde nejvyšší orderNumber souboru, v případě, že nenajde žádný soubor vrací 0
        /// </summary>
        public static int GetMaxOrderNumber(string path, string date)
        {
            var files = Directory.GetFiles(path).Where(x => x.Contains($"articles_{date}"))
                                                .Select(Path.GetFileNameWithoutExtension);

            return  files.Any()
                    ? files.Select(x => int.Parse(x.Substring(x.LastIndexOf("_") + 1))).Max()
                    : 0;

        }

        /// <summary>
        /// Vrátí načtenou cestu z konzole zadanou uživatelem
        /// </summary>
        public static string GetPath(string str)
        {
            while(true)
            {
                Console.WriteLine(str);
                string path = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(path))
                {
                    Console.WriteLine("Cesta nezadána");
                    continue;
                }

                return path;
            }
        }
    }
}
