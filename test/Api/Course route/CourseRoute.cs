using System;
using MongoDB.Bson;
using MongoDB.Driver;
using test.Api.StudentRout;
using test.Api.Studentroute;
using test.Connection;
using test.Model;

namespace test.Api.Courseroute
{
	public class CourseRoute: CoursesProtocol
    {
        private readonly IMongoCollection<Courses> _course;
        private readonly IMongoCollection<Student> _student;


        public CourseRoute(StudentConnection student_connection,CourseConnection conneection, IMongoClient client)
		{
            var database = client.GetDatabase(conneection.DatabaseName);

            _course = database.GetCollection<Courses>(conneection.CoursesCollectionName);

            var database2 = client.GetDatabase(student_connection.DatabaseName);
            _student = database2.GetCollection<Student>(student_connection.StudentsCollectionName);
        }

        public void buyTheCourse(string id, string studentId)
        {
            var course = _course.Find(course => course.Id == id).FirstOrDefault();

            if (course.Students == null)
            {
                course.Students = new List<String>();
            }

            course.Students.Add(studentId);
            _course.ReplaceOne(data => data.Id == id, course);
        }

        public Courses create(Courses course)
        {
            _course.InsertOne(course);
            return course;
        }

        public async Task<List<CourseWithStudents>> GetAllCoursesAsync()
        {
            var coursesWithStudents = await _course.Aggregate()
                .Lookup(
                    foreignCollection: _student,
                    localField: c => c.Students,
                    foreignField: s => s.Id,
                    @as: (CourseWithStudents c) => c.StudentsData
                )
                .ToListAsync();

            return coursesWithStudents;

            //var courses = _course.Find(course => true).ToList();
            //return courses;
        }

        public async Task<CourseWithStudents> getCourse(string id)
        {
            var courseWithStudents = await _course.Aggregate()
            .Match(data => data.Id == id) // Match the specific course by its ID
            .Lookup(
                foreignCollection: _student,
                localField: c => c.Students,
                foreignField: s => s.Id,
                @as: (CourseWithStudents c) => c.StudentsData
            )
            .FirstOrDefaultAsync();

            return courseWithStudents;
        }
    }
}

