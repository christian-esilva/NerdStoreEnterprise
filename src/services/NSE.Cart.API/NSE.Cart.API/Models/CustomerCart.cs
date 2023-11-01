using FluentValidation;
using FluentValidation.Results;

namespace NSE.Cart.API.Models
{
    public class CustomerCart
    {
        internal const int MAX_QUANTITY_ITEM = 5;

        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalValue { get; set; }
        public List<ItemCart> Items { get; set; } = new List<ItemCart>();
        public ValidationResult ValidationResult { get; set; }

        public CustomerCart(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
        }

        public CustomerCart() { }

        internal void CalculateCartValue() => TotalValue = Items.Sum(p => p.CalculateValue());

        internal bool ExistsItemCart(ItemCart item) => Items.Any(p => p.ProductId == item.ProductId);

        internal ItemCart GetByProductId(Guid productId) => Items.FirstOrDefault(p => p.ProductId == productId);

        internal void AddItem(ItemCart item)
        {
            item.AssociateCart(Id);
            if (ExistsItemCart(item))
            {
                var itemExists = GetByProductId(item.ProductId);
                itemExists.AddUnit(item.Quantity);

                item = itemExists;
                Items.Remove(item);
            }

            Items.Add(item);

            CalculateCartValue();
        }

        internal void UpdateItem(ItemCart item)
        {
            item.AssociateCart(Id);

            var itemExists = GetByProductId(item.ProductId);

            Items.Remove(itemExists);
            Items.Add(item);

            CalculateCartValue();
        }

        internal void DeleteItem(ItemCart item)
        {
            Items.Remove(GetByProductId(item.ProductId));
            CalculateCartValue();
        }

        internal void UpdateUnits(ItemCart item, int units)
        {
            item.UpdateUnit(units);
            UpdateItem(item);
        }

        internal bool IsValid()
        {
            var errors = Items.SelectMany(i => new ItemCart.ItemCartValidation().Validate(i).Errors).ToList();
            errors.AddRange(new CustomerCartValidation().Validate(this).Errors);
            ValidationResult = new ValidationResult(errors);

            return ValidationResult.IsValid;
        }
    }

    public class CustomerCartValidation : AbstractValidator<CustomerCart>
    {
        public CustomerCartValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Cliente desconhecido");

            RuleFor(c => c.Items.Count)
                .GreaterThan(0)
                .WithMessage("O carrinho não possui itens");

            RuleFor(c => c.TotalValue)
                .GreaterThan(0)
                .WithMessage("O valor total do carrinho precisa ser maior que 0");
        }
    }
}
