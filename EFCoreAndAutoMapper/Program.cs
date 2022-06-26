using System;
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
            });

            mapper = mapperConfiguration.CreateMapper();

            Use_Map();
            Use_ProjectTo();
        }

        static void Use_Map()
        {
            PrintCallerName();

            using (var db = new StudentDbContext())
            {
                var studentDtos = db.Set<Student>().Select(student => mapper.Map<StudentDto>(student)).ToList();
            }
        }

        static void Use_ProjectTo()
        {
            PrintCallerName();

            using (var db = new StudentDbContext())
            {
                var studentDtos = db.Set<Student>().ProjectTo<StudentDto>(mapperConfiguration).ToList();
            }
        }

        static void PrintCallerName([System.Runtime.CompilerServices.CallerMemberName] string caller = "")
        {
            Console.WriteLine();
            Console.WriteLine("====================");
            Console.WriteLine(caller);
            Console.WriteLine("====================");
        }
    }
}
