using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NzWalks.Api.Controllers;
//https:/localhost:portnumber/api/students
[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    //Get: https:/localhost/portnumber/api/students 
    [HttpGet]
    public IActionResult GetAllStudents()
    {
        string[] studentNames = new string[] { "Sevinchbek", "Omadbek", "Maqsud", "Samandar", "Husniddin" };
        return Ok(studentNames);
    }
}
