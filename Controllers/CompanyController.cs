using CustomersManagment.Models;
using CustomersManagment.Requests;
using CustomersManagment.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CustomersManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public ActionResult<List<Company>> GetAll() =>
            _companyService.GetAll();

        [HttpGet("{id:length(24)}", Name = "GetCompany")]
        public ActionResult<Company> Get(int id)
        {
            var company = _companyService.Get(id);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        [HttpPost]
        public ActionResult<Company> Create(CompanyRequest request)
        {
            var response = _companyService.Create(request);
            if (response == null) return Ok("the same company is already found");

            return Ok(response);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(int id, CompanyRequest request)
        {
            var companyFromDb = _companyService.Get(id);

            if (companyFromDb == null)
            {
                return NotFound();
            }

            _companyService.Update(id, request);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(int id)
        {
            var company = _companyService.Get(id);

            if (company == null)
            {
                return NotFound();
            }

            _companyService.Remove(company.Id);

            return NoContent();
        }
    }
}
