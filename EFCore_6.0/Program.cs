using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreWithAutoMapper.Dto;

namespace EFCoreWithAutoMapper
{
    class Program
    {
        private static MapperConfiguration mapperConfiguration = null;
        private static IMapper mapper = null;

        static void Main(string[] args)
        {
            mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Student, StudentDto>();
                cfg.CreateMap<Class, ClassDto>();
            });

            mapper = mapperConfiguration.CreateMapper();

            PrintQueryResult(Use_Map());
            PrintQueryResult(Use_ProjectTo());
        }

        static IEnumerable<StudentDto> Use_Map()
        {
            PrintCallerName();

            using (var db = new StudentDbContext())
            {
                return db.Set<Student>()
                            .Include(s => s.Class)

                            // Hmm. Related classes are not fetched. IAutoMapper.Map() does not work this case. The Class reference is always null.
                            // Probably, I need a special configuration for this kind of relationship?
                            //.Select(student => mapper.Map<StudentDto>(student))

                            .Select(s => new StudentDto()
                            {
                                Name = s.Name,
                                Class = new ClassDto()
                                {
                                    Name = s.Class.Name
                                }
                            })

                            .ToList();
            }
        }

        static IList<StudentDto> Use_ProjectTo()
        {
            PrintCallerName();

            using (var db = new StudentDbContext())
            {
                return db.Set<Student>().ProjectTo<StudentDto>(mapperConfiguration).ToList();
            }
        }

        static void PrintCallerName([System.Runtime.CompilerServices.CallerMemberName] string caller = "")
        {
            Console.WriteLine();
            Console.WriteLine("====================");
            Console.WriteLine(caller);
            Console.WriteLine("====================");
        }

        static void PrintQueryResult(IEnumerable<StudentDto> studentDtos)
        {
            Console.WriteLine("Query result:");
            foreach(var s in studentDtos)
            {
                Console.WriteLine($"Student name {s.Name} -> class {s.Class.Name}");
            };
        }
    }
}
