using AutoMapper;
using DAL.Entitys;
using MovieStore.Models;

namespace MovieStore.Hellper
{
    public class MappintProFile : Profile
    {
        public MappintProFile()
        {
            CreateMap<Genre , GenreVM> ().ReverseMap();
            CreateMap<Movie, MovieVM> ().ReverseMap();  

        }
    }
}
