﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Business.Models;
using Events.Business.Interfaces;
using Events.Business.Helpers;

namespace Events.Business.Classes
{
    public class RemindManager : BaseManager
    {
        protected IEmailReminderDataProvider dataProvider;

        protected override string Name { get; set; }

        public RemindManager(IEmailReminderDataProvider dataProvider, ICacheManager cacheManager)
            : base(cacheManager)
        {
            Name = EnvironmentInfo.ReminderCacheName;
            this.dataProvider = dataProvider;
        }

        public IList<Event> GetListEventsToRemind()
        {
            return FromCache<IList<Event>>("list",
                () =>
                {
                    return dataProvider.GetListEventsToRemind();
                });
        }

        public IList<User> GetUsersToRemind(string eventId)
        {
            return FromCache<IList<User>>("UserToRemindByEventId:" + eventId,
                () =>
                {
                    return dataProvider.GetUsersToRemind(eventId);
                });
        }

        public RemindModel GetIsRemindedModel(string eventId)
        {
            return FromCache<RemindModel>(eventId,
                () =>
                {
                    return dataProvider.GetIsRemindedModel(eventId);
                });
        }

        public void SaveOrUpdateIsRemindedModel(RemindModel model)
        {
            dataProvider.SaveOrUpdateIsRemindedModel(model);
            ClearCache();
            ToCache<RemindModel>(model.EventId,
                () =>
                {
                    return model;
                });
        }

        public void ResetRemindModel(string eventId)
        {
            dataProvider.ResetRemindModel(eventId);
            ClearCache();
        }
    }
}
