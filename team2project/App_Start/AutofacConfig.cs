﻿using Autofac;
using Autofac.Integration.Mvc;
using Events.Business.Classes;
using Events.NHibernateDataProvider.NHibernateCore;
using Events.EntityFrameworkDataProvider;
using System.Web.Mvc;
using Events.RuntimeCache;
using Events.Business.Interfaces;
using Events.Business;

namespace team2project
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<EventManager>();
            builder.RegisterType<CommentManager>();
            builder.RegisterType<EntityFrameworkEventsDataProvider>()
                .As<IEventDataProvider>();
            builder.RegisterType<EntityFrameworkCommentDataProvider>()
                .As<ICommentDataProvider>();
            builder.RegisterType<RuntimeCacheManager>()
                .As<ICacheManager>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}