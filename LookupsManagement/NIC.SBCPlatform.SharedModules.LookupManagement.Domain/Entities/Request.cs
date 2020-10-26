using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace NIC.SBCPlatform.SharedModules.LookupManagement.Entities
{
    public class Request : AuditedAggregateRoot<Guid>, ISoftDelete
    {
        public RequestTypeEnum RequestTypeId { get; set; }
        public RequestStatusEnum RequestStatusId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public Request()
        {
        }

        public Request(Guid id, RequestTypeEnum requestTypeId, RequestStatusEnum requestStatusId) : base(id)
        {
            RequestTypeId = requestTypeId;
            RequestStatusId = requestStatusId;
        }

        public void Update(RequestStatusEnum requestStatusId, RequestTypeEnum requestTypeId)
        {
            RequestStatusId = requestStatusId;
            RequestTypeId = requestTypeId;
        }
    }
}
