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
    public class SongRepository : ISongRepository
    {
        private readonly SongContext _context;

        public SongRepository(SongContext con)
        {
            _context = con;
        }

        public IQueryable<Song> GetAll()
        {
            return _context.Songs;
        }

        public Song GetSingle(Guid ID)
        {
            return _context.Songs.FirstOrDefault(x => x.ID == ID);
        }

        public void Add(Song item)
        {
            _context.Songs.Add(item);
        }

        public void Delete(Guid ID)
        {
            Song Song = GetSingle(ID);
            _context.Songs.Remove(Song);
        }

        public void Update(Song item)
        {
            _context.Songs.Update(item);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
