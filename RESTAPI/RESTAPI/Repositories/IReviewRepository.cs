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
    public interface IReviewRepository
    {
        void Add(Review item);
        void Delete(Guid ID);
        IQueryable<Review> GetAll();
        Review GetSingle(Guid ID);
        bool Save();
        void Update(Review item);
    }
}
