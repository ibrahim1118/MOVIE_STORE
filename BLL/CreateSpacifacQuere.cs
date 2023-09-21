using DAL.Spacifaction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public static class CreateSpacifacQuere<T> where T : class
    {
        public static IQueryable<T> CreatQuery(IQueryable<T> query, ISpacifaction<T> spac)
        {
            var quer = query;
            if (spac.where is not null)
                quer = quer.Where(spac.where);
            if (spac.IsPagingtion)
            quer = quer.Skip(spac.Skip).Take(spac.Take);
            quer = spac.Includes.Aggregate(quer, (a, b) => a.Include(b));
            return quer; 
        }
    }
}
