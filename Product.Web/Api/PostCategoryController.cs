using AutoMapper;
using Product.Model.Models;
using Product.Service;
using Product.Web.Infrastructure.Core;
using Product.Web.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Product.Web.Infrastructure.Extensions;

namespace Product.Web.Api
{
    [RoutePrefix("api/postcategory")]
    public class PostCategoryController : ApiControllerBase
    {
        IPostCategoryService _postCategoryService;

        public PostCategoryController(IErrorService errorService,IPostCategoryService postCategoryService) :
            base(errorService)
        {
            this._postCategoryService = postCategoryService;
        }

        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage Request)
        {
            return CreateHttpResponse(Request, () =>
            {
                var listcategory = _postCategoryService.GetAll();

                var listPostCategoryVm = Mapper.Map<List<PostCategoryViewModel>>(listcategory);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, listPostCategoryVm);

                return response;
            });
        }

        [Route("Add")]
        public HttpResponseMessage Post(HttpRequestMessage Request,PostCategoryViewModel postCategoryVm)
        {
            return CreateHttpResponse(Request, () => 
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    PostCategory newPostCategory = new PostCategory();
                    newPostCategory.UpdataPostCategory(postCategoryVm);

                    var category= _postCategoryService.Add(newPostCategory);
                    _postCategoryService.Save();

                    response = Request.CreateResponse(HttpStatusCode.Created, category);
                }
                return response;
            });
        }

        [Route("Update")]
        public HttpResponseMessage Put(HttpRequestMessage Request, PostCategoryViewModel postCategoryVm)
        {
            return CreateHttpResponse(Request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var postCategoryDb = _postCategoryService.GetById(postCategoryVm.ID);
                    postCategoryDb.UpdataPostCategory(postCategoryVm);

                    _postCategoryService.Update(postCategoryDb);
                    _postCategoryService.Save();

                    response = Request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        public HttpResponseMessage Delete(HttpRequestMessage Request, int id)
        {
            return CreateHttpResponse(Request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    _postCategoryService.Delete(id);
                    _postCategoryService.Save();

                    response = Request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }


    }
}