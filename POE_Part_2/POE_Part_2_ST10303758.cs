using System;
using System.Collections.Generic;

namespace POE_Part_2
{
    // Define a delegate for calorie notifications
    public delegate void CalorieNotificationHandler(string recipeName, double totalCalories);

    /// <summary>
    /// Class to manage recipes including adding, scaling, displaying, and clearing recipe data.
    /// </summary>
    public class RecipeManager
    {
        // Define a class to hold recipe details
        public class Recipe
        {
            public string Name { get; set; }
            public List<Ingredient> Ingredients { get; set; }
            public List<string> Steps { get; set; }
            public double TotalCalories { get; set; }
        }

        // Define a class to hold ingredient details
        public class Ingredient
        {
            public string Name { get; set; }
            public double Quantity { get; set; }
            public double OriginalQuantity { get; set; } // Added property to store original quantity
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
        /// Adds a new recipe with details provided by the user.
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

                ingredient.OriginalQuantity = ingredient.Quantity; // Store original quantity

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

        /// <summary>
        /// Calculates total calories for a recipe.
        /// </summary>
        public double CalculateTotalCalories(Recipe recipe)
        {
            double totalCalories = 0;

            foreach (var ingredient in recipe.Ingredients)
            {
                // Multiply the calories of each ingredient by its quantity and add to the total
                totalCalories += (ingredient.Calories * ingredient.Quantity);
            }

            return totalCalories;
        }

        /// <summary>
        /// Scales the quantities of ingredients in a recipe by a given factor.
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
                        if (ingredient.Quantity != 0) // Check if original quantity is not zero
                        {
                            // Store the original quantity and calories for each ingredient
                            double originalQuantity = ingredient.Quantity;
                            int originalCalories = ingredient.Calories;

                            // Scale the quantity
                            ingredient.Quantity *= factor;

                            // Calculate the new calories based on the scaled quantity
                            ingredient.Calories = (int)Math.Round(originalCalories * (ingredient.Quantity / originalQuantity));
                        }
                    }

                    // Recalculate the total calories for the recipe based on the scaled quantities
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
        /// Resets the quantities of ingredients in a recipe to their original values.
        /// </summary>
        public void ResetQuantities(string recipeName)
        {
            Recipe recipe = recipes.Find(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (recipe != null)
            {
                foreach (var ingredient in recipe.Ingredients)
                {
                    // Reset the quantity to its original value
                    ingredient.Quantity = ingredient.OriginalQuantity;

                    // Reset the calories to the original calories per unit
                    // This assumes that the original calories were calculated based on the original quantity
                    ingredient.Calories = (int)Math.Round(ingredient.Calories * (ingredient.Quantity / ingredient.OriginalQuantity));
                }

                // Recalculate the total calories for the recipe based on the reset quantities
                recipe.TotalCalories = CalculateTotalCalories(recipe);

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
        /// Displays all available recipes along with their ingredients, steps, and total calories.
        /// </summary>
        public void DisplayAllRecipes()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes available.");
                Console.WriteLine();
                return;
            }

            // Sort recipes by name
            recipes.Sort((x, y) => x.Name.CompareTo(y.Name));

            Console.WriteLine("Available Recipes:");
            foreach (var recipe in recipes)
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
                Console.WriteLine("--------------------------------------------");
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
        /// Clears all recipe data including ingredient quantities and calories.
        /// </summary>
        public void ClearData()
        {
            foreach (var recipe in recipes)
            {
                ResetQuantities(recipe.Name);
            }
            recipes.Clear();
            Console.WriteLine("All data cleared including ingredient quantities and calories.");
            Console.WriteLine();
        }
    }
}
