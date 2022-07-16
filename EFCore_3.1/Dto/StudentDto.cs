using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore3.Dto
{
    public class StudentDto
    {
        public string Name { get; set; }
        public ClassDto Class { get; set; }
    }
}
