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
| IMapper.Map | 1 | SELECT **[ID], [ClassId], [Grade], [Name]** FROM [Student] | It selects more than what a DTO is defined.
| ProjectTo | 1 |  SELECT CAST(0 AS bit), **[class].[Name], [students].[Name]** FROM [Student] AS [student] INNER JOIN [Class] AS [class] ON [student].[ClassId] = [class].[ID] | It works well as expected.

#### 2. Simple selection
Execute an EF selection to query **only the name of student**

| Method | SQL execution count | Generated SQL Query | Note
| --- | --- | --- | --- |
| Without AutoMapper | 1 |  SELECT **[Name]** FROM [Student] | It's a base result to evaluate other methods.
| IMapper.Map | 1 |  SELECT **[ID], [ClassId], [Grade], [Name]** FROM [Student] | It selects more than what a DTO is defined.
| ProjectTo | 1 |  SELECT **[Name]** FROM [Student] | It works well as expected.

# Conclusion

- According to available test cases, AutoMapper does not cause multiple SQL connections/queries for each selection.
- `IMapper.Map` generates uneffective queries in all test cases. It turns out that mixing `IMapper.Map` with EF is a wrong practice. `IMapper.Map` is not designed for dealing with Queryable sources such as Entity Framework. [There's a guy making the same mistake.](https://github.com/AutoMapper/AutoMapper/discussions/3779)
- `ProjectTo` produces expected results in all test cases. It's the right tool when working with EF. Check it out [here](https://docs.automapper.org/en/stable/Queryable-Extensions.html).

# Configuration

There're steps to run the comparison.

- Create a database named `Student` on a SQL Server instance.
- Prepare a text file `connectionstring.txt` containing the database above, then put it in folder `EFCoreAndAutoMapper`
- On folder `EFCoreAndAutoMapper`, open a terminal and execute command `dotnet ef database update` to insert dummy data.
- Launch the project and check out the output on the console screen.
