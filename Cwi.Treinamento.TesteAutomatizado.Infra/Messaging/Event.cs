using System;
using MediatR;

namespace Cwi.Treinamento.TesteAutomatizado.Infra.Messaging
{
    public abstract class Event : Message, INotification
    {
        protected Event()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; private set; }
    }
}