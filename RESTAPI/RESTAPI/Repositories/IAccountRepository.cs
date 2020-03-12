//Cameron Low
//Distributed Applications
//Assignment 2
using RESTAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAPI.Repositories
{
    public interface IAccountRepository
    {
        void Add(Account item);
        void Delete(Guid ID);
        IQueryable<Account> GetAll();
        Account GetSingle(Guid ID);
        bool Save();
        void Update(Account item);
    }
}
