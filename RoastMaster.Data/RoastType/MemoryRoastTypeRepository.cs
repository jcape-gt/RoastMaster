using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoastMaster.Data
{
    // Meant for testing so no context class exists to handle data processing.
    // Instead, a list is initialized with hardcoded values for querying.
    public class MemoryRoastTypeRepository : IRoastTypeRepository
    {
        protected List<RoastType> roastTypeList;

        // Singleton pattern forces a unique instance of this repository 
        private static MemoryRoastTypeRepository instance;
        public static MemoryRoastTypeRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new MemoryRoastTypeRepository();

                return instance;
            }
        }

        private MemoryRoastTypeRepository()
        {
            InitializeRoastTypeList();
        }

        private void InitializeRoastTypeList()
        {
            roastTypeList = new List<RoastType>();

            roastTypeList.Add(new RoastType { ID = 1, Name = "Green", RoastPercentage = 0.0 });
            roastTypeList.Add(new RoastType { ID = 2, Name = "American", RoastPercentage = 0.4 });
            roastTypeList.Add(new RoastType { ID = 3, Name = "City", RoastPercentage = 0.5 });
            roastTypeList.Add(new RoastType { ID = 4, Name = "City+", RoastPercentage = 0.55 });
            roastTypeList.Add(new RoastType { ID = 5, Name = "Full City", RoastPercentage = 0.6 });
            roastTypeList.Add(new RoastType { ID = 6, Name = "Full City+", RoastPercentage = 0.65 });
            roastTypeList.Add(new RoastType { ID = 7, Name = "Vienna", RoastPercentage = 0.75 });
            roastTypeList.Add(new RoastType { ID = 8, Name = "Charcoal", RoastPercentage = 1.0 });
        }

        public void Delete(int id)
        {
            roastTypeList.RemoveAll(x => x.ID == id);
        }

        public IEnumerable<RoastType> Get()
        {
            return roastTypeList;
        }

        public RoastType GetByID(int id)
        {
            return roastTypeList.FirstOrDefault(x => x.ID == id);
        }

        public void Insert(RoastType roastType)
        {
            roastTypeList.Add(roastType);
        }

        public void Update(RoastType roastType)
        {
            // Use the ID of roastType to map and update
            RoastType itemToUpdate = roastTypeList.FirstOrDefault(x => x.ID == roastType.ID);

            if (itemToUpdate != null)
            {
                itemToUpdate.Name = roastType.Name;
                itemToUpdate.RoastPercentage = roastType.RoastPercentage;
            }
        }

        // No save logic for a memory only repository
        public void Save() { }
    }
}
