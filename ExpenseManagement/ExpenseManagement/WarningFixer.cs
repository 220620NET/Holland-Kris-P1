using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFrontEnd
{
    public class WarningFixer
    {
        public WarningFixer() { }
        /// <summary>
        /// This method ensures a non-null parsed decimal reading from the Console
        /// </summary>
        /// <returns>Decimal parsed from a string</returns>
        public decimal Parsing()
        {
            bool fix = true;
            while (fix)
            {
                try
                {
                    string? s;
                    try
                    {
                        s = Console.ReadLine();
                        if (s == null)
                        {
                            throw new ArgumentNullException();
                        }
                    }
                    catch (ArgumentNullException)
                    {
                        throw new FormatException();
                    }
                    decimal k = decimal.Parse(s);             
                    return k;
                }
                catch (FormatException)
                {
                    fix = true;
                }
                catch (ArgumentNullException)
                {
                    fix= true;
                }
            }
            return 0;
        }
    }
}
