using ChartUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChartUI.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IDataRepository _dataRepository;
        private readonly AppDbContext _appDbContext;

        public HomeController(IDataRepository dataRepository, AppDbContext appDbContext)
        {
            _dataRepository = dataRepository;
            _appDbContext = appDbContext;
        }

        // GET: HomeController
        [Route("~/Home")]
        [Route("~/")]
        public ActionResult Index()
        {
            _dataRepository.SavetoDatabase();
            var model = _dataRepository.GetDates();
            return View(model);
        }

        public ActionResult MapChart()
        {
            DateTime dateValue;
            DateTime.TryParse("2021-04-17 01:02:40.0000000", out dateValue);
            var FlightGearDataList = _dataRepository.GetAllDatasAsList(dateValue);
            return View(FlightGearDataList);
        }

        public ActionResult AnyChart(DateTime dateTime)
        {
            
            var FlightGearDataList = _dataRepository.GetAllDatasAsList(dateTime);

            List<DataPoint> dataPoints1 = new List<DataPoint>();
            List<DataPoint> dataPoints2 = new List<DataPoint>();
            List<DataPoint> dataPoints3 = new List<DataPoint>();
            List<DataPoint> dataPoints4 = new List<DataPoint>();
            List<DataPoint> dataPoints5 = new List<DataPoint>();

            for (int x = 0; x < FlightGearDataList.Count(); x++)
            {
                dataPoints1.Add(new DataPoint(x, FlightGearDataList[x].Roll));
                dataPoints2.Add(new DataPoint(x, FlightGearDataList[x].Pitch));
                dataPoints3.Add(new DataPoint(x, FlightGearDataList[x].Yaw));
                dataPoints4.Add(new DataPoint(x, FlightGearDataList[x].Altitude));
                dataPoints5.Add(new DataPoint(x, FlightGearDataList[x].Speed));
            }

            ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints1);
            ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints2);
            ViewBag.DataPoints3 = JsonConvert.SerializeObject(dataPoints3);
            ViewBag.DataPoints4 = JsonConvert.SerializeObject(dataPoints4);
            ViewBag.DataPoints5 = JsonConvert.SerializeObject(dataPoints5);

            return View();
        }

        // GET
        public ActionResult Delete(DateTime dateTime)
        {
            _appDbContext.FlightGearDatas.RemoveRange(_appDbContext.FlightGearDatas.Where(x => x.FlightDate == dateTime));
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
