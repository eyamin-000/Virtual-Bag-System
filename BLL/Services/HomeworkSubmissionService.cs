using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class HomeworkSubmissionService
    {
        HomeworkSubmissionRepo repo;
        Mapper mapper;

        public HomeworkSubmissionService(HomeworkSubmissionRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public List<HomeworkSubmissionDTO> Get()
        {
            var data = repo.Get();
            return mapper.Map<List<HomeworkSubmissionDTO>>(data);
        }

        public HomeworkSubmissionDTO Get(int id)
        {
            var data = repo.Get(id);
            return mapper.Map<HomeworkSubmissionDTO>(data);
        }

        public bool Create(HomeworkSubmissionDTO h)
        {
            var mapped = mapper.Map<HomeworkSubmission>(h);
            return repo.Create(mapped);
        }

        public bool Update(HomeworkSubmissionDTO h)
        {
            var mapped = mapper.Map<HomeworkSubmission>(h);
            return repo.Update(mapped);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}
