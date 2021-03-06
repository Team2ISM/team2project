﻿using Events.Business.Models;
using team2project.Models;
using Events.Business;

namespace team2project
{
    public class AutomapperConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.CreateMap<Event, EventViewModel>();
            AutoMapper.Mapper.CreateMap<EventViewModel, Event>();
            AutoMapper.Mapper.CreateMap<UserViewModel, User>();
            AutoMapper.Mapper.CreateMap<User, UserViewModel>();
            AutoMapper.Mapper.CreateMap<CommentViewModel, Comment>();
            AutoMapper.Mapper.CreateMap<Comment, CommentViewModel>();
            AutoMapper.Mapper.CreateMap<User, Subscriber>();
            AutoMapper.Mapper.CreateMap<Subscriber, User>();
            AutoMapper.Mapper.CreateMap<CitiesViewModel, City>();
            AutoMapper.Mapper.CreateMap<City, CitiesViewModel>();
        }
    }
}