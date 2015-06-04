﻿using Events.Business.Classes;
using Events.Business.Models;
using Events.NHibernateDataProvider.NHibernateCore;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using team2project.Models;
using System;

namespace team2project.Controllers
{
    public class EventController : Controller
    {
        EventManager eventManager;
        CommentManager commentManager;
        CitiesManager cityManager;

        public EventController(EventManager eventManager, CommentManager commentManager, CitiesManager cityManager)
        {
            this.eventManager = eventManager;
            this.commentManager = commentManager;
            this.cityManager = cityManager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<EventViewModel> list = AutoMapper.Mapper.Map<List<EventViewModel>>(eventManager.GetList());
            foreach (var ev in list)
            {
                ev.Location = cityManager.GetById(Convert.ToInt32(ev.LocationId)).Name;
            }
            return View("List", list);
        }

        [HttpGet]
        public ActionResult Filters(string loc, string days)
        {
            if (days == "-1")
            {
                return View("List", AutoMapper.Mapper.Map<List<EventViewModel>>(eventManager.GetList(loc, null)));
            }
            int? locId = null;
            if(!String.IsNullOrEmpty(loc))
                locId = cityManager.GetByName(loc).Id;
            List<EventViewModel> list = AutoMapper.Mapper.Map<List<EventViewModel>>(eventManager.GetList(locId.ToString(), days));
            foreach (var ev in list)
            {
                ev.Location = cityManager.GetById(Convert.ToInt32(ev.LocationId)).Name;
            }
            return View("List", list);
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var evntModel = eventManager.GetById(id);

            if (evntModel == null)
            {
                return View("~/Views/Error/Page404.cshtml");
            }
            if (evntModel.Active == false)
            {
                return View("EventNotFound");
            }
            var evntViewModel = AutoMapper.Mapper.Map<EventViewModel>(evntModel);
<<<<<<< HEAD
            evntViewModel.Location = cityManager.GetById(Convert.ToInt32(evntViewModel.LocationId)).Name;
            ViewData["Comments"] = commentManager.GetByEventId(id);
=======
            evntViewModel.Comments = commentManager.GetByEventId(id);
>>>>>>> origin/master
            return View(evntViewModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            var evnt = new EventViewModel();
            return View(evnt);
        }
        [HttpGet]
        [Authorize]
        public ActionResult Update(string id)
        {
            var evntModel = eventManager.GetById(id);
           
            if (evntModel.AuthorId != User.Identity.Name)
                return RedirectToAction("Index");

            if (evntModel == null)
            {
                return View("EventNotFound");
            }
            var evnt = AutoMapper.Mapper.Map<EventViewModel>(evntModel);
<<<<<<< HEAD
            evnt.Location = cityManager.GetById(Convert.ToInt32(evnt.LocationId)).Name;
            ViewBag.Title = "Редактируйте это событие";
            ViewBag.Button = "Сохранить";
=======
>>>>>>> origin/master
            return View("Create", evnt);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Update(EventViewModel evnt)
        {
       
            if (evnt.AuthorId != User.Identity.Name)
                return RedirectToAction("Index");

            if (!ModelState.IsValid)
            {
                return View("Create", evnt);
            }
            evnt.AuthorId = User.Identity.Name;
            var evntModel = AutoMapper.Mapper.Map<Event>(evnt);
            eventManager.Update(evntModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteEvent(string id)
        {
<<<<<<< HEAD
            //  commentManager.DeleteByEventId(id);
            var evntModel = eventManager.GetById(id);

            if (evntModel.AuthorId != User.Identity.Name)
                return RedirectToAction("Index");

=======
>>>>>>> origin/SolariBranch
            eventManager.Delete(id);
            return RedirectToRoute("FutureEvents");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(EventViewModel evnt)
        {
            if (!ModelState.IsValid)
            {
                return View(evnt);
            }
            var evntModel = AutoMapper.Mapper.Map<Event>(evnt);
            evntModel.AuthorId = User.Identity.Name;

            // Replace <pre> tags with nothing, 'cause they break markup
            evntModel.Description = evntModel.Description.Replace("<pre>", "");
            evntModel.Description = evntModel.Description.Replace("</pre>", "");
            //

            eventManager.Create(evntModel.Id, evntModel);
            return RedirectToAction("Index");
        }

    }
}
