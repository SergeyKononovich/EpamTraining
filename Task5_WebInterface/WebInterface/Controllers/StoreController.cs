using System;
using System.Web.Mvc;
using Common.Logging;
using UIPart.IBL;

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
            var items = BO.SaleService.GetAll();
            return View(items);
        }
        
    }
}