using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class TeacherAssignmentService
    {
        TeacherAssignmentRepo repo;
        Mapper mapper;

        public TeacherAssignmentService(TeacherAssignmentRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public List<TeacherAssignmentDTO> Get()
        {
            var data = repo.Get();
            return mapper.Map<List<TeacherAssignmentDTO>>(data);
        }

        public TeacherAssignmentDTO Get(int id)
        {
            var data = repo.Get(id);
            return mapper.Map<TeacherAssignmentDTO>(data);
        }

        public bool Create(TeacherAssignmentDTO t)
        {
            var mapped = mapper.Map<TeacherAssignment>(t);
            return repo.Create(mapped);
        }

        public bool Update(TeacherAssignmentDTO t)
        {
            var mapped = mapper.Map<TeacherAssignment>(t);
            return repo.Update(mapped);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}
