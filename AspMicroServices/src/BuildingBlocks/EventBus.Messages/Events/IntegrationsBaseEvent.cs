using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class IntegrationsBaseEvent
    {
        public IntegrationsBaseEvent()
        {
            this.Id = Guid.NewGuid();
            this.CreationDate = DateTime.Now;
        }

        public IntegrationsBaseEvent(Guid guid, DateTime dateTime)
        {
            this.Id = guid;
            this.CreationDate = dateTime;
        }
        public Guid Id { get; private set; }

        public DateTime CreationDate { get; private set; }
    }
}
