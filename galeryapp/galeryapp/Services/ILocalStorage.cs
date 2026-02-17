using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace galeryapp.Services
{
    public interface ILocalStorage
    {
        void Store(string filename);
        List<string> Get();
    }
}
