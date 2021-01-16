using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ActivitySourceExample
{
    public class ExampleConsumer : IConsumer<ExampleMessage>
    {
        private ILogger<ExampleConsumer> _logger;

        public ExampleConsumer(ILogger<ExampleConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ExampleMessage> context)
        {
            _logger.LogInformation("Message Id: {Id}", context.Message.Id);

            await Task.CompletedTask;
        }
    }
}
