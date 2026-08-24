using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class SubjectService
    {
        SubjectRepo repo;
        Mapper mapper;

        public SubjectService(SubjectRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public List<SubjectDTO> Get()
        {
            var data = repo.Get();
            return mapper.Map<List<SubjectDTO>>(data);
        }

        public SubjectDTO Get(int id)
        {
            var data = repo.Get(id);
            return mapper.Map<SubjectDTO>(data);
        }

        public bool Create(SubjectDTO s)
        {
            var mapped = mapper.Map<Subject>(s);
            return repo.Create(mapped);
        }

        public bool Update(SubjectDTO s)
        {
            var mapped = mapper.Map<Subject>(s);
            return repo.Update(mapped);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}
