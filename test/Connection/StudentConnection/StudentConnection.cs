using System;
namespace test
{
	public interface StudentConnection
	{
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string StudentsCollectionName { get; set; }
    }
}

