using System;

namespace TestingMongoDb.models
{
    public abstract class EntityBase
    {
        public string Identity { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
