using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Z_P_B_1.Services.Interfaces
{
    public interface IDataMainServices<T>
    {
        Task<IEnumerable<T>> GetItems(bool mustRefresh = false);
        IEnumerable<T> Search(string searchText);
    }
}
