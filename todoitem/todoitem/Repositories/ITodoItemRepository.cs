using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todoitem.Models;

namespace todoitem.Repositories
{
    internal interface ITodoItemRepository
    {
        event EventHandler<TodoItem> OnItemAdded;
        event EventHandler<TodoItem> OnItemUpdated;

        public async Task AddItemAsync (TodoItem item)
        {
            throw new NotImplementedException();
        }
        Task AddItemAsync(TodoItem item);
        Task UpdateItemAsync(TodoItem item);
        Task AddOrUpdateAsync (TodoItem item);
    }
}
