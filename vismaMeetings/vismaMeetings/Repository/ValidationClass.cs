using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using vismaMeetings.Models;

namespace vismaMeetings.Repository
{
    public class ValidationClass
    {
        
        public bool StringEqualityValidation(string? v, string? name)
        {
            string normalized1 = Regex.Replace(v, @"\s", "");
            string normalized2 = Regex.Replace(name, @"\s", "");

            bool stringEquals = String.Equals(
                normalized1,
                normalized2,
                StringComparison.OrdinalIgnoreCase);
            return stringEquals;
        }

        public bool CheckIfStringContains(string? text, string? comparison)
        {
            return  text.IndexOf(comparison, StringComparison.OrdinalIgnoreCase) >= 0;
        }


        public DateTime EndDateInput(DateTime startDate)
        {
            Console.WriteLine("Input duration of the meeting in minutes");
            double minutes = 0;
            double.TryParse(Console.ReadLine(), out minutes);
            return startDate.AddMinutes(minutes);
        }

        public DateTime DateInputValidation()
        {
            Console.WriteLine("Input Date string using YYYY-MM-DD HH:MM:SS template");
            bool validDate = false;
            DateTime dateValue = DateTime.MaxValue;
            while (!validDate)
            {
                string dateString = Console.ReadLine();
                if (DateTime.TryParse(dateString, out dateValue))
                    validDate = true;
                else
                    Console.WriteLine("Invalid date input");
            }
            return dateValue;
        }

    }
}
