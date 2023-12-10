using System;
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

        public CourseRoute(CourseConnection conneection, IMongoClient client)
		{
            var database = client.GetDatabase(conneection.DatabaseName);

            _course = database.GetCollection<Courses>(conneection.CoursesCollectionName);
        }

        public Courses create(Courses course)
        {
            _course.InsertOne(course);
            return course;
        }

        public List<Courses> getAllCourses()
        {
            var courses = _course.Find(course => true).ToList();
            return courses;
        }

        public Courses getCourse(string id)
        {
            var course = _course.Find(course => course.Id == id).FirstOrDefault();
            return course;
        }
    }
}

