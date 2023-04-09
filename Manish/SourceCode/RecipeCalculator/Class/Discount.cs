using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace RecipeCalculator.Class
{
    public class Discount
    {
        private int _id;
        private string _name;
        private float _percentage;

        public int Id { get { return _id; } }
        public string Name { get { return _name; } }
        public float Percentage { get { return _percentage; } }

        public Discount(int id, string name, float percentage)
        {
            _id = id;
            _name = name;
            _percentage = percentage;
        }
    }
}