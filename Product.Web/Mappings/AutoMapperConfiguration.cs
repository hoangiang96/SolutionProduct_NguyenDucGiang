using AutoMapper;
using Product.Model.Models;
using Product.Web.Models;
using System.Collections.Generic;

namespace Product.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<List<Post>, List<PostViewModel>>();
                cfg.CreateMap<List<PostCategory>, List<PostCategoryViewModel>>();
                cfg.CreateMap<List<Tag>, List<TagViewModel>>();

                cfg.CreateMap<ProductCategory, ProductCategoryViewModel>();
                cfg.CreateMap<Item, itemViewModel>();
                cfg.CreateMap<ProductTag, ProductTagViewModel>();
                //cfg.CreateMap<Footer, FooterViewModel>();
            });
        }
    }
}