using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using test.Api.StudentRout;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace test.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    { 

        readonly StudentProtocol student;

        public StudentController(StudentProtocol student)
        {
            this.student = student;
        }


        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            List<Student> result = student.GetAllStudents();

            return Ok(response.BaseResponse(result));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(String id)
        {
            Console.WriteLine($"The Id is {id}");
            if (id == "<string>" || id == null)
            { 
                return Ok(response.BaseResponse(403, "Failed to fetch Id"));
            }
            else
            {
                var result = student.Get(id);

                if (result == null)
                {
                    return Ok(response.BaseResponse(404, $"Student with Id = {id} not found"));
                }
                else
                {
                    return Ok(response.BaseResponse(result));
                }
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
            if (student == null)
            {
                return Ok(response.BaseResponse(403,"Failed to fetch data from body"));
            }
            else
            {
                var createdStudent = this.student.Create(student);

                return Ok(response.BaseResponse(createdStudent));
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<string> Put(String id, [FromBody]Student value)
        {
            var exsits = student.Get(id);

            if (exsits != null)
            {
                if (value == null)
                {
                    return Ok(response.BaseResponse(403, "Failed to fetch data from body"));
                }
                else
                {
                    value.Id = id;
                    student.Update(id, value);
                    return Ok(response.BaseResponse(value));
                }
            }
            else
            {
                return Ok(response.BaseResponse(404, $"Student with Id = {id} not found"));
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(String id)
        {
            var exsits = student.Get(id);

            if (exsits != null)
            {
                student.Remove(id);
                return Ok(response.BaseResponse(200, "Deleted Successfully"));
            }
            else
            {
                return Ok(response.BaseResponse(404, $"Student with Id = {id} not found"));
            }
        }
    }
}

