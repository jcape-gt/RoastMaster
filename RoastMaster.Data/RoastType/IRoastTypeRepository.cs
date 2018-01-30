using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoastMaster.Data
{
    public interface IRoastTypeRepository
    {
        IEnumerable<RoastType> Get();
        RoastType GetByID(int id);
        void Insert(RoastType roastType);
        void Update(RoastType roastType);
        void Delete(int id);
        void Save();
    }
}
