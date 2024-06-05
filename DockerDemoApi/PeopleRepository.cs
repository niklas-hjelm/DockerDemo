using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace DockerDemoApi;

public class PeopleRepository
{
    IMongoCollection<Person> _peopleCollection;

    public PeopleRepository()
    {
        var client = new MongoClient("mongodb://localhost:5001/");
        var database = client.GetDatabase("PeopleDb");
        _peopleCollection = database.GetCollection<Person>("People", new MongoCollectionSettings() {AssignIdOnInsert = true});
    }

    public async Task<IEnumerable<Person>> GetAllPeople()
    {
        return await _peopleCollection.Find(p => true).ToListAsync();
    }

    public async Task AddPerson(string name)
    {
        await _peopleCollection.InsertOneAsync(new Person {Name = name});
    }
}

public class Person
{
    [BsonRepresentation(BsonType.ObjectId), BsonId]
    public string Id { get; set; }

    public string Name { get; set; }
}