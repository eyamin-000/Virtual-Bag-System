using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class HomeworkService
    {
        HomeworkRepo repo;
        Mapper mapper;

        public HomeworkService(HomeworkRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public List<HomeworkDTO> Get()
        {
            var data = repo.Get();
            return mapper.Map<List<HomeworkDTO>>(data);
        }

        public HomeworkDTO Get(int id)
        {
            var data = repo.Get(id);
            return mapper.Map<HomeworkDTO>(data);
        }

        public bool Create(HomeworkDTO h)
        {
            var mapped = mapper.Map<Homework>(h);
            return repo.Create(mapped);
        }

        public bool Update(HomeworkDTO h)
        {
            var mapped = mapper.Map<Homework>(h);
            return repo.Update(mapped);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}
