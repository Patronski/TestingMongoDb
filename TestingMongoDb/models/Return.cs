using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace TestingMongoDb.models
{
    [BsonIgnoreExtraElements]
    public class Return : MongoEntity
    {
        public string Status { get; set; }

        public int StatusId { get; set; }

        public string Country { get; set; }

        public string TrackingId { get; set; }

        public string TrackingUrl { get; set; }

        public string DetailsLink { get; set; }

        public int TotalBoxCount { get; set; }

        public string ReturnType { get; set; }

        public int PalletsCount { get; set; }

        public string ExternalReferenceNumber { get; set; }

        public string ReturnNumber { get; set; }

        public int? ReturnReasonId { get; set; }

        public string ReturnReason { get; set; }

        public int TrackingUpdateAttempts { get; set; }

        public bool IsFinalTrackingEventReached { get; set; }
    }
}
