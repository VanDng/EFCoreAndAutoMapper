using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCore6.Dto;

namespace EFCore6
{
    internal class Utility
    {
        public static void PrintCallerName([System.Runtime.CompilerServices.CallerMemberName] string caller = "")
        {
            Console.WriteLine();
            Console.WriteLine("====================");
            Console.WriteLine(caller);
            Console.WriteLine("====================");
        }

        public static void PrintQueryResult(IEnumerable<StudentDto> studentDtos)
        {
            Console.WriteLine("Query result:");
            foreach (var s in studentDtos)
            {
                Console.WriteLine($"Student name {s.Name} -> class name {(string.IsNullOrEmpty(s.Class?.Name) ? "<Null>" : s.Class.Name)}");
            };
        }
    }
}
