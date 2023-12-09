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

        private object BaseResponse<T>(T obj)
        {
            var response = new
            {
                data = obj,
                itemsCount = 1,
                success = true,
                statusCode = 200,
                message = "Success"
            };

            return response;
        }

        private object BaseResponse<T>(List<T> obj)
        {
            var response = new
            {
                data = obj,
                itemsCount = obj.Count,
                success = true,
                statusCode = 200,
                message = "Success"
            };

            return response;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            List<Student> result = student.GetAllStudents();

            return Ok(BaseResponse(result));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(String id)
        {
            Console.WriteLine($"The Id is {id}");
            if (id == "<string>" || id == null)
            {
                var response = new
                {
                    data = (object)null,
                    success = false,
                    statusCode = 403,
                    message = "Failed to fetch Id"
                };

                return BadRequest(response);
            }
            else
            {
                var result = student.Get(id);

                if (result == null)
                {
                    return NotFound($"Student with Id = {id} not found");
                }
                else
                {
                    return Ok(BaseResponse(result));
                }
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
            if (student == null)
            {
                var response = new
                {
                    data = (object)null,
                    success = false,
                    statusCode = 403,
                    message = "Failed to fetch data from body"
                };

                return BadRequest(response);
            }
            else
            {
                var createdStudent = this.student.Create(student);

                return Ok(BaseResponse(createdStudent));
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
                    var response = new
                    {
                        data = (object)null,
                        success = false,
                        statusCode = 403,
                        message = "Failed to fetch data from body"
                    };

                    return BadRequest(response);
                }
                else
                {
                    value.Id = id;
                    student.Update(id, value);
                    return Ok(BaseResponse(value));
                }
            }
            else
            {
                return NotFound($"Student with Id = {id} not found");
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
                var response = new
                {
                    data = (Object)null,
                    itemsCount = 1,
                    success = true,
                    statusCode = 200,
                    message = "Deleted Successfully"
                };

                return Ok(response);
            }
            else
            {
                return NotFound($"Student with Id = {id} not found");
            }
        }
    }
}

