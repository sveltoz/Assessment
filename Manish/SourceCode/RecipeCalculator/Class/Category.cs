using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeCalculator.Class
{
    public class Category
    {
        private int _id;
        private string _name;

        public int Id { get { return _id; } }
        public string Name { get { return _name; } }
        public Category(int id, string name)
        {
            _id = id;
            _name = name;
        }

    }
}