﻿using Events.Business.Classes;
using System.Collections.Generic;
using System.Web.Mvc;
using team2project.Models;
using System.Web.Script.Serialization;
using team2project.Helpers;

namespace team2project.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        EventManager manager;
        CommentManager commentManager;

        public AdminController(EventManager manager, CommentManager commentManager)
        {
            this.manager = manager;
            this.commentManager = commentManager;
        }

        [HttpGet]
        public ActionResult ManagerPage()
        {
            List<EventViewModel> list = AutoMapper.Mapper.Map<List<EventViewModel>>(manager.GetAllEvents(true));
            return View("ManagerPage", list);
        }

        [HttpGet]
        public ActionResult GetEvents()
        {
            List<EventViewModel> list = AutoMapper.Mapper.Map<List<EventViewModel>>(manager.GetAllEvents(true));
            foreach(var elem in manager.GetAllEvents(true))
            {
                manager.MarkAsSeen(elem.Id);
            }
            return Json(
                new JsonResultHelper()
                {
                    Data = list,
                    Message = "Success: Get list of all events",
                    Status = JsonResultHelper.StatusEnum.Success
                },
                JsonRequestBehavior .AllowGet
                );
        }

        [HttpPost]
        public ActionResult ToggleButtonStatusActive(string id)
        {
            manager.ToggleButtonStatusActive(id);
            return Json(
                new JsonResultHelper()
                {
                    Data = null,
                    Message = "Success: Toogle Active Event",
                    Status = JsonResultHelper.StatusEnum.Success
                }
                );
        }  

        [HttpPost]
        public ActionResult DeleteEvent(string id)
        {
            commentManager.DeleteByEventId(id);
            manager.Delete(id);
            return Json(
                new JsonResultHelper()
                {
                    Data = null,
                    Message = "Success: Delete Event",
                    Status = JsonResultHelper.StatusEnum.Success
                }
                );
        }

        //[HttpPost]
        //public ActionResult MarkEvent(string id)
        //{
        //    manager.MarkAsSeen(id);
        //    return Json(
        //        new JsonResultHelper()
        //        {
        //            Data = null,
        //            Message = "Success: Marking Event",
        //            Status = JsonResultHelper.StatusEnum.Success
        //        }
        //        );
        //}
    }
}
