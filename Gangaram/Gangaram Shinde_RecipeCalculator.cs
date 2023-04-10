using System;
using System.Collections.Generic;

namespace RecipeCalculator
{
    // Define interface for ingredients
    public interface IIngredient
    {
        string Name { get; }
        decimal Price { get; }
        bool IsOrganic { get; }
    }


    public class ProduceIngredient : IIngredient
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsOrganic { get; set; }
    }

    public class MeatPoultryIngredient : IIngredient
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsOrganic { get; set; }
    }

    public class PantryIngredient : IIngredient
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsOrganic { get; set; }
    }

    // Define interface for recipes
    public interface IRecipe
    {
        string Name { get; }
        List<IIngredient> Ingredients { get; }
        decimal CalculateTotalCost(string currencyCode);
    }

    // Implement recipe interface for each recipe
    public class Recipe1 : IRecipe
    {
        public string Name => "Recipe 1";
        public List<IIngredient> Ingredients { get; set; }

        public Recipe1()
        {
            Ingredients = new List<IIngredient>
            {
                new ProduceIngredient { Name = "garlic clove", Price = 0.67M, IsOrganic = true },
                new ProduceIngredient { Name = "lemon", Price = 2.03m, IsOrganic = false },
                new PantryIngredient { Name = "cup of Olive Oil", Price = 3/4m*1.92m, IsOrganic = true },
                new PantryIngredient { Name = " teaspoons of salt", Price = 3/4*0.16m, IsOrganic = false },
                new PantryIngredient { Name = "Pepper", Price =1/2* 0.17m, IsOrganic = false }
            };
        }

        public decimal CalculateTotalCost(string currencyCode)
        {
            decimal subtotal = 0;
            decimal tax = 0;
            decimal discount = 0;

            // Calculate subtotal
            foreach (var ingredient in Ingredients)
            {
                subtotal += ingredient.Price;
            }

            // Calculate tax (except for produce)
            foreach (var ingredient in Ingredients)
            {
                if (!(ingredient is ProduceIngredient))
                {
                    tax += subtotal * 0.086m;
                    break;
                }
            }

            // Calculate discount (5% for organic items)
            foreach (var ingredient in Ingredients)
            {
                if (ingredient.IsOrganic)
                {
                    discount -= subtotal * 0.05m;
                }
            }

            decimal totalCost = subtotal + tax - discount;

            // Convert to desired currency
            switch (currencyCode)
            {
                case "USD":
                    return totalCost;
                case "INR":
                    return totalCost * 81.80m;
                case "MXN":
                    return totalCost * 20.09m;
                default:
                    throw new ArgumentException($"Unsupported currency code: {currencyCode}");
            }
        }
    }
    public class Recipe2 : IRecipe
    {
        public string Name => "Recipe 1";
        public List<IIngredient> Ingredients { get; set; }

        public Recipe2()
        {
            Ingredients = new List<IIngredient>
            {
                new ProduceIngredient { Name = "garlic clove", Price = 0.67M, IsOrganic = true },
                new ProduceIngredient { Name = "chicken breasts", Price = 4*2.29m, IsOrganic = false },
                new PantryIngredient { Name = "cup olive oil", Price = 1.92m, IsOrganic = true },
                new PantryIngredient { Name = "Salt", Price = 0.5m*0.16m, IsOrganic = false },
                new PantryIngredient { Name = "cup vinegar", Price = 0.5m*1.26m, IsOrganic = false }
            };
        }

  

        public decimal CalculateTotalCost(string currencyCode)
        {
            decimal subtotal = 0;
            decimal tax = 0;
            decimal discount = 0;

            // Calculate subtotal
            foreach (var ingredient in Ingredients)
            {
                subtotal += ingredient.Price;
            }

            // Calculate tax (except for produce)
            foreach (var ingredient in Ingredients)
            {
                if (!(ingredient is ProduceIngredient))
                {
                    tax += subtotal * 0.086m;
                    break;
                }
            }

            // Calculate discount (-5% for organic items)
            foreach (var ingredient in Ingredients)
            {
                if (ingredient.IsOrganic)
                {
                    discount -= subtotal * 0.05m;
                }
            }

            decimal totalCost = subtotal + tax - discount;

            // Convert to desired currency
            switch (currencyCode)
            {
                case "USD":
                    return totalCost;
                case "INR":
                    return totalCost * 81.80m;
                case "MXN":
                    return totalCost * 20.09m;
                default:
                    throw new ArgumentException($"Unsupported currency code: {currencyCode}");
            }
        }
    }

}