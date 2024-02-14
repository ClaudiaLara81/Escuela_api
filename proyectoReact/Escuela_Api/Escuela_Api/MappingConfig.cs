using AutoMapper;
using Escuela_Api.Models;
using Escuela_Api.Models.Dto;

namespace Escuela_Api
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Student, StudentDto>();
            CreateMap<StudentDto, Student>();

            CreateMap<Student, StudentCreatedDto>().ReverseMap();
            CreateMap<Student, StudentUpdateDto>().ReverseMap();

            CreateMap<NumberStudent, NumberStudentDto>().ReverseMap();
            CreateMap<NumberStudent, NumberStudentCreateDto>().ReverseMap();
            CreateMap<NumberStudent, NumberStudentUpdateDto>().ReverseMap();
            
        }

    }
}
