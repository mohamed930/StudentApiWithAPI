using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using test.Api.Studentroute;
using test.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace test.Controllers
{
    [Route("api/[controller]")]
    public class CoursesController : Controller
    {
        readonly CoursesProtocol course;

        public CoursesController(CoursesProtocol course)
        {
            this.course = course;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            List<Courses> result = course.getAllCourses();

            return Ok(response.BaseResponse(result));
        }

        // GET: api/values
        [HttpGet("{id}")]
        public IActionResult Get(String id)
        {
            if (id == null || id == "<string>")
            {
                return Ok(response.BaseResponse(403, "Error in parameters"));
            }
            else
            {
                if (IsHex24Digits(id))
                {
                    return Ok(response.BaseResponse(course.getCourse(id)));
                }
                else
                {
                    return Ok(response.BaseResponse(403, "Error in parameters"));
                }
                
            }
        }

        // POST: api/values
        [HttpPost]
        public IActionResult Post([FromBody] Courses course)
        {
            if (course == null)
            {
                return Ok(response.BaseResponse(403, "Error in parameters"));
            }
            else
            {
                this.course.create(course);
                return Ok(response.BaseResponse(course));
            }
        }


        private bool IsHex24Digits(string input)
        {
            // Regular expression pattern to match 24-digit hexadecimal string
            Regex regex = new Regex("^[0-9a-fA-F]{24}$");

            // Check if the input matches the pattern
            return regex.IsMatch(input);
        }
    }
}

