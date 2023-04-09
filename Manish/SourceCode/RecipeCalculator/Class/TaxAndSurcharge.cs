using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeCalculator.Class
{
    public class TaxAndSurcharge
    {
        private int _Id;
        private string _Name;
        private float _Percentage;

        public int Id { get { return _Id; } }
        public string Name { get { return _Name; } }
        public float Percentage { get { return _Percentage; } }

        public TaxAndSurcharge(int id, string name, float percentage)
        {
            _Id = id;
            _Name = name;
            _Percentage = percentage;
        }   
    }
}