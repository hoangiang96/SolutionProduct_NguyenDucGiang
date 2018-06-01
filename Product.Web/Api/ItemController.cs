using Product.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Product.Service;
using AutoMapper;
using Product.Model.Models;
using Product.Web.Models;

namespace Product.Web.Api
{
    [RoutePrefix("Api/Item")]
    public class ItemController : ApiControllerBase
    {

        private IItemService _itemService;

        public ItemController(IErrorService errorService,IItemService itemService) : base(errorService)
        {
            this._itemService = itemService;
        }

        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _itemService.GetAll();

                var responseData = Mapper.Map<IEnumerable<Item>, IEnumerable<itemViewModel>>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

    }
}
