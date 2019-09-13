using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.Domain;
using Newtonsoft.Json;

namespace CintaUang.Controllers
{
    public class BaseController : Controller
    {
        List<ViewNotification> viewNotifications = new List<ViewNotification>();

        public void AddNotification(ViewNotification viewNotification)
        {
            viewNotifications.Add(viewNotification);
            TempData["viewNotifications"] = JsonConvert.SerializeObject(viewNotifications);
        }
    }
}