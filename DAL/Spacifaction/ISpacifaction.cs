using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Spacifaction
{
    public interface ISpacifaction<T> where T : class
    {
        Expression<Func<T , bool>> where { get; set; }

        List<Expression<Func<T,object>>> Includes { get; set; }

        public bool IsPagingtion { get; set; }
        public int Skip { get; set; }

        public int Take { get; set; }
    }
}
