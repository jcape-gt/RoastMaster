using RoastMaster.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoastManager.Razor.Models
{
    public class IndexModel
    {
        MemoryCoffeeRepository coffeeRepo = MemoryCoffeeRepository.Instance;
        MemoryRoastTypeRepository roastTypeRepo = MemoryRoastTypeRepository.Instance;
        MemorySpeciesRepository speciesRepo = MemorySpeciesRepository.Instance;

        public IEnumerable<RoastType> roastTypeList;
        public Dictionary<Species, List<Coffee>> originCoffeeDict;

        public IndexModel()
        {
            IEnumerable<Coffee> coffeeList;
            IEnumerable<Species> speciesList;
            originCoffeeDict = new Dictionary<Species, List<Coffee>>();

            coffeeList = coffeeRepo.Get();
            roastTypeList = roastTypeRepo.Get();
            speciesList = speciesRepo.Get();

            foreach(Species s in speciesList)
            {
                originCoffeeDict.Add(s, new List<Coffee>());
            }

            foreach(Coffee coffee in coffeeList)
            {
                originCoffeeDict.First(x => x.Key.ID == coffee.SpeciesId).Value.Add(coffee);
            }
        }
    }
}