using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using test.Api.Studentroute;
using test.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace test.Controllers
{

    public class CourseRequest
    {
        public string courseId { get; set; }
        public string studentId { get; set; }
    }

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
        public async Task<IActionResult> GetAsync()
        {
            List<CourseWithStudents> result = await course.GetAllCoursesAsync();

            return Ok(response.BaseResponse(result));
        }

        // GET: api/values
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpecificAsync(string id)
        {
            if (id == null || id == "<string>")
            {
                return Ok(response.BaseResponse(403, "Error in parameters"));
            }
            else
            {
                if (IsHex24Digits(id))
                {
                    var course = await this.course.getCourse(id);
                    return Ok(response.BaseResponse(course));
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

        // PUT: api/values
        [HttpPut]
        public IActionResult Put([FromBody] CourseRequest request)
        {
            if (request == null)
            {
                return Ok(response.BaseResponse(403, "Error in build parameters"));
            }
            else
            {
                if (!IsHex24Digits(request.courseId) || !IsHex24Digits(request.studentId))
                {
                    return Ok(response.BaseResponse(403, "Error in build parameters"));
                }
                else
                {
                    var result = course.getCourse(request.courseId);
                    if (result == null)
                    {
                        return Ok(response.BaseResponse(404, "Course not found!!"));
                    }
                    else
                    {
                        course.buyTheCourse(request.courseId, request.studentId);
                        return Ok(response.BaseResponse(201, "course bought successfully"));
                    }
                }
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

