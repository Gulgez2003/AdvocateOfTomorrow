﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities.Concrete
{
    public interface IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
    }
}
