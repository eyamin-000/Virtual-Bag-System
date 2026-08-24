using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class AttendanceService
    {
        AttendanceRepo repo;
        Mapper mapper;

        public AttendanceService(AttendanceRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public List<AttendanceDTO> Get()
        {
            var data = repo.Get();
            return mapper.Map<List<AttendanceDTO>>(data);
        }

        public AttendanceDTO Get(int id)
        {
            var data = repo.Get(id);
            return mapper.Map<AttendanceDTO>(data);
        }

        public bool Create(AttendanceDTO a)
        {
            var mapped = mapper.Map<Attendance>(a);
            return repo.Create(mapped);
        }

        public bool Update(AttendanceDTO a)
        {
            var mapped = mapper.Map<Attendance>(a);
            return repo.Update(mapped);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}
