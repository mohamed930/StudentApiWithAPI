using System;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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
    }
}

