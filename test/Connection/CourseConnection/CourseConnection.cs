using System;
namespace test.Connection
{
	public interface CourseConnection
	{
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CoursesCollectionName { get; set; }
    }
}

