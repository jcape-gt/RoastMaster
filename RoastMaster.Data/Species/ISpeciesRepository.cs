using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoastMaster.Data
{
    public interface ISpeciesRepository
    {
        IEnumerable<Species> Get();
        Species GetByID(int id);
        void Insert(Species species);
        void Update(Species species);
        void Delete(int id);
        void Save();
    }
}
