using R12.Models;
using R12.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace R12.ViewModels
{
    public class ItemViewModel : BaseViewModel
    {
        private ItemModel item;
        public ItemModel Item
        {
            get => item;
            set
            {
                item = value;
                OnPropertyChanged();
            }
        }
        public int identifier { get; set; }
        private CategoryModel category;
        public CategoryModel Category
        {
            get => category;
            set
            {
                category = value;
                OnPropertyChanged();
            }
        }
        private EntityModel entity;
        public EntityModel Entity
        {
            get => entity;
            set
            {
                entity = value;
                OnPropertyChanged();
            }
        }
        public ItemViewModel(int id)
        {
            identifier = id;
            Title = "Datos de Articulo";
        }
        public async Task OnAppear()
        {
            Item = await Repository.Item_GetByIdAsync(this.identifier);
            category = await Repository.Category_GetById((int)Item.CategoryId);
            entity = await Repository.Entity_GetByIdAsync((int)Item.EntityId);
            Category = category;
            Entity = entity;
        }
    }
}
