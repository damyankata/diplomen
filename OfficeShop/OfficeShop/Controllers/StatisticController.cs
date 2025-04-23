using Microsoft.AspNetCore.Mvc;
using OfficeShop.Core.Contracts;
using OfficeShop.Models.Statistic;

namespace OfficeShop.Controllers
{
    public class StatisticController : Controller
    {
        private readonly IStatisticService statisticsService;

        public StatisticController(IStatisticService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        public IActionResult Index()
        {
            StatisticVM statistics = new StatisticVM
            {
                CountClients = statisticsService.CountClients(),
                CountProducts = statisticsService.CountProducts(),
                CountOrders = statisticsService.CountOrders(),
                SumOrders = statisticsService.SumOrders()
            };

            return View(statistics);
        }
    }
}
