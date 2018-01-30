using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoastMaster.Data
{
    // Meant for testing so no context class exists to handle data processing.
    // Instead, a list is initialized with hardcoded values for querying.
    public class MemorySpeciesRepository : ISpeciesRepository
    {
        protected List<Species> speciesList;

        // Singleton pattern forces a unique instance of this repository 
        private static MemorySpeciesRepository instance;
        public static MemorySpeciesRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new MemorySpeciesRepository();

                return instance;
            }
        }

        private MemorySpeciesRepository()
        {
            InitializeSpeciesList();
        }

        private void InitializeSpeciesList()
        {
            speciesList = new List<Species>();
            speciesList.Add(new Species { ID = 1, Name = "Coffea Arabica", RoastTime = 420 });
            speciesList.Add(new Species { ID = 2, Name = "Coffea Robusta", RoastTime = 480 });
        }

        public void Delete(int id)
        {
            speciesList.RemoveAll(x => x.ID == id);
        }

        public IEnumerable<Species> Get()
        {
            return speciesList;
        }

        public Species GetByID(int id)
        {
            return speciesList.FirstOrDefault(x => x.ID == id);
        }

        public void Insert(Species species)
        {
            speciesList.Add(species);
        }

        // Use the ID of species to map and update
        public void Update(Species species)
        {
            Species itemToUpdate = speciesList.FirstOrDefault(x => x.ID == species.ID);

            if (itemToUpdate != null)
            {
                itemToUpdate.Name = species.Name;
                itemToUpdate.RoastTime = species.RoastTime;
            }
        }

        // No save logic for a memory only repository
        public void Save() { }
    }
}
