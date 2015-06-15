﻿using System.Collections.Generic;
using NHibernate;
using Events.Business.Interfaces;
using Events.Business.Models;
using Events.Business.Classes;
using System;
using NHibernate.Criterion;
using Events.Business.Helpers;


namespace Events.NHibernateDataProvider.NHibernateCore
{
    public class NHibernateEventDataProvider : IEventDataProvider
    {
        public IList<Event> GetList(int daysToEvent, string location)
        {
            if (daysToEvent == null) return null;
            using (ISession session = Helper.OpenSession())
            {
                var endOfDay = DateTime.Today;
                if (!String.IsNullOrEmpty(location))
                {
                    session.EnableFilter("equalLocation").SetParameter("chosenLocation", location);
                }
                if (daysToEvent > 0)
                {
                    session.EnableFilter("equalDate").SetParameter("nowaday", endOfDay).SetParameter("chosenDate", endOfDay.AddDays(daysToEvent - 1));
                }
                else
                {
                    session.EnableFilter("effectiveDate").SetParameter("asOfDate", endOfDay);
                }
                var criteria = session.CreateCriteria<Event>();
                criteria.Add(Restrictions.Eq("Active", true));
                criteria.AddOrder(Order.Asc("FromDate"));
                return criteria.List<Event>();
            }
        }

        public IList<Event> GetAllEvents()
        {
            using (ISession session = Helper.OpenSession())
            {
                return session.CreateCriteria<Event>()
                        .AddOrder(Order.Asc("Checked"))
                        .AddOrder(Order.Desc("DateOfCreation")).List<Event>();
            }
        }

        public IList<Event> GetAuthorPastEvents(string email)
        {
            using (ISession session = Helper.OpenSession())
            {
                var criteria = session.CreateCriteria(typeof(Event));
                criteria.Add(Restrictions.And(Restrictions.Eq("AuthorId", email),
                    Restrictions.Lt("ToDate", DateTime.Now)));
                return criteria.List<Event>();
            }
        }

        public IList<Event> GetAuthorFutureEvents(string email)
        {
            using (ISession session = Helper.OpenSession())
            {
                var criteria = session.CreateCriteria(typeof(Event));
                criteria.Add(Restrictions.And(Restrictions.Eq("AuthorId", email),
                    Restrictions.Ge("ToDate", DateTime.Now)));
                return criteria.List<Event>();
            }
        }

        public Event GetById(string id)
        {
            using (ISession session = Helper.OpenSession())
            {
                return session.Get<Event>(id);
            }
        }

        public EventStatuses ToggleStatus(string id, bool status)
        {
            EventStatuses result = EventStatuses.NotExist;
            Event evnt = GetById(id);
            if (evnt != null)
            {
                result = EventStatuses.WasToggled;
                if (evnt.Active != status)
                {
                    evnt.Active = status;
                    this.Update(evnt);
                    result = EventStatuses.ToggleOK;
                }
            }
            return result;
        }

        public void MarkAsSeen(string id)
        {
            Event evnt = GetById(id);
            if (evnt != null)
            {
                evnt.Checked = true;
                this.Update(evnt);
            }
        }

        public void Create(Event model)
        {
            using (ISession session = Helper.OpenSession())
            {
                    session.Save(model);
                    session.Flush();
            }
        }

        public void Update(Event model)
        {
            using (ISession session = Helper.OpenSession())
            {
                    session.Update(model);
                    session.Flush();
            }
        }

        public void Delete(Event model)
        {
            using (ISession session = Helper.OpenSession())
            {
                    session.Delete(model);
                    session.Flush();
            }
        }
    }
}
