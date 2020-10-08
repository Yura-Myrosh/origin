using SportsStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository reposytory;
        public int PageSize = 4;
        public HomeController(IStoreRepository repo)
        {
            reposytory = repo;
        }
        public ViewResult Index(string category, int productPage = 1)
            => View(new ProductsListViewModel
            {
                Products = reposytory.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                    reposytory.Products.Count() :
                    reposytory.Products.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            });
    }
}
