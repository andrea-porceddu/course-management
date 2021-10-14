using CourseManagement.Model;
using CourseManagement.Model.Data;

namespace CourseManagement
{
    class Program
    {
        static void Main(string[] args)
        {
          IRepository repo = new LocalRepository();
          // IRepository repo = new DatabaseRepository();
          CourseService cs = new CourseService(repo);
          UserInterface ui = new UserInterface(cs);
          ui.Start(); 
        }

    }
    
}

/*
creare progetto class library dotnet core 5.0
dotnet tool install --global dotnet-ef
nuget package sql server
nuget package design
da terminale cartella progetto class library
dotnet ef dbcontext scaffold "Server=localhost; User Id=sa; Password=1Secure*Password; Database=db_nome" --context CourseContext --context-dir EntityFrameworkData --output-dir Entities Microsoft.EntityFrameworkCore.SqlServer

migration (creare migrazione chiamata iniziale , genera codice c# che crea le tabelle sul db)
dotnet ef migrations add initial
dotnet database update
*/