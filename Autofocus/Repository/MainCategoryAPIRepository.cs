
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
    public class MainCategoryAPIRepository : RepositoryAPI<MainCategoryDtos>, IMainCategoryAPIRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public MainCategoryAPIRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
