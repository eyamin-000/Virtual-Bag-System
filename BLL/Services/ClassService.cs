using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class ClassService
    {
        ClassRepo repo;
        Mapper mapper;

        public ClassService(ClassRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public List<ClassDTO> Get()
        {
            var data = repo.Get();
            return mapper.Map<List<ClassDTO>>(data);
        }

        public ClassDTO Get(int id)
        {
            var data = repo.Get(id);
            return mapper.Map<ClassDTO>(data);
        }

        public bool Create(ClassDTO c)
        {
            var mapped = mapper.Map<Class>(c);
            return repo.Create(mapped);
        }

        public bool Update(ClassDTO c)
        {
            var mapped = mapper.Map<Class>(c);
            return repo.Update(mapped);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}
