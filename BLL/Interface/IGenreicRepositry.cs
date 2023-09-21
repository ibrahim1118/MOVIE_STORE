using DAL.Spacifaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IGenreicRepositry<T> where T : class
    {
        public  Task<int> AddAsync(T item);

        public Task<T?> GetbyidWithApacAsync(ISpacifaction<T> spac);

        public Task<T?> GetbyidAsync(int id);
        public Task<IEnumerable<T>> GetAllAsync(ISpacifaction<T> spac);  

        public Task<int> DeleteAsync(T item);

        public  Task<int> UpdateAsync(T item);

    }
}
