using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCore3.Dto;
using EFCore3;
using Microsoft.EntityFrameworkCore;

namespace EFCore3
{
    internal class ComplexSelection
    {
        private static MapperConfiguration mapperConfiguration = null;
        private static IMapper mapper = null;

        public static void Run()
        {
            Utility.PrintCallerName("Complex selection");

            mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Student, StudentDto>();
                cfg.CreateMap<Class, ClassDto>();
            });

            mapper = mapperConfiguration.CreateMapper();

            Utility.PrintQueryResult(No_AutoMapper());
            Utility.PrintQueryResult(Use_Map());
            Utility.PrintQueryResult(Use_ProjectTo());
        }

        static IEnumerable<StudentDto> No_AutoMapper()
        {
            Utility.PrintCallerName();

            using (var db = new StudentDbContext())
            {
                return db.Set<Student>()
                            .Include(s => s.Class)

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

        static IEnumerable<StudentDto> Use_Map()
        {
            Utility.PrintCallerName();

            using (var db = new StudentDbContext())
            {
                return db.Set<Student>()
                            .Include(s => s.Class)

                            // Hmm. Related classes are not fetched. IAutoMapper.Map() does not work this case. The Class reference is always null.
                            // Probably, I need a special configuration for this kind of relationship?
                            .Select(student => mapper.Map<StudentDto>(student))

                            .ToList();
            }
        }

        static IList<StudentDto> Use_ProjectTo()
        {
            Utility.PrintCallerName();

            using (var db = new StudentDbContext())
            {
                return db.Set<Student>().ProjectTo<StudentDto>(mapperConfiguration).ToList();
            }
        }
    }
}
