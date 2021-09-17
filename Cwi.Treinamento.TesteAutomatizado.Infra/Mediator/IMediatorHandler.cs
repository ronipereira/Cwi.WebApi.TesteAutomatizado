using System.Threading.Tasks;
using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;

namespace Cwi.Treinamento.TesteAutomatizado.Infra.Mediator
{
    /// <summary>
    /// Define a interface IMediatorHandler.
    /// </summary>
    public interface IMediatorHandler
    {
        Task<CommandResponse<TResult>> SendCommand<TResult>(Command<TResult> command);

        Task PublishEvent<TEvent>(TEvent @event)
          where TEvent : Event;
    }
}