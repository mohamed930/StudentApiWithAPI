using System;
namespace test.Connection
{
	public class StudentConnectionSetting: StudentConnection
    {
        public string DatabaseName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string StudentsCollectionName { get; set; } = string.Empty;
    }
}

