using Xunit;

namespace POE_Part_2.Tests
{
    public class RecipeManagerTests
    {
        [Fact]
        public void Test_CalculateTotalCalories()
        {
            // Arrange
            var recipeManager = new RecipeManager();
            var recipe = new RecipeManager.Recipe();
            recipe.Ingredients = new System.Collections.Generic.List<RecipeManager.Ingredient>
            {
                new RecipeManager.Ingredient { Name = "Ingredient1", Quantity = 1, Calories = 100 },
                new RecipeManager.Ingredient { Name = "Ingredient2", Quantity = 2, Calories = 50 }
            };

            // Act
            var totalCalories = recipeManager.CalculateTotalCalories(recipe);

            // Assert
            Assert.Equal(250, totalCalories);
        }

        // Add more test methods as needed for other functionalities of RecipeManager
    }
}
