using System;
using Volo.Abp.Application.Dtos;

namespace NIC.SBCPlatform.SharedModules.LookupManagement
{
    public class RequestDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public RequestTypeEnum RequestTypeId { get; set; }
        public RequestStatusEnum RequestStatusId { get; set; }


    }
}
