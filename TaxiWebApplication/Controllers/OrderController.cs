using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaxiWebApplication.Models;
using TaxiWebApplication.Services;
using TaxiWebApplication.ViewModels;

namespace TaxiWebApplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationContext _adb;
        private IUserManagerService _userService;
        private readonly KnnService _knn;
        private readonly GraphRoadService _grs;
        private readonly UserManager<User> _userManager;
        //  private readonly UserManagerService _userService;

        public OrderController(ApplicationContext adb, IUserManagerService userService, UserManager<User> userManager,
            KnnService knn, GraphRoadService grs)
        {
            _adb = adb;
            _userManager = userManager;
            _userService = userService;
            _knn = knn;
            _grs = grs;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new OrderViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(OrderViewModel orderViewModel)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);


            //var order = new Order
            //{
            //    Address = Address,

            //    UserId = user.Id
            //};

            int t = _knn.Parse(orderViewModel.Address);
            string[] mas = orderViewModel.Address.Split(' ');
            mas[0] = mas[0].Replace('.', ',');
            mas[1] = mas[1].Replace('.', ',');
            double[] unknown = new double[2];
            for (int i = 0; i < mas.Length; i++)
            {
                unknown[i] = double.Parse(mas[i]);
            }


            //if (_adb.Orders.Any(ord => ord.UserId == order.UserId))
            //{
            //    ModelState.AddModelError("Address", "Too much orders by for current user");
            //    return View(ordModel);
            //}

            //_adb.Orders.Add(order);
            //await _adb.SaveChangesAsync();


            //double[] destination = new double[] { 50.644, 29.91 };

            //double dist = _knn.DistanceToDest(unknown, destination);

            ViewData["Distance"] = unknown[0];
            ViewData["Price"] = unknown[1];

            return RedirectToAction("DistanceAndPrice", "Order", new {distance = unknown[0], price = unknown[1]});

           // return Json(new { redirectToUrl = Url.Action("DistanceAndPrice", "Order") });


        }

        public IActionResult DistanceAndPrice(decimal distance, decimal price)
        {
            ViewData["Distance"] = distance;
            ViewData["Price"] = price;
            return View();
        }

        [HttpPost]
       
        public async Task<JsonResult> VVDistance([FromBody]TwoMarkersViewModel viewModel)
        {
            TwoMarkersViewModel coords = new TwoMarkersViewModel
            {
                FirstLat = viewModel.FirstLat,
                FirstLong = viewModel.FirstLong,
                SecondLat = viewModel.SecondLat,
                SecondLong = viewModel.SecondLong
            };

            double dist = await _grs.GraphDistance(coords.FirstLat, coords.FirstLong, coords.SecondLat, coords.SecondLong);
            DistPriceViewModel mod = new DistPriceViewModel { Distance = dist, Price = dist * 6 };
            

            return new JsonResult(mod);
        }

    }
}
