using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//This is going to be a temporary way to test and manage data.  Basically modeled directly from CheeseMVC. Will replace with database

namespace BreadBuilder.Models
{
    public class BreadData
    {
        static private List<Bread> breads = new List<Bread>();

        //get all breads
        static public List<Bread> GetAll()
        {
            return breads;
        }

        //add new bread
        public static void Add(Bread newBread)
        {
            breads.Add(newBread);
        }

        //get by bread ID

        public static Bread GetByID(int id)
        {
            return breads.Single(b => b.ID == id);
        }
    }
}
