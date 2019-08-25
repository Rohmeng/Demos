using System;
using AutoMapper;
using SwaggerAutomapperDemo.Models;

namespace SwaggerAutomapperDemo
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<User, UserDTO>().ReverseMap().ForMember(d => d.Phone, o => o.Ignore()); //可自定义忽略某项         

            CreateMap<Order, OrderDto>();
            
            //由简到繁自定义映射配置
            CreateMap<CalendarEvent, CalendarEventForm>()
                .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.EventDate.Date))//定义映射规则
                .ForMember(dest => dest.EventHour, opt => opt.MapFrom(src => src.EventDate.Hour))//定义映射规则
                .ForMember(dest => dest.EventMinute, opt => opt.MapFrom(src => src.EventDate.Minute));//定义映射规则

            CreateMap<Source, Destination>();

            //包含派生类
            CreateMap<ParentSource, ParentDestination>().Include<ChildSource, ChildDestination>();
            CreateMap<ChildSource, ChildDestination>();

            //嵌套映射
            CreateMap<OuterSource, OuterDest>();
            CreateMap<InnerSource, InnerDest>();
        }
    }
}
