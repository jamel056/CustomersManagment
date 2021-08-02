using CustomersManagment.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomersManagment.Requests
{
    public class ContactRequest
    {
        public Contact GetModel()
        {
            return new Contact
            {
                Name = Name,
                Company = new List<Company>()
            };
        }

        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        public IList<int> Company { get; set; }
    }
}
