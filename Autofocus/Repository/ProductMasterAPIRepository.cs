
using Autofocus.Models.Dtos;
using Autofocus.Models;
using Autofocus.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Autofocus.Repository
{
    public class ProductMasterAPIRepository : RepositoryAPI<ProductMasterUpsertDtos>, IProductMasterAPIRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductMasterAPIRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
