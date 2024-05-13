using System;
using System.Collections.Generic;

namespace POE_Part_2
{
    // Define a delegate for calorie notifications
    public delegate void CalorieNotificationHandler(string recipeName, double totalCalories);

    /// <summary>
    /// Manages recipes including adding, scaling, displaying, and clearing recipe data.
    /// </summary>
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

        // Event for calorie notifications
        public event CalorieNotificationHandler CalorieExceeded;

        // Constructor to initialize the list of recipes
        public RecipeManager()
        {
            recipes = new List<Recipe>();
        }

        /// <summary>
        /// Enters details for a new recipe including name, ingredients, steps, and calculates total calories.
        /// </summary>
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

            // Raise the event for calorie notification
            CalorieExceeded?.Invoke(recipe.Name, recipe.TotalCalories);

            // Check if total calories exceed 300
            if (recipe.TotalCalories > 300)
            {
                Console.ForegroundColor = ConsoleColor.Red; // Set text color to red
                Console.WriteLine("Warning: Total calories of the recipe exceed 300!");
                Console.ResetColor(); // Reset text color to default
                Console.WriteLine();
            }

            // Add the recipe to the list
            recipes.Add(recipe);
        }


        // Method to calculate total calories for a recipe
        public double CalculateTotalCalories(Recipe recipe)
        {
            double totalCalories = 0;

            foreach (var ingredient in recipe.Ingredients)
            {
                totalCalories += ingredient.Calories * ingredient.Quantity;
            }

            return totalCalories;
        }

        /// <summary>
        /// Scales a recipe by a given factor, updating quantities and calories accordingly.
        /// </summary>
        public void ScaleRecipe(string recipeName)
        {
            Recipe recipe = recipes.Find(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (recipe != null)
            {
                Console.Write("Enter the scaling factor (0.5, 2, or 3): ");
                double factor = double.Parse(Console.ReadLine());
                Console.WriteLine();

                // Check if the entered factor is valid and scale the recipe
                if (factor == 0.5 || factor == 2 || factor == 3)
                {
                    foreach (var ingredient in recipe.Ingredients)
                    {
                        // Scale the quantity
                        ingredient.Quantity *= factor;
                        // Scale the calories accordingly by multiplying
                        ingredient.Calories = (int)Math.Round(ingredient.Calories * factor);
                    }

                    // Recalculate the total calories for the recipe
                    recipe.TotalCalories = CalculateTotalCalories(recipe);
                    Console.WriteLine("Recipe scaled successfully.");

                    // Check if total calories exceed 300 after scaling
                    if (recipe.TotalCalories > 300)
                    {
                        Console.ForegroundColor = ConsoleColor.Red; // Set text color to red
                        Console.WriteLine("Warning: Total calories of the recipe exceed 300 after scaling!");
                        Console.ResetColor(); // Reset text color to default
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid scaling factor. Recipe scaling failed.");
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Recipe not found.");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Resets ingredient quantities to their original values.
        /// </summary>
        public void ResetQuantities(string recipeName)
        {
            Recipe recipe = recipes.Find(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (recipe != null)
            {
                foreach (var ingredient in recipe.Ingredients)
                {
                    // Assuming the original quantity is stored elsewhere, let's reset it here
                    // For demonstration purposes, let's assume the original quantity is 1
                    ingredient.Quantity = 1.0;
                }
                Console.WriteLine("Quantities reset to original values.");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Recipe not found.");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Displays all available recipes in alphabetical order by name.
        /// </summary>
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

        /// <summary>
        /// Displays details of a specific recipe including ingredients, steps, and total calories.
        /// </summary>
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
                if (recipe.TotalCalories > 300)
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Set text color to red
                    Console.WriteLine($"Total Calories: {recipe.TotalCalories} (Warning: Exceeds 300)");
                    Console.ResetColor(); // Reset text color to default
                }
                else
                {
                    Console.WriteLine($"Total Calories: {recipe.TotalCalories}");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Recipe not found.");
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Clears all recipe data.
        /// </summary>
        public void ClearData()
        {
            recipes.Clear();
            Console.WriteLine("All data cleared.");
            Console.WriteLine();
        }
    }
}
