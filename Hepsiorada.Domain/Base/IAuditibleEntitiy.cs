using System;

namespace Hepsiorada.Domain.Base
{
    public abstract class IAuditibleEntitiy : IEntityHasId
    {
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedIp { get; set; }
    }
}
