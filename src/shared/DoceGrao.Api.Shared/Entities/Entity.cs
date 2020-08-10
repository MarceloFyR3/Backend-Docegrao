using System;
using System.Collections.Generic;
using System.Text;

namespace DoceGrao.Api.Shared.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();

        }

        public Guid Id { get; set; }
    }
}
