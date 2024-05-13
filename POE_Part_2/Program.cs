// Create an instance of the RecipeManager class
using POE_Part_2;

RecipeManager recipeManager = new RecipeManager();

// Variable to control the loop
bool exit = false;

// Loop to display menu and handle user input
while (!exit)
{
    // Display menu options
    Console.WriteLine("1. Enter recipe");
    Console.WriteLine("2. Display recipe");
    Console.WriteLine("3. Scale Recipe");
    Console.WriteLine("4. Reset quantities");
    Console.WriteLine("5. Clear all data");
    Console.WriteLine("6. Exit");
    Console.WriteLine();

    // Prompt user for choice
    Console.Write("Enter your choice: ");
    string choice = Console.ReadLine();
    Console.WriteLine();

    // Switch statement to perform actions based on user choice
    switch (choice)
    {
        case "1":
            // Call EnterRecipeDetails method to input recipe details
            recipeManager.EnterRecipeDetails();
            break;
        case "2":
            // Call DisplayRecipe method to display the recipe
            recipeManager.DisplayRecipe();
            break;
        case "3":
            // Call ScaleRecipe method to scale the recipe
            recipeManager.ScaleRecipe();
            break;
        case "4":
            // Call ResetQuantities method to reset quantities to original values
            recipeManager.ResetQuantities();
            break;
        case "5":
            // Call ClearData method to clear all recipe data
            recipeManager.ClearData();
            break;
        case "6":
            // Set exit flag to true to exit the loop
            exit = true;
            break;
        default:
            // Handle invalid choice
            Console.WriteLine("Invalid Choice. Please try again");
            Console.WriteLine();
            break;
    }
}

