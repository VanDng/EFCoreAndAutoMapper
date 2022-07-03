using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCore6.Dto;

namespace EFCore6
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
