using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

[BsonIgnoreExtraElements]
public class Student
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("phone")]
    public string Phone { get; set; }

    [BsonElement("createdAt")]
    public String CreatedAt { get; set; } // Consider using DateTime instead of string

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("gender")]
    public bool Gender { get; set; }
}