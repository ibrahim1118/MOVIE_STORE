using BLL.Interface;
using DAL.Data;
using DAL.Spacifaction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Implementation
{
    public class GenreicRepositry<T> : IGenreicRepositry<T> where T : class
    {
        private readonly AppDbContext _context;

        public GenreicRepositry(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(T item)
        {
           await _context.Set<T>().AddAsync(item);
          return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(T item)
        {
            _context.Set<T>().Remove(item); 
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(ISpacifaction<T> spac)
        {
            return  await CreateSpacifacQuere<T>.CreatQuery(_context.Set<T>() , spac).ToListAsync();    
        }

        public async Task<T?> GetbyidAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetbyidWithApacAsync(ISpacifaction<T> spac)
        {
            return await CreateSpacifacQuere<T>.CreatQuery(_context.Set<T>(), spac).FirstOrDefaultAsync();
        }


        public async Task<int> UpdateAsync(T item)
        {
            _context.Set<T>().Update(item);
            return await _context.SaveChangesAsync();
        }
    }
}
