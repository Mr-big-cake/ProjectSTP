using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSTP.Interfaces
{
    internal interface IRepository<T> where T : class
    {
        string ConnectionString { get; set; }
        T[] GetItemsList();
        T GetItem(int id);
        void Create(T item);
        void Update(T item); 
        void Delete(int id); 
        void Save();
    }
}
