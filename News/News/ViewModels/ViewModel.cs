using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
namespace News.ViewModels
{
    public abstract partial class ViewModel
    {
        public INavigate Navigation { get; set; }

        internal ViewModel (INavigate navigate)=>Navigation = navigate; 
    }
}
