using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class UserService
    {
        UserRepo repo;
        Mapper mapper;

        public UserService(UserRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public List<UserDTO> Get()
        {
            var data = repo.Get();
            return mapper.Map<List<UserDTO>>(data);
        }

        public UserDTO Get(int id)
        {
            var data = repo.Get(id);
            return mapper.Map<UserDTO>(data);
        }

        public bool Create(UserDTO u)
        {
            var mapped = mapper.Map<User>(u);
            return repo.Create(mapped);
        }

        public bool Update(UserDTO u)
        {
            var mapped = mapper.Map<User>(u);
            return repo.Update(mapped);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}
