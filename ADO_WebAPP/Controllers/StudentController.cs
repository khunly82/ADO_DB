using ADO_Console.Domain;
using ADO_Console.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ADO_WebAPP.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            SqlConnection connection = new SqlConnection("server=(localdb)\\MSSQLLocalDB;database=TFDemoADO;integrated security=true");
            StudentRepository repo = new StudentRepository(connection);

            return View(repo.Get());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            SqlConnection connection = new SqlConnection("server=(localdb)\\MSSQLLocalDB;database=TFDemoADO;integrated security=true");
            StudentRepository repo = new StudentRepository(connection);

            repo.Add(student);

            return RedirectToAction("Index");
        }
    }
}
