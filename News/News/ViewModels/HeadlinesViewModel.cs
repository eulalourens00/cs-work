using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using News.Services;
using News.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace News.ViewModels
{
    public partial class HeadlinesViewModel: ViewModel
    {
        private readonly INewService newService;
        private NewResult currentNews;

        public HeadlinesViewModel(INewService newService, INavigate navigate):
            base(navigate)
        {
            this.newService = newService;
        }

        public async Task Initialize(string scope) => await Initialize(scope.ToLower() switch
        {
            "local"=> NewScope.Local,
            "global"=>NewScope.Global,
            "headlines"=>NewScope.Headlines,
            _=> NewScope.Headlines
        });

        public async Task Initialize(NewScope scope)
        {
            currentNews = await newService.GetNews(scope);  
        }

        [RelayCommand]
        public async Task ItemSelected(object selectedItem)
        {
            var selectedArticle = selectedArticle as Article;
            var url = HttpUtility.UrlEncode(selectedItem.Url);
            await Navigation.NavigateTo($"articleview?url={url}");
        }
    }
}
