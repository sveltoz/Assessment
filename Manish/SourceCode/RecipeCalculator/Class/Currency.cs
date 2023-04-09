using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeCalculator.Class
{
    public class Currency
    {
        private int _id;
        private string _country;
        private string _currencyCode;
        private float _conversionFactor;
        public string Country { get { return _country; }  }

        public string CurrencyCode { get { return _currencyCode; } }
        public float ConversionFactor { get { return _conversionFactor; } }
        public Currency (int id, string country, float conversionFactor, string currencyCode)
        {
            _id = id;
            _country = country;
            _currencyCode = currencyCode;
            _conversionFactor = conversionFactor;
        }
    }
}