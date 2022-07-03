# Introduction

The repository is about to compare the output when using Entity Framework Core with the AutoMapper method `IMapper.Map` and the extension `ProjectTo`.

What are they? Take a look here [IMapper.Map](https://docs.automapper.org/en/stable/Getting-started.html#how-do-i-use-automapper), [ProjectTo](https://docs.automapper.org/en/stable/Queryable-Extensions.html).

### **Database**
Given a database below:
- Table `Student`
  
  | Column | Description
  | --- | --- |
  | ID ||
  | Name | Name of student |
  | Grade | Average grade of student |
  | ClassId | Foregin key referencing to table `Class` |

- Table `Class`
  
  | Column | Description
  | --- | --- |
  | ID ||
  | Name | Name of class |
### **DTO**
Given DTO definitions below:
- DTO `StudentDto`
  | Property | Description
  | --- | --- |
  | Name | Name of student |
  | Class | A reference to `ClassDto` |

- DTO `ClassDto`
  | Property | Description
  | --- | --- |
  | Name | Name of student |

### **Mapper Configuration**

There're two different configurations for two comparisions.

#### 1. Complex selection
```csharp
mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Student, StudentDto>();
                cfg.CreateMap<Class, ClassDto>();
            });
```

#### 2. Simple selection
```csharp
mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Student, StudentDto>()
                      .ForMember(dst => dst.Class, opt => opt.Ignore());
            });
```

### **Comparison result**

#### 1. Complex selection
Execute an EF selection to query **the name of student** and **their class name** if any.

| Method | SQL execution count | Generated SQL Query | Note
| --- | --- | --- | --- |
| Without AutoMapper | 1 |  SELECT **[student].[Name], [class].[Name]** FROM [Student] AS [student] INNER JOIN [Class] AS [class] ON [student].[ClassId] = [class].[ID] | It's a base result to evaluate other methods.
| IMapper.Map | 1 | SELECT **[ID], [ClassId], [Grade], [Name]** FROM [Student] | - EF Core framework seems not to understand AutoMapper, it does not generate query to select the class name. <br/>- It selects more than what a DTO is defined.
| ProjectTo | 1 |  SELECT CAST(0 AS bit), **[class].[Name], [students].[Name]** FROM [Student] AS [student] INNER JOIN [Class] AS [class] ON [student].[ClassId] = [class].[ID] | It works well as expected.

#### 2. Simple selection
Execute an EF selection to query **only the name of student**

| Method | SQL execution count | Generated SQL Query | Note
| --- | --- | --- | --- |
| Without AutoMapper | 1 |  SELECT **[Name]** FROM [Student] | It's a base result to evaluate other methods.
| IMapper.Map | 1 |  SELECT **[ID], [ClassId], [Grade], [Name]** FROM [Student] | It selects more than what a DTO is defined.
| ProjectTo | 1 |  SELECT **[Name]** FROM [Student] | It works well as expected.

# Conclusion

- AutoMapper does not make EF Core to generate multiple unnecessary queries as we, sometimes, think. There's only one execution in all test cases.
- `IMapper.Map` generates uneffective queries in all test cases.
- `ProjectTo` produces expected results in all test cases.
- **Did I wrongly configure the mapper configuration so that `IMapper.Map` did not work properly?**

# Configuration

There're steps to quickly run the comparison.

- Create a database named `Student` on a SQL Server instance.
- Prepare a text file `connectionstring.txt` containing the database above, then put it in folder `EFCoreAndAutoMapper`
- On folder `EFCoreAndAutoMapper`, open a terminal and execute command `dotnet ef database update`.
- Launch project `EFCoreAndAutoMapper` and check out on the console screen.
