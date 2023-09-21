using DAL.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Spacifaction
{
    public class MovieSpacifaction :Spacifaction<Movie>
    {
        public MovieSpacifaction(int id):base(m=>m.Id==id)
        {
            Includes.Add(m => m.Genre); 
        }
        public MovieSpacifaction(string? temr="" , int CurrentPage=0 , int PageSize =0)
            :base(m=>(string.IsNullOrEmpty(temr)||m.Title.ToLower().Contains(temr.ToLower())))
        {
                Includes.Add(m => m.Genre);
            if (PageSize > 0)
            { 
                IsPagingtion= true;
                Skip = (CurrentPage - 1) * PageSize;
                Take = PageSize;
            } 

        }
    }
}
