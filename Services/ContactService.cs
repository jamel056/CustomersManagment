using CustomersManagment.Models;
using CustomersManagment.Requests;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace CustomersManagment.Services
{
    public interface IContactService
    {
        List<Contact> GetAll();
        Contact Get(int id);
        Contact Create(ContactRequest request);
        void Update(int id, ContactRequest request);
        void Remove(int id);
    }

    public class ContactService : IContactService
    {
        private readonly IMongoCollection<Contact> _contact;
        private readonly IMongoCollection<Company> _company;

        public ContactService(ICustomersManagmentDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _contact = database.GetCollection<Contact>(settings.ContactCollectionName);
            _company = database.GetCollection<Company>(settings.CompanyCollectionName);
        }

        public List<Contact> GetAll() =>
            _contact.Find(contact => true).ToList();

        public Contact Get(int id) =>
            _contact.Find<Contact>(contact => contact.Id == id).FirstOrDefault();

        public Contact Create(ContactRequest request)
        {
            var contactFromDb = _contact.Find<Contact>(x => x.Name.Equals(request.Name)).FirstOrDefault();
            if (contactFromDb != null) return null;

            var contact = request.GetModel();
            contact.Id = GetAll().LastOrDefault() != null ? GetAll().LastOrDefault().Id + 1 : 0;
            foreach (var item in request.Company)
            {
                var company = _company.Find<Company>(contact => contact.Id == item).FirstOrDefault();
                if (company != null) contact.Company.Add(company);
            }

            _contact.InsertOne(contact);
            return contact;
        }

        public void Update(int id, ContactRequest request)
        {
            var contact = request.GetModel();
            contact.Id = id;
            foreach (var item in request.Company)
            {
                var company = _company.Find<Company>(contact => contact.Id == item).FirstOrDefault();
                if (company != null) contact.Company.Add(company);
            }
            _contact.ReplaceOne(contact => contact.Id == id, contact);
        }

        public void Remove(int id) =>
            _contact.DeleteOne(contact => contact.Id == id);

    }
}
