
using System;
using test.Model;

namespace test.Api.Studentroute
{
	public interface CoursesProtocol
	{
		Courses create(Courses course);
		List<Courses> getAllCourses();
		Courses getCourse(string id);
	}
}

