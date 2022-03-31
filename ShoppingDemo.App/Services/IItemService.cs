using System;
using Shopper.App.Models;
using ShoppingDemo.App.Data.Entites;
using ShoppingDemo.EFCore;

namespace ShoppingDemo.App.Services
{
    public interface IItemService
    {
         void CreateItem(AddItemModel model);
         void EditItem(EditItemModel model);
         Item FindItem(Guid Id);
         void DeleteItem(Item item);
    }

    public class ItemService : IItemService
    {
        IUploadService _uploadService;
        IItemRepository _itemRepository;

        IShoppingCartItemRepository _shoppingCartItemRepository;

        public ItemService(IUploadService uploadService, IItemRepository itemRepository,
        IShoppingCartItemRepository shoppingCartItemRepository)
        {
            _uploadService = uploadService;
            _itemRepository = itemRepository;
            _shoppingCartItemRepository = shoppingCartItemRepository;
        }

        public void CreateItem(AddItemModel model)
        {
            var item =  new Item
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Quantity = model.Quantity,
                inStock = model.inStock,
            }; 

            if(model.imageFile?.Length > 0)
            {
                item.Image = _uploadService.ToByteArray(model.imageFile);
                item.FileName = _uploadService.Upload(model.imageFile);
            } 
                _itemRepository.Add(item);
                _itemRepository.Commit();
        }


        public void EditItem(EditItemModel model)
        {
            var item = _itemRepository.GetById(model.Id);
            item.Name = model.Name;
            item.Price = model.Price;
            item.Quantity = model.Quantity;
            item.inStock = model.inStock;
            item.Description = model.Description;

            if(model.imageFile?.Length > 0)
            {
                item.Image = _uploadService.ToByteArray(model.imageFile);
                item.FileName = _uploadService.Upload(model.imageFile);
            }
            
            _itemRepository.Commit();
        }

        public Item FindItem(Guid Id)
        {
            return _itemRepository.GetById(Id);
        }

        public void DeleteItem(Item item)
        {
            var cartItems =_shoppingCartItemRepository.GetAllByItemId(item.Id);
            if(cartItems.Count >0)
                _shoppingCartItemRepository.DeleteRange(cartItems);

           _itemRepository.Delete(item);
           _itemRepository.Commit();
        }
    }

}