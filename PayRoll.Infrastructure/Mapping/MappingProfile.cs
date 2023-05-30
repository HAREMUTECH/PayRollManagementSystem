
using AutoMapper;
using PayRoll.Application.Common.Dto.CadreDto;
using PayRoll.Application.Common.Dto.EmployeeDto;
using PayRoll.Application.Common.Dto.LevelDto;
using PayRoll.Application.Common.Dto.PayRollDto;
using PayRoll.Application.Common.Dto.PositionDto;
using PayRoll.Application.Common.Dto.SalaryOptionDto;
using PayRoll.Domain.Entities;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace PayRoll.Infrastructure.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Config();
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                    ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }


        private void Config()
        {

            CreateMap<Cadre, CadreDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Level, LevelDto>().ReverseMap();
            CreateMap<PayRollManagement, PayRollManagementDto>().ReverseMap();
            CreateMap<Position, PositionDto>().ReverseMap();
            CreateMap<SalaryOption, SalaryOptionDto>().ReverseMap();
           
        }
    }
}
