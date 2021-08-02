using CustomersManagment.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomersManagment.Requests
{
    public class CompanyRequest
    {
        public Company GetModel()
        {
            return new Company
            {
                Name = Name,
                NoOfEmployees = NoOfEmployees,
                Contacts = new List<Contact>()
            };
        }

        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        public int NoOfEmployees { get; set; }
        public IList<int> Contacts { get; set; }
    }
}
