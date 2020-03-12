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
    public class AccountRepository : IAccountRepository
    {
        private readonly SongContext _context;

        public AccountRepository(SongContext con)
        {
            _context = con;
        }

        public IQueryable<Account> GetAll()
        {
            return _context.AccountSessions;
        }

        public Account GetSingle(Guid ID)
        {
            return _context.AccountSessions.FirstOrDefault(x => x.ID == ID);
        }

        public void Add(Account item)
        {
            _context.AccountSessions.Add(item);
        }

        public void Delete(Guid ID)
        {
            Account Account = GetSingle(ID);
            _context.AccountSessions.Remove(Account);
        }

        public void Update(Account item)
        {
            _context.AccountSessions.Update(item);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
