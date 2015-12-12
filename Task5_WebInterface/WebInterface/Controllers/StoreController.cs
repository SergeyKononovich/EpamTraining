using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.Logging;
using UIPart.IBL;
using UIPart.Models;
using WebInterface.Models;

namespace WebInterface.Controllers
{
    [Authorize]
    public class StoreController : Controller
    {
        public StoreController()
        {
        }
        public StoreController(IBO bo, ILog log)
        {
            if (bo == null) throw new ArgumentNullException(nameof(bo));
            if (log == null) throw new ArgumentNullException(nameof(log));

            BO = bo;
            Log = log;
        }
        

        private IBO BO { get; }
        private ILog Log { get; }


        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SalesData(JQueryDataTableParamModel table)
        {
            var filter = GetFilter();
            var totalRecords = 0;
            var totalDisplayRecords = 0;

            ICollection<SaleModel> displayed;
            try
            {
                displayed = BO.SaleService.GetPage(filter, table.iDisplayStart, table.iDisplayLength,
                    out totalRecords, out totalDisplayRecords);
            }
            catch (Exception e)
            {
                Log.Trace(e);
                displayed = new List<SaleModel>();
            }

            var rows = from c in displayed
                       select new[] 
                           {
                               c.Id.ToString(),
                               c.Date.ToShortDateString(),
                               c.Goods.Name,
                               c.Manager.SecondName,
                               c.Client.FullName,
                               c.SellingPrice.ToString()
                           };
            return Json(new
            {
                sEcho = table.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalDisplayRecords,
                aaData = rows
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin")]
        public ActionResult AddSale()
        {
            return View("Index");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteSales(int[] ids)
        {
            if (ids == null || ids.Length == 0)
                return HttpNotFound();

            try
            {
                BO.SaleService.DeleteRange(ids);
            }
            catch (Exception e)
            {
                Log.Error(e);
                ViewBag.Message = "Deleting faild";
                return PartialView("DeleteAns");
            }

            ViewBag.Message = "Deleting succeeded";
            return PartialView("DeleteAns");
        }

        private SaleFilter GetFilter()
        {
            var idFilter = Convert.ToString(Request["sSearch_0"]);
            var dateFilter = Convert.ToString(Request["sSearch_1"]);
            var goodsFilter = Convert.ToString(Request["sSearch_2"]);
            var managerFilter = Convert.ToString(Request["sSearch_3"]);
            var clientFilter = Convert.ToString(Request["sSearch_4"]);
            var priceFilter = Convert.ToString(Request["sSearch_5"]);
            
            int fromId, toId;
            DateTime fromDate, toDate;
            int fromPrice, toPrice;
            ParseNumberRangeFilter(idFilter, out fromId, out toId);
            ParseDateRangeFilter(dateFilter, out fromDate, out toDate);
            ParseNumberRangeFilter(priceFilter, out fromPrice, out toPrice);

            return new SaleFilter
            {
                FromId = fromId,
                ToId = toId,
                ToDate = toDate,
                FromDate = fromDate,
                GoodsNamePart = goodsFilter,
                ManagerSecondNamePart = managerFilter,
                ClientFullNamePart = clientFilter,
                ToPrice = toPrice,
                FromPrice = fromPrice
            };
        }

        private static void ParseNumberRangeFilter(string filter, out int from, out int to)
        {
            from = int.MinValue;
            to = int.MaxValue;
            if (filter.Contains('~'))
            {
                var temp = filter.Split('~')[0];
                from = temp == "undefined" ? from : Convert.ToInt32(temp);
                temp = filter.Split('~')[1];
                to = temp == "undefined" ? to : Convert.ToInt32(temp);
            }
        }
        private static void ParseDateRangeFilter(string filter, out DateTime from, out DateTime to)
        {
            from = DateTime.MinValue;
            to = DateTime.MaxValue;
            if (filter.Contains('~'))
            {
                var temp = filter.Split('~')[0];
                from = temp == "undefined" ? from : Convert.ToDateTime(temp);
                temp = filter.Split('~')[1];
                to = temp == "undefined" ? to : Convert.ToDateTime(temp);
            }
        }
    }
}