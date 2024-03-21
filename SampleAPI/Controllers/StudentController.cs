using ADOWrappers.Contacts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IGenericSqlDataReader _genericSqlDataReader;

        public StudentController(IConfiguration configuration, IGenericSqlDataReader genericSqlDataReader)
        {
            _configuration = configuration;
            _genericSqlDataReader = genericSqlDataReader;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string conn = _configuration.GetConnectionString("SampleConnection")!;
            var students = await _genericSqlDataReader.ExecuteReaderAsync<Student>(conn, "zsp_GetStudents", CommandType.StoredProcedure);
            return Ok(students);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {

            string conn = _configuration.GetConnectionString("SampleConnection")!;

            string query = "Select Id, FirstName, LastName FROM Student Where Id = @Id";
            SqlParameter[] parameters = new SqlParameter[]
        {
            new SqlParameter("@Id", SqlDbType.Int) { Value = Id },
			// Add other parameters as needed
		};
            var student = await _genericSqlDataReader.ExecuteReaderAsync<Student>(conn, query, CommandType.Text, parameters);
            if(student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
    }
}
