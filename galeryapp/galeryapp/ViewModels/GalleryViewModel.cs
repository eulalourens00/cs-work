using galeryapp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using galeryapp.ViewModels;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using galeryapp.Models;
using galeryapp.ViewModels;
namespace galeryapp.ViewModels
{
    public partial class GalleryViewModel: ViewModel
    {
        private readonly IPhotoImporter photoImporter;

        [ObservableProperty]
        public ObservableCollection<Photo> photos;

        [ObservableProperty]
        public ILocalStorage localStorage;
        public GalleryViewModel(IPhotoImporter photoImporter, ILocalStorage localStorage):
            base()
        {
            this.photoImporter = photoImporter;
            this.localStorage = localStorage;
        }

        protected internal override async Task Initialize()
        {
            IsBusy = true;
            Photos = await photoImporter.Get(0, 20);

            Photos.CollectionChanged += Photos_CollectionChanged;
            await Task.Delay(3000);

            IsBusy = false;
        }

        private void Photos_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.NewItems != null && e.NewItems.Count > 0)
            {
                IsBusy = false;
                Photos.CollectionChanged -= Photos_CollectionChanged;
            }
        }

        private int currentStartIndex = 0;

        [RelayCommand]
        public async Task LoadMore()
        {
            currentStartIndex += 20;
            itemsAdded = 0;
            var collection = await photoImporter.Get(currentStartIndex, 20);
            collection.CollectionChanged += Collection_CollectionChanged;
        }
        private int itemsAdded;

        private void Collection_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach(Photo photo in e.NewItems)
            {
                itemsAdded++;
                Photos.Add(photo);
            }
        }
    }
}
