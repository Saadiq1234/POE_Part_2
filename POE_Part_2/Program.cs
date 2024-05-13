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
            // Prompt user for the name of the recipe to display
            Console.Write("Enter the name of the recipe to display: ");
            string recipeName = Console.ReadLine();
            // Call DisplayRecipe method to display the recipe
            recipeManager.DisplayRecipe(recipeName);
            break;

        case "3":
            // Prompt user for the name of the recipe to scale
            Console.Write("Enter the name of the recipe to scale: ");
            string recipeToScale = Console.ReadLine();
            // Call ScaleRecipe method to scale the recipe
            recipeManager.ScaleRecipe(recipeToScale);
            break;

        case "4":
            // Prompt user for the name of the recipe to reset quantities
            Console.Write("Enter the name of the recipe to reset quantities: ");
            string recipeToReset = Console.ReadLine();
            // Call ResetQuantities method to reset quantities to original values
            recipeManager.ResetQuantities(recipeToReset);
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
