using MediatR;

namespace NSE.Customers.API.Application.Events
{
    public class CustomerEventHandler : INotificationHandler<CustomerRegisteredEvent>
    {
        public Task Handle(CustomerRegisteredEvent notification, CancellationToken cancellationToken)
        {
            // Enviar evento de confirmação
            return Task.CompletedTask;
        }
    }
}
