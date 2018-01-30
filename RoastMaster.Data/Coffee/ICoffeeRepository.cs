using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoastMaster.Data
{
    public interface ICoffeeRepository
    {
        IEnumerable<Coffee> Get();
        Coffee GetByID(int id);
        void Insert(Coffee coffee);
        void Update(Coffee coffee);
        void Delete(int id);
        void Save();
    }
}
