using System;
using test.Api.StudentRout;

using MongoDB.Driver;
using test.Connection;

namespace test
{
	public class StudentRoute: StudentProtocol
    {
        private readonly IMongoCollection<Student> _student;

        public StudentRoute(StudentConnection conneection,IMongoClient client)
		{
            var database = client.GetDatabase(conneection.DatabaseName);

            _student = database.GetCollection<Student>(conneection.StudentsCollectionName);
		}

        public Student Create(Student student)
        {
            _student.InsertOne(student);
            return student;
        }

        public Student Get(String id)
        {
            var students = _student.Find(student => student.Id == id).FirstOrDefault();
            return students;
        }

        public List<Student> GetAllStudents()
        {
            var users = _student.Find(student => true).ToList();
            return users;
        }

        public void Remove(string id)
        {
            _student.DeleteOne(student => student.Id == id);
        }

        public void Update(string id, Student strudent)
        {
            _student.ReplaceOne(data => data.Id == id, strudent);
        }
    }
}

