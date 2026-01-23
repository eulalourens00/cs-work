using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todoitem.Models;
using todoitem.Repositories;

namespace todoitem.ViewModels
{
    internal class ItemViewModel: ViewModel
    {
        private readonly ITodoItemRepository repository;

        [ObservableProperty]
        private TodoItem item;

        public ItemViewModel(ITodoItemRepository repository)
        {
            this.repository = repository;
            Item = new TodoItem
            {
                Due = DateTime.Now.AddDays(1)
            };
        }

        [RelayCommand]
        public async Task SaveAsync()
        {
            await repository.AddOrUpdateAsync(Item);
            await Navigation.PopAsync();
        }
    }
}
