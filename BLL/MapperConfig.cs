using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class MapperConfig
    {
        public static MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Class, ClassDTO>().ReverseMap();
                cfg.CreateMap<Subject, SubjectDTO>().ReverseMap();
                cfg.CreateMap<User, UserDTO>().ReverseMap();
                cfg.CreateMap<TeacherAssignment, TeacherAssignmentDTO>().ReverseMap();
                cfg.CreateMap<Book, BookDTO>().ReverseMap();
                cfg.CreateMap<Note, NoteDTO>().ReverseMap();
                cfg.CreateMap<Homework, HomeworkDTO>().ReverseMap();
                cfg.CreateMap<HomeworkSubmission, HomeworkSubmissionDTO>().ReverseMap();
                cfg.CreateMap<AttendanceSession, AttendanceSessionDTO>().ReverseMap();
                cfg.CreateMap<Attendance, AttendanceDTO>().ReverseMap();
                cfg.CreateMap<StudyActivity, StudyActivityDTO>().ReverseMap();
                cfg.CreateMap<Notification, NotificationDTO>().ReverseMap();
            });

        public static Mapper GetMapper()
        {
            return new Mapper(config);
        }
    }
}
