using CustomersManagment.Models;
using CustomersManagment.Requests;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace CustomersManagment.Services
{
    public interface ICompanyService
    {
        List<Company> GetAll();
        Company Get(int id);
        Company Create(CompanyRequest request);
        void Update(int id, CompanyRequest request);
        void Remove(int id);
    }

    public class CompanyService : ICompanyService
    {
        private readonly IMongoCollection<Company> _company;
        private readonly IMongoCollection<Contact> _contact;

        public CompanyService(ICustomersManagmentDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _company = database.GetCollection<Company>(settings.CompanyCollectionName);
            _contact = database.GetCollection<Contact>(settings.ContactCollectionName);
        }

        public List<Company> GetAll() =>
            _company.Find(company => true).ToList();

        public Company Get(int id) =>
            _company.Find<Company>(company => company.Id == id).FirstOrDefault();

        public Company Create(CompanyRequest request)
        {
            var companyFromDb = _company.Find<Company>(x => x.Name.Equals(request.Name)).FirstOrDefault();
            if (companyFromDb != null) return null;

            var company = request.GetModel();
            company.Id = GetAll().LastOrDefault() != null ? GetAll().LastOrDefault().Id + 1 : 0;
            foreach (var item in request.Contacts)
            {
                var contact = _contact.Find<Contact>(contact => contact.Id == item).FirstOrDefault();
                if (contact != null) company.Contacts.Add(contact);
            }
            _company.InsertOne(company);
            return company;
        }

        public void Update(int id, CompanyRequest request)
        {
            var company = request.GetModel();
            company.Id = id;
            foreach (var item in request.Contacts)
            {
                var contact = _contact.Find<Contact>(contact => contact.Id == item).FirstOrDefault();
                if (contact != null) company.Contacts.Add(contact);
            }
            _company.ReplaceOne(company => company.Id == id, company);
        }

        public void Remove(int id) =>
            _company.DeleteOne(company => company.Id == id);

    }
}
