using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class AttendanceSessionService
    {
        AttendanceSessionRepo repo;
        Mapper mapper;

        public AttendanceSessionService(AttendanceSessionRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public List<AttendanceSessionDTO> Get()
        {
            var data = repo.Get();
            return mapper.Map<List<AttendanceSessionDTO>>(data);
        }

        public AttendanceSessionDTO Get(int id)
        {
            var data = repo.Get(id);
            return mapper.Map<AttendanceSessionDTO>(data);
        }

        public bool Create(AttendanceSessionDTO a)
        {
            var mapped = mapper.Map<AttendanceSession>(a);
            return repo.Create(mapped);
        }

        public bool Update(AttendanceSessionDTO a)
        {
            var mapped = mapper.Map<AttendanceSession>(a);
            return repo.Update(mapped);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}
