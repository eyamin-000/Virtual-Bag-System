using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class BookService
    {
        BookRepo repo;
        Mapper mapper;

        public BookService(BookRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public List<BookDTO> Get()
        {
            var data = repo.Get();
            return mapper.Map<List<BookDTO>>(data);
        }

        public BookDTO Get(int id)
        {
            var data = repo.Get(id);
            return mapper.Map<BookDTO>(data);
        }

        public bool Create(BookDTO b)
        {
            var mapped = mapper.Map<Book>(b);
            return repo.Create(mapped);
        }

        public bool Update(BookDTO b)
        {
            var mapped = mapper.Map<Book>(b);
            return repo.Update(mapped);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}
