using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Spacifaction
{
    public class Spacifaction<T> : ISpacifaction<T> where T : class
    {
        public Expression<Func<T, bool>> where { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new  List<Expression<Func<T, object>>>();

        public  int  Skip { get; set; }
      
        public int Take { get; set; }
        public bool IsPagingtion { get; set; }

        public Spacifaction(Expression<Func<T, bool>> where)
        {
            this.where = where;
        }
        public Spacifaction()
        {

        }
    }
}
