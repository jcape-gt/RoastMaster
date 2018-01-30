using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoastMaster.Data
{
    // Meant for testing so no context class exists to handle data processing.
    // Instead, a list is initialized with hardcoded values for querying.
    public class MemoryCoffeeRepository : ICoffeeRepository
    {
        protected List<Coffee> coffeeList;

        // Singleton pattern forces a unique instance of this repository 
        private static MemoryCoffeeRepository instance;
        public static MemoryCoffeeRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new MemoryCoffeeRepository();

                return instance;
            }
        }

        private MemoryCoffeeRepository()
        {
            // Hardcoded values are loaded into list
            InitializeCoffeeList();
        }

        // Create a static list of coffees with hardcoded values
        private void InitializeCoffeeList()
        {
            coffeeList = new List<Coffee>();
            coffeeList.Add(new Coffee { Name = "Guatemala Huehuetenango", ID = 1, SpeciesId = 1 });
            coffeeList.Add(new Coffee { Name = "Guatemala Acatenango Gesha", ID = 2, SpeciesId = 1 });
            coffeeList.Add(new Coffee { Name = "Hawaiian Kona", ID = 3, SpeciesId = 1 });
            coffeeList.Add(new Coffee { Name = "Guadeloupe Bonifieur", ID = 4, SpeciesId = 1 });
            coffeeList.Add(new Coffee { Name = "Ethiopian Harar", ID = 5, SpeciesId = 2 });
            coffeeList.Add(new Coffee { Name = "Ethiopian Sidamo", ID = 6, SpeciesId = 2 });
            coffeeList.Add(new Coffee { Name = "Sumatra Lintong", ID = 7, SpeciesId = 2 });
            coffeeList.Add(new Coffee { Name = "Sumatra Mandheling ", ID = 8, SpeciesId = 2 });
        }
    
        public void Delete(int id)
        {
            coffeeList.RemoveAll(x => x != null && x.ID == id);
        }

        // Fine for an in memory only repository, but reference and shallow copies should not be
        // returned for persistent processing
        public IEnumerable<Coffee> Get()
        {
            return coffeeList;
        }

        public Coffee GetByID(int id)
        {
            return coffeeList.FirstOrDefault(x => x.ID == id);
        }

        public void Insert(Coffee coffee)
        {
            coffeeList.Add(coffee);
        }

        // Use the ID of coffee to map and update
        public void Update(Coffee coffee)
        {
            Coffee itemToUpdate = coffeeList.FirstOrDefault(x => x.ID == coffee.ID);

            if (itemToUpdate != null)
            {
                itemToUpdate.Name = coffee.Name;
                itemToUpdate.SpeciesId = coffee.SpeciesId;
            }
        }

        // No save logic for a memory only repository
        public void Save() { }
    }
}
