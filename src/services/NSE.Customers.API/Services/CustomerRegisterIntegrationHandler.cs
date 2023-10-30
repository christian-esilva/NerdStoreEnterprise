using FluentValidation.Results;
using NSE.Core.Mediator;
using NSE.Core.Messages.Integration;
using NSE.Customers.API.Application.Commands;
using NSE.MessageBus.Contracts;

namespace NSE.Customers.API.Services
{
    public class CustomerRegisterIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public CustomerRegisterIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetRespond();

            return Task.CompletedTask;
        }

        private async Task<ResponseMessage> RegisterCustomer(UserRegisteredIntegrationEvent message)
        {
            var command = new RegisterCustomerCommand(message.Id, message.Name, message.Email, message.Cpf);
            ValidationResult success;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                success = await mediator.SendCommand(command);
            }

            return new ResponseMessage(success);
        }

        private void SetRespond()
        {
            _bus.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(RegisterCustomer);
            _bus.AdvancedBus.Connected += OnConnect;
        }

        private void OnConnect(object sender, EventArgs e)
        {
            SetRespond();
        }
    }
}
