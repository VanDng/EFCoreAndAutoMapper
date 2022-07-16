using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCore3.Dto;

namespace EFCore3
{
    class Program
    {
        static void Main(string[] args)
        {
            ComplexSelection.Run();
            SimpleSelection.Run();
        }
    }
}
