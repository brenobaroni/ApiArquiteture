using Api.Domain.Commands;
using Wolverine.Attributes;

namespace Api.Application.Events
{
    [WolverineHandler]
    public class SaleEvents
    {
        public async Task<bool> Handle(SaleCreatedEvent sale_event)
        {
            Console.WriteLine("Sending Email to Customer.....");

            return true;
        }
    }
}
