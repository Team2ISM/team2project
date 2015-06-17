﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Business;
using Events.Business.Models;
using Events.Business.Interfaces;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transaction;

namespace Events.NHibernateDataProvider.NHibernateCore
{
    public class NHibernateSubscribersDataProvider : ISubscribersDataProvider
    {
        public int GetCount(string eventId)
        {
            using (ISession session = Helper.OpenSession())
            {
                var criteria = session.CreateCriteria(typeof(Subscribing))
                    .Add(Restrictions.Eq("EventId", eventId))
                    .SetProjection(Projections.CountDistinct("Id"));
                return (int)criteria.UniqueResult();
            }
        }

        public IList<Subscribing> GetAllSubscribers(string eventId)
        {
            using (ISession session = Helper.OpenSession())
            {
                var criteria = session.CreateCriteria(typeof(Subscribing));
                criteria.Add(Restrictions.Eq("EventId", eventId));
                return criteria.List<Subscribing>();
            }
        }

        public void SubscribeUser(Subscribing row)
        {
            if (!IsSubscribed(row.EventId, row.UserId))
            {
                using (ISession session = Helper.OpenSession())
                {
                    session.Save(row);
                }
            }
        }

        public void UnsubscribeUser(Subscribing row)
        {
            if (IsSubscribed(row.EventId, row.UserId))
            {
                using (ISession session = Helper.OpenSession())
                {
                    var criteria = session.CreateCriteria<Subscribing>();
                    criteria.Add(Restrictions.Eq("EventId", row.EventId));
                    criteria.Add(Restrictions.Eq("UserId", row.UserId));
                    var rowForDeleting = criteria.List<Subscribing>()[0];
                    session.Delete(rowForDeleting);
                    session.Flush();
                }
            }
        }

        public bool IsSubscribed(string eventId, string userId)
        {
            using (ISession session = Helper.OpenSession())
            {
                var criteria = session.CreateCriteria(typeof(Subscribing));
                criteria.Add(Restrictions.Eq("EventId", eventId));
                criteria.Add(Restrictions.Eq("UserId", userId));
                var subscription = criteria.UniqueResult<Subscribing>();
                return (subscription != null);
            }
        }
    }
}
