using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCore6.Dto;
using EFCore6;

namespace EFCore6
{
    internal class SimpleSelection
    {
        private static MapperConfiguration mapperConfiguration = null;
        private static IMapper mapper = null;

        public static void Run()
        {
            Utility.PrintCallerName("Simple selection");

            mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Student, StudentDto>()
                        .ForMember(dst => dst.Class, opt => opt.Ignore());
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
                                Name = s.Name
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
