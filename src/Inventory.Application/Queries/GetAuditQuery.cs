using MediatR;
using Inventory.Application.DTOs;
using System;
using System.Collections.Generic;

namespace Inventory.Application.Queries
{
    public class GetAuditQuery : IRequest<List<AuditDto>>
    {
        public Guid? UserId { get; set; }
    }
}