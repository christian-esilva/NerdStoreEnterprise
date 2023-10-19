using NSE.WebApp.MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Refit;

namespace NSE.WebApp.MVC.Services.Contracts
{
    public interface ICatalogServiceRefit
    {
        [Get("/catalog/products")]
        Task<IEnumerable<ProductViewModel>> GetAllProductsAsync();

        [Get("/catalog/products/{id}")]
        Task<ProductViewModel> GetProductAsync(Guid id);
    }
}
