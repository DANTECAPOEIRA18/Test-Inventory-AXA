using MediatR;
using Inventory.Application.DTOs;
using System.Collections.Generic;

namespace Inventory.Application.Queries
{
    public class GetTypeDocumentsQuery : IRequest<List<TypeDocumentDto>>
    {
    }
}