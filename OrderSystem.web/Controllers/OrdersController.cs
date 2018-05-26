using OrderSystem.Application.Interfaces;
using OrderSystem.Application.Services;
using OrderSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderSystem.web.Controllers
{
    public class OrdersController : Controller
    {
        #region Fields
        private ISuppliersService _SuppliersService = null;
        private IOrdersService _OrdersService = null;
        #endregion
        public OrdersController(ISuppliersService suppliersService, IOrdersService ordersService)
        {
            _SuppliersService = suppliersService;
            _OrdersService = ordersService;
        }
        public ActionResult Index()
        {
            var orders = _OrdersService.GetOrders();
            return View(orders);
        }
        [HttpGet]
        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        public ActionResult New(Order order)
        {
            _OrdersService.CreateOrder(order);
            return RedirectToAction("Index");
        }
    }
}