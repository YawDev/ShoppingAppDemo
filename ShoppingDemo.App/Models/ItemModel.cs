using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Shopper.App.Models
{
    public class ItemModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public bool inStock { get; set; }

        public int Quantity { get; set; }
    }

    public class AddItemModel : ItemModel
    {
        public IFormFile imageFile {get;set;}
    }

    public class EditItemModel : AddItemModel
    {
    }
}
