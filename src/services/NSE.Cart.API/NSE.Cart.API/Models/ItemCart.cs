using FluentValidation;

namespace NSE.Cart.API.Models
{
    public class ItemCart
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public string Image { get; set; }
        public Guid CartId { get; set; }
        public CustomerCart CustomerCart { get; set; }

        public ItemCart() => Id = Guid.NewGuid();

        internal void AssociateCart(Guid cartId) => CartId = cartId;

        internal decimal CalculateValue() => Quantity * Value;

        internal void AddUnit(int unit) => Quantity += unit;

        internal void UpdateUnit(int unit) => Quantity = unit;

        internal bool IsValid()
        {
            return new ItemCartValidation().Validate(this).IsValid;
        }

        public class ItemCartValidation : AbstractValidator<ItemCart>
        {
            public ItemCartValidation()
            {
                RuleFor(c => c.ProductId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do produto inválido");

                RuleFor(c => c.Name)
                    .NotEmpty()
                    .WithMessage("O nome do produto é obrigatório");

                RuleFor(c => c.Quantity)
                    .GreaterThan(0)
                    .WithMessage(item => $"A quantidade miníma do produto {item.Name} é 1");

                RuleFor(c => c.Quantity)
                    .LessThanOrEqualTo(CustomerCart.MAX_QUANTITY_ITEM)
                    .WithMessage(item => $"A quantidade máxima do produto {item.Name} é {CustomerCart.MAX_QUANTITY_ITEM}");

                RuleFor(c => c.Value)
                    .GreaterThan(0)
                    .WithMessage(item => $"o valor do produto {item.Name} precisa ser maior que 0");
            }
        }
    }
}
