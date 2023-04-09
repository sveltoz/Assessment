using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace RecipeCalculator.Class
{
    public class Ingredient
    {
        private int _id;
        private string _name;
        private float _quantity;
        private float _price;
        private string _unit;
        private List<Discount> _discounts;
        private List<Category> _categories;
        private List<TaxAndSurcharge> _taxAndSurcharges;
        private Currency _currency;

        public int Id { get { return _id; } }
        public string Name { get { return _name; } }
        public float Quantity { get { return _quantity; } }
        public float Price { get { return _price; } }

        public string Unit { get { return _unit; } }

        public Currency Currency { get { return _currency; } }

        public Ingredient(int id, string name, string unit, float quantity, float price, List<Discount> discounts, List<Category> categories, List<TaxAndSurcharge> taxAndSurcharges, Currency currency)
        {
            _id = id;
            _name = name;
            _quantity = quantity;
            _price = price;
            _unit = unit;
            _discounts = discounts;
            _categories = categories;
            _taxAndSurcharges = taxAndSurcharges;
            _currency = currency;
        }

        public float GetGrossAmount()
        {
            return _price * _quantity;
        }
        public float GetTotalPrice()
        {
            return GetGrossAmount() - GetDiscountAmount() + GetTaxAndSurcharge();
        }
        public float GetTaxAndSurcharge()
        {
            if (_taxAndSurcharges.Count() > 0)
            {
                float totalTax = 0;
                foreach (var tax in _taxAndSurcharges)
                {
                    totalTax = (GetGrossAmount() - GetDiscountAmount()) * (tax.Percentage / 100);
                }
                return totalTax;
            }
            else
                return 0;
        }
        public float GetDiscountAmount()
        {
            if (_discounts.Count() > 0)
            {
                float totalDiscount = 0;
                foreach (var discount in _discounts)
                {
                    totalDiscount += (GetGrossAmount() * (discount.Percentage / 100));
                }
                return totalDiscount;
            }
            else
                return 0;
        }

        public float CountrySpecificPrice { get { return _price * Currency.ConversionFactor; } }
        public float CountrySpecificDiscount { get { return GetDiscountAmount() * Currency.ConversionFactor; } }
        public float CountrySpecificTax { get { return GetTaxAndSurcharge() * Currency.ConversionFactor; } }
        public float CountrySpecificAmount { get { return GetTotalPrice() * Currency.ConversionFactor; } }
        public float CountrySpecificGrossAmount { get { return GetGrossAmount() * Currency.ConversionFactor; } }


    }
}