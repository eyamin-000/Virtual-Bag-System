using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class StudyActivityService
    {
        StudyActivityRepo repo;
        Mapper mapper;

        public StudyActivityService(StudyActivityRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public List<StudyActivityDTO> Get()
        {
            var data = repo.Get();
            return mapper.Map<List<StudyActivityDTO>>(data);
        }

        public StudyActivityDTO Get(int id)
        {
            var data = repo.Get(id);
            return mapper.Map<StudyActivityDTO>(data);
        }

        public bool Create(StudyActivityDTO s)
        {
            var mapped = mapper.Map<StudyActivity>(s);
            return repo.Create(mapped);
        }

        public bool Update(StudyActivityDTO s)
        {
            var mapped = mapper.Map<StudyActivity>(s);
            return repo.Update(mapped);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}
