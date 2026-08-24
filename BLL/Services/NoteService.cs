using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class NoteService
    {
        NoteRepo repo;
        Mapper mapper;

        public NoteService(NoteRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public List<NoteDTO> Get()
        {
            var data = repo.Get();
            return mapper.Map<List<NoteDTO>>(data);
        }

        public NoteDTO Get(int id)
        {
            var data = repo.Get(id);
            return mapper.Map<NoteDTO>(data);
        }

        public bool Create(NoteDTO n)
        {
            var mapped = mapper.Map<Note>(n);
            return repo.Create(mapped);
        }

        public bool Update(NoteDTO n)
        {
            var mapped = mapper.Map<Note>(n);
            return repo.Update(mapped);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}
