namespace test.Api.StudentRout
{
    public interface StudentProtocol
	{
        List<Student> GetAllStudents();
        Student Get(String id);
        Student Create(Student student);
        void Update(string id, Student strudent);
        void Remove(string id);
	}
}

