using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomersManagment.Models
{
    public class Company
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        public int NoOfEmployees { get; set; }
        public IList<Contact> Contacts { get; set; }
    }
}
