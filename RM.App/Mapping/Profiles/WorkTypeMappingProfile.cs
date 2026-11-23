using AutoMapper;
using RM.Api.DTOs.Responses;
using RM.App.Models;

namespace RM.App.Mapping.Profiles
{
    /// <summary>
    /// Профиль AutoMapper для настроек преобразований между:
    /// <list type="bullet">
    ///   <item><see cref="WorkTypeResponse"/> (из Api)</item>
    ///   <item><see cref="WorkType"/> (из <see cref="Models"/>)</item>
    /// </list>
    /// </summary>
    public class WorkTypeMappingProfile : Profile
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="WorkTypeMappingProfile"/>.
        /// </summary>
        public WorkTypeMappingProfile()
        {
            CreateMap<WorkTypeResponse, WorkType>();
        }
    }
}
