
using System;
using test.Model;

namespace test.Api.Studentroute
{
	public interface CoursesProtocol
	{
		Courses create(Courses course);
        Task<List<CourseWithStudents>> GetAllCoursesAsync();
        Task<CourseWithStudents> getCourse(string id);
		void buyTheCourse(string id, string studentId);
	}
}

