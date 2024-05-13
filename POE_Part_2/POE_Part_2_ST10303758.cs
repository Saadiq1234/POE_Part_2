﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE_Part_2
{
    internal class RecipeManager
    {
        // Define a class to hold recipe details
        internal class Recipe
        {
            public string Name { get; set; }
            public List<Ingredient> Ingredients { get; set; }
            public List<string> Steps { get; set; }
            public double TotalCalories { get; set; }
        }

        // Define a class to hold ingredient details
        internal class Ingredient
        {
            public string Name { get; set; }
            public double Quantity { get; set; }
            public string Unit { get; set; }
            public int Calories { get; set; }
            public string FoodGroup { get; set; }
        }

        // Private field to store recipes
        private List<Recipe> recipes;

        // Constructor to initialize the list of recipes
        public RecipeManager()
        {
            recipes = new List<Recipe>();
        }

        // Method to enter recipe details
        public void EnterRecipeDetails()
        {
            Recipe recipe = new Recipe();
            Console.Write("Enter the name of the recipe: ");
            recipe.Name = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Enter the number of ingredients: ");
            int numIngredients = int.Parse(Console.ReadLine());
            Console.WriteLine();

            recipe.Ingredients = new List<Ingredient>();

            // Loop to input details for each ingredient
            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine($"Enter details for ingredient {i + 1}:");

                Ingredient ingredient = new Ingredient();

                Console.Write("Name: ");
                ingredient.Name = Console.ReadLine();

                Console.Write("Quantity: ");
                ingredient.Quantity = double.Parse(Console.ReadLine());

                Console.Write("Unit of measurement: ");
                ingredient.Unit = Console.ReadLine();

                Console.Write("Calories: ");
                ingredient.Calories = int.Parse(Console.ReadLine());

                Console.Write("Food group: ");
                ingredient.FoodGroup = Console.ReadLine();

                // Add the ingredient to the recipe
                recipe.Ingredients.Add(ingredient);

                Console.WriteLine();
            }

            Console.Write("Enter the number of steps: ");
            int numSteps = int.Parse(Console.ReadLine());
            Console.WriteLine();

            recipe.Steps = new List<string>();

            // Loop to input recipe steps
            for (int i = 0; i < numSteps; i++)
            {
                Console.Write($"Enter step {i + 1}: ");
                recipe.Steps.Add(Console.ReadLine());
                Console.WriteLine();
            }

            // Calculate total calories for the recipe
            recipe.TotalCalories = CalculateTotalCalories(recipe);

            // Check if total calories exceed 300
            if (recipe.TotalCalories > 300)
            {
                Console.WriteLine("Warning: Total calories of the recipe exceed 300!");
                Console.WriteLine();
            }

            // Add the recipe to the list
            recipes.Add(recipe);
        }

        // Method to calculate total calories for a recipe
        private double CalculateTotalCalories(Recipe recipe)
        {
            double totalCalories = 0;

            foreach (var ingredient in recipe.Ingredients)
            {
                totalCalories += ingredient.Calories * ingredient.Quantity;
            }

            return totalCalories;
        }

        // Method to display all recipes in alphabetical order by name
        public void DisplayAllRecipes()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes available.");
                Console.WriteLine();
                return;
            }

            recipes.Sort((x, y) => x.Name.CompareTo(y.Name));

            Console.WriteLine("Available Recipes:");
            foreach (var recipe in recipes)
            {
                Console.WriteLine(recipe.Name);
            }
            Console.WriteLine();
        }

        // Method to display a specific recipe
        public void DisplayRecipe(string recipeName)
        {
            Recipe recipe = recipes.Find(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (recipe != null)
            {
                Console.WriteLine($"Recipe: {recipe.Name}");
                Console.WriteLine();

                // Display ingredients
                Console.WriteLine("Ingredients:");
                foreach (var ingredient in recipe.Ingredients)
                {
                    Console.WriteLine($"{ingredient.Name}: {ingredient.Quantity} {ingredient.Unit}");
                }
                Console.WriteLine();

                // Display steps
                Console.WriteLine("Steps:");
                for (int i = 0; i < recipe.Steps.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {recipe.Steps[i]}");
                }
                Console.WriteLine();

                // Display total calories
                Console.WriteLine($"Total Calories: {recipe.TotalCalories}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Recipe not found.");
                Console.WriteLine();
            }
        }

        // Method to clear all recipe data
        public void ClearData()
        {
            recipes.Clear();
            Console.WriteLine("All data cleared.");
            Console.WriteLine();
        }
    }
}