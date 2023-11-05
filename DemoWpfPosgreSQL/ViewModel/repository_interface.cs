using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoWpfPosgreSQL.ViewModel
{
    public interface IRepository<T> where T:class
    {
        ICollection<T> GetAll();
        void Add(T item);
        void Update(T new_item);
        void Delete(T item);
    }
}
