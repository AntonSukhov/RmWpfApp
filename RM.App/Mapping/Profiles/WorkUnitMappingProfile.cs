using AutoMapper;
using RM.Api.DTOs.Responses;
using RM.App.Models;

namespace RM.App.Mapping.Profiles
{
    /// <summary>
    /// Профиль AutoMapper для настроек преобразований между:
    /// <list type="bullet">
    ///   <item><see cref="WorkUnitResponse"/> (из <see cref="Api.DTOs.Responses"/>)</item>
    ///   <item><see cref="WorkUnit"/> (из <see cref="Models"/>)</item>
    /// </list>
    /// </summary>
    public class WorkUnitMappingProfile : Profile
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="WorkUnitMappingProfile"/>.
        /// </summary>
        public WorkUnitMappingProfile()
        {
            CreateMap<WorkUnitResponse, WorkUnit>();
        }
    }
}
