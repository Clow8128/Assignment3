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
    public class ReviewRepository : IReviewRepository
    {
        private readonly SongContext _context;

        public ReviewRepository(SongContext con)
        {
            _context = con;
        }

        public IQueryable<Review> GetAll()
        {
            return _context.Reviews;
        }

        public Review GetSingle(Guid ID)
        {
            return _context.Reviews.FirstOrDefault(x => x.ID == ID);
        }

        public void Add(Review item)
        {
            _context.Reviews.Add(item);
        }

        public void Delete(Guid ID)
        {
            Review Review = GetSingle(ID);
            _context.Reviews.Remove(Review);
        }

        public void Update(Review item)
        {
            _context.Reviews.Update(item);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
