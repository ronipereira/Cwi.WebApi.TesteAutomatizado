using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;
using MediatR;

namespace Cwi.Treinamento.TesteAutomatizado.Infra.Mediator
{
    /// <summary>
    /// Define a classe IMediatorHandler.
    /// </summary>
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator mediator;

        public MediatorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<CommandResponse<TResult>> SendCommand<TResult>(Command<TResult> command)
        {
            return mediator.Send(command);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual Task PublishEvent<TEvent>(TEvent @event)
          where TEvent : Event
        {
            return mediator.Publish(@event);
        }
    }
}