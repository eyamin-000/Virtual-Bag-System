using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class NotificationService
    {
        NotificationRepo repo;
        Mapper mapper;

        public NotificationService(NotificationRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public List<NotificationDTO> Get()
        {
            var data = repo.Get();
            return mapper.Map<List<NotificationDTO>>(data);
        }

        public NotificationDTO Get(int id)
        {
            var data = repo.Get(id);
            return mapper.Map<NotificationDTO>(data);
        }

        public bool Create(NotificationDTO notification)
        {
            var mapped = mapper.Map<Notification>(notification);
            return repo.Create(mapped);
        }

        public bool Update(NotificationDTO notification)
        {
            var mapped = mapper.Map<Notification>(notification);
            return repo.Update(mapped);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}
