using System;
namespace test.Connection
{
	public class CoursesConnectionSetting: CourseConnection
	{
        public string DatabaseName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string CoursesCollectionName { get; set; } = string.Empty;
    }
}

