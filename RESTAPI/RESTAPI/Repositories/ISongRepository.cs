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
    public interface ISongRepository
    {
        void Add(Song item);
        void Delete(Guid ID);
        IQueryable<Song> GetAll();
        Song GetSingle(Guid ID);
        bool Save();
        void Update(Song item);
    }
}
