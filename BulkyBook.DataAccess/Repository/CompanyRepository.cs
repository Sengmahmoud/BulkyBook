using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context):base(context)
        {
            this._context = context;
        }
        public void Update(Company company)
        {
            var objFromDb = _context.Companies.FirstOrDefault(s => s.Id == company.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = company.Name;
                objFromDb.streetAddress = company.streetAddress;
                objFromDb.State = company.State;
                objFromDb.phoneNumber = company.phoneNumber;
                objFromDb.PostalCode = company.PostalCode;
                objFromDb.City = company.City;
                objFromDb.IsAuthorizedCopmaany = company.IsAuthorizedCopmaany;
               
            }
        }
    }
}
