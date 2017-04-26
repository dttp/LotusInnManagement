using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusInn.Management.Data
{
    public interface IObjectDataAdapter<T>
    {
        T Insert(T @object);
        void Update(T @object);

        void Delete(string id);
        T GetById(string id);
    }
}
