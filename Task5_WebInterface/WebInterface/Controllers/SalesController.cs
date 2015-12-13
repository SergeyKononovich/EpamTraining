using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Common.Logging;
using UIPart.IBL;
using UIPart.Models;
using WebInterface.Models;

namespace WebInterface.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        public SalesController()
        {
        }
        public SalesController(IBO bo, ILog log)
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

        public JsonResult SalesData(JQueryDataTableParamViewModel table, SaleFilterViewModel filter)
        {
            if (ModelState.IsValid)
            {
                var page = new SalesPage();
                try
                {
                    var filterModel = GetSalesFilter(filter);
                    var sortOpt = GetSalesSortingOptions();
                    page = BO.SaleService.GetPage(filterModel, sortOpt, 
                        table.iDisplayStart, table.iDisplayLength);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }

                return Json(new
                {
                    sEcho = table.sEcho,
                    iTotalRecords = page.TotalRecords,
                    iTotalDisplayRecords = page.TotalDisplayRecords,
                    aaData = GetJsonFromPage(page)
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                message = "Invalid params"
            });
        }

        [Authorize(Roles = "admin")]
        public ActionResult AddSale()
        {
            return PartialView("AddSale");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddSale(AddSaleViewModel sale)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BO.SaleService.Add(GetSaleModel(sale));
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    ViewBag.Message = "An error occurred while adding";
                    return PartialView("AddSaleAns");
                }

                Log.Trace("Sale adding successfully");
                ViewBag.Message = "Sale adding successfully";
                return PartialView("AddSaleAns");
            }

            return PartialView("_AddSaleInputsPartial");
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
                ViewBag.Message = "An error occurred while deleting";
                return PartialView("DeleteSalesAns");
            }

            Log.Trace("Deleting succeeded");
            ViewBag.Message = "Deleting succeeded";
            return PartialView("DeleteSalesAns");
        }


        private SaleModel GetSaleModel(AddSaleViewModel view)
        {
            return new SaleModel()
            {
                Id = 0,
                Date = view.Date,
                Goods =
                            BO.GoodsService.FindByName(view.Goods) ??
                            new GoodsModel { Name = view.Goods },
                Manager =
                            BO.ManagerService.FindBySecondName(view.Manager) ??
                            new ManagerModel { SecondName = view.Manager },
                Client =
                            BO.ClientService.FindByFullName(view.Client) ??
                            new ClientModel { FullName = view.Client },
                SellingPrice = view.SellingPrice
            };
        }
        private SalesSortingOptions GetSalesSortingOptions()
        {
            var options = new SalesSortingOptions();

            var countSortedColums = Convert.ToInt32(Request["iSortingCols"]);
            if (countSortedColums < 1) return options;

            for (var i = 0; i < countSortedColums; ++i)
            {
                var o = new SaleSortOption();

                var sortColumnIndex = Convert.ToInt32(Request["iSortCol_" + i]);
                if (sortColumnIndex < 0 || sortColumnIndex > 5) continue;
                o.Column = GetOrderColumnByIndex(sortColumnIndex);

                var sortDir = Request["sSortDir_" + i]; // asc or desc
                if (sortDir != "asc")
                    o.Ascending = false;

                options.Options.Add(o);
            }

            return options;
        }
        
        private static SalesFilter GetSalesFilter(SaleFilterViewModel view)
        {
            return Mapper.Map<SalesFilter>(view);
        }
        private static SaleOrderColumn GetOrderColumnByIndex(int col)
        {
            switch (col)
            {
                case 0: return SaleOrderColumn.Id;
                case 1: return SaleOrderColumn.Date;
                case 2: return SaleOrderColumn.Goods;
                case 3: return SaleOrderColumn.Manager;
                case 4: return SaleOrderColumn.Client;
                case 5: return SaleOrderColumn.SellingPrice;
                default: return SaleOrderColumn.Id;
            }
        }
        private static IEnumerable<string[]> GetJsonFromPage(SalesPage page)
        {
            return from c in page.Displayed
                   select new[]
                   {
                        c.Id.ToString(),
                        c.Date.ToShortDateString(),
                        c.Goods.Name,
                        c.Manager.SecondName,
                        c.Client.FullName,
                        c.SellingPrice.ToString()
                    };
        }
    }
}