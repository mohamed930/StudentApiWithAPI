using System;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace test.Model
{
    [BsonIgnoreExtraElements]
    public class Courses
	{
		[BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("img")]
        public string Image { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("price")]
        public string Price { get; set; }

        [BsonElement("language")]
        public string Lang { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<String>? Students { get; set; }
    }


    public class CourseWithStudents : Courses
    {
        public List<Student> StudentsData { get; set; }
    }
}

