using Product.Common;
using Product.Data.Infrastructure;
using Product.Data.Repositories;
using Product.Model.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Product.Service
{
    public interface IItemService
    {
        Item Add(Item item);

        void Update(Item item);

        Item Delete(int id);

        IEnumerable<Item> GetAll();

        IEnumerable<Item> GetAll(string keyword);

        IEnumerable<Item> GetLastest(int top);

        IEnumerable<Item> GetHotItem(int top);

        IEnumerable<Item> GetListItemByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow);

        IEnumerable<Item> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        IEnumerable<Item> GetListItem(string keyword);

        IEnumerable<Item> GetReatedItems(int id, int top);

        IEnumerable<string> GetListItemByName(string name);

        Item GetById(int id);

        void Save();
        
    }

    public class ItemService : IItemService
    {
        private IItemRepository _ItemRepository;
        private ITagRepository _tagRepository;
        private IItemTagRepository _ItemTagRepository;

        private IUniOfWork _unitOfWork;

        public ItemService(IItemRepository ItemRepository, IItemTagRepository ItemTagRepository,
            ITagRepository _tagRepository, IUniOfWork unitOfWork)
        {
            this._ItemRepository = ItemRepository;
            this._ItemTagRepository = ItemTagRepository;
            this._tagRepository = _tagRepository;
            this._unitOfWork = unitOfWork;
        }

        public Item Add(Item Item)
        {
            var item = _ItemRepository.Add(Item);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(Item.Tags))
            {
                string[] tags = Item.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }

                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = Item.ID;
                    productTag.TagID = tagId;
                    _ItemTagRepository.Add(productTag);
                }
            }
            return Item;
        }

        public Item Delete(int id)
        {
            return _ItemRepository.Delete(id);
        }

        public IEnumerable<Item> GetAll()
        {
            return _ItemRepository.GetAll();
        }

        public IEnumerable<Item> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _ItemRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _ItemRepository.GetAll();
        }

        public Item GetById(int id)
        {
            return _ItemRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Item Item)
        {
            _ItemRepository.Update(Item);
            if (!string.IsNullOrEmpty(Item.Tags))
            {
                string[] tags = Item.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }
                    _ItemTagRepository.DeleteMulti(x => x.ProductID == Item.ID);
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = Item.ID;
                    productTag.TagID = tagId;
                    _ItemTagRepository.Add(productTag);
                }
            }
        }

        public IEnumerable<Item> GetLastest(int top)
        {
            return _ItemRepository.GetMulti(x => x.Status).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Item> GetHotItem(int top)
        {
            return _ItemRepository.GetMulti(x => x.Status && x.HotFlag == true).OrderByDescending(x => x.CreatedDate).Take(top);

        }

        public IEnumerable<Item> GetListItemByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _ItemRepository.GetMulti(x => x.Status && x.CategoryID == categoryId);

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "discount":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;
                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<string> GetListItemByName(string name)
        {
            return _ItemRepository.GetMulti(x => x.Status && x.Name.Contains(name)).Select(y => y.Name);
        }

        public IEnumerable<Item> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _ItemRepository.GetMulti(x => x.Status && x.Name.Contains(keyword));

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "discount":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;
                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Item> GetReatedItems(int id, int top)
        {
            var item = _ItemRepository.GetSingleById(id);
            return _ItemRepository.GetMulti(x => x.Status && x.ID != id && x.CategoryID == item.CategoryID).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Tag> GetListTagByItemId(int id)
        {
            return _ItemTagRepository.GetMulti(x => x.ProductID == id, new string[] { "Tag" }).Select(y => y.Tag);
        }

        public void IncreaseView(int id)
        {
            var item = _ItemRepository.GetSingleById(id);
            if (item.ViewCount.HasValue)
                item.ViewCount += 1;
            else
                item.ViewCount = 1;
        }

        public IEnumerable<Item> GetListItemByTag(string tagId, int page, int pageSize, out int totalRow)
        {
            var model = _ItemRepository.GetListProductByTag(tagId, page, pageSize, out totalRow);
            return model;
        }

        public Tag GetTag(string tagId)
        {
            return _tagRepository.GetSingleByCondition(x => x.ID == tagId);
        }

        //Selling product
        //public bool SellProduct(int productId, int quantity)
        //{
        //    var product = _ItemRepository.GetSingleById(productId);
        //    if (product.Quantity < quantity)
        //        return false;
        //    product.Quantity -= quantity;
        //    return true;
        //}

        public IEnumerable<Item> GetListItem(string keyword)
        {
            IEnumerable<Item> query;
            if (!string.IsNullOrEmpty(keyword))
                query = _ItemRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                query = _ItemRepository.GetAll();
            return query;
        }
        
    }
}