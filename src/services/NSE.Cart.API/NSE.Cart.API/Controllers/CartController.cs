using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSE.Cart.API.Data;
using NSE.Cart.API.Models;
using NSE.WebApi.Core.Controllers;
using NSE.WebApi.Core.User;

namespace NSE.Cart.API.Controllers
{
    [Authorize]
    public class CartController : MainController
    {
        private readonly IAspNetUser _user;
        private readonly CartContext _context;

        public CartController(IAspNetUser user, CartContext context)
        {
            _user = user;
            _context = context;
        }

        [HttpGet("cart")]
        public async Task<CustomerCart> GetCart()
        {
            return await GetCustomerCartAsync() ?? new CustomerCart();
        }

        [HttpPost("cart")]
        public async Task<IActionResult> AddItemCart(ItemCart item)
        {
            var cart = await GetCustomerCartAsync();
            if (cart == null)
                HandleItemNewCart(item);
            else
                HandleExistsCart(cart, item);

            CartValidation(cart);
            if (!IsValid()) return CustomResponse();

            await PersistAsync();
            return CustomResponse();
        }

        [HttpPut("cart/{productId}")]
        public async Task<IActionResult> UpdateItemCart(Guid productId, ItemCart item)
        {
            var cart = await GetCustomerCartAsync();
            var itemCart = await GetValidatedItemCartAsync(productId, cart, item);

            if (itemCart == null) return CustomResponse();

            cart.UpdateUnits(itemCart, item.Quantity);

            CartValidation(cart);
            if (!IsValid()) return CustomResponse();

            _context.ItemsCart.Update(itemCart);
            _context.CustomerCart.Update(cart);

            await PersistAsync();
            return CustomResponse();
        }

        [HttpDelete("cart/{productId}")]
        public async Task<IActionResult> DeleteItemCart(Guid productId)
        {
            var cart = await GetCustomerCartAsync();
            var itemCart = await GetValidatedItemCartAsync(productId, cart);
            if (itemCart == null) return CustomResponse();

            cart.DeleteItem(itemCart);

            CartValidation(cart);
            if (!IsValid()) return CustomResponse();

            _context.ItemsCart.Remove(itemCart);
            _context.CustomerCart.Update(cart);

            await PersistAsync();
            return CustomResponse();
        }

        private async Task<CustomerCart> GetCustomerCartAsync() =>
            await _context.CustomerCart
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.CustomerId == _user.GetUserId());

        private void HandleItemNewCart(ItemCart item)
        {
            var cart = new CustomerCart(_user.GetUserId());
            cart.AddItem(item);
            _context.CustomerCart.Add(cart);
        }

        private void HandleExistsCart(CustomerCart cart, ItemCart item)
        {
            var productExists = cart.ExistsItemCart(item);
            cart.AddItem(item);

            if (productExists)
            {
                _context.ItemsCart.Update(cart.GetByProductId(item.ProductId));
            }
            else
            {
                _context.ItemsCart.Add(item);
            }

            _context.CustomerCart.Update(cart);
        }

        private async Task<ItemCart> GetValidatedItemCartAsync(Guid productId, CustomerCart cart, ItemCart item = null)
        {
            if (item is not null && productId != item.ProductId)
            {
                AddProcessingErrors("O item não corresponde ao informado");
                return null;
            }

            if (cart is null)
            {
                AddProcessingErrors("Carrinho não encontrado");
                return null;
            }

            var itemCart = await _context.ItemsCart
                .FirstOrDefaultAsync(i => i.CartId == cart.Id && i.ProductId == productId);

            if (itemCart is null || !cart.ExistsItemCart(itemCart))
            {
                AddProcessingErrors("O item não está no carrinho");
                return null;
            }

            return itemCart;
        }

        private async Task PersistAsync()
        {
            var result = await _context.SaveChangesAsync();
            if (result <= 0) AddProcessingErrors("Não foi possível salvar os dados");
        }

        private bool CartValidation(CustomerCart cart)
        {
            if (cart.IsValid()) return true;

            cart.ValidationResult.Errors.ToList().ForEach(e => AddProcessingErrors(e.ErrorMessage));
            return false;
        }
    }
}
