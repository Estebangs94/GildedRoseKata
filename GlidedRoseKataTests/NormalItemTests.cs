using ConsoleApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GlidedRoseKataTests
{
    public class NormalItemTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Normal_Item_Should_Decrease_Quality_By_One(int days)
        {
            // Arrange
            var target = new NormalItem { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 };

            //Act
            for (int i = 0; i < days; i++)
            {
                target.Update();
            }

            // Assert
            Assert.Equal("+5 Dexterity Vest", target.Name);
            Assert.Equal(20 - (1 * days), target.Quality);
        }

        [Theory]
        [InlineData(30)]
        [InlineData(60)]
        [InlineData(90)]
        public void Normal_Item_Should_Decrease_Quality_Up_To_Zero(int days)
        {
            // Arrange
            var target = new NormalItem { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 };

            // Act
            for (int i = 0; i < days; i++)
            {
                target.Update();
            }


            // Assert
            Assert.Equal("+5 Dexterity Vest", target.Name);
            Assert.Equal(0, target.Quality);
        }

        [Fact]
        public void Normal_Item_Should_Decrease_Quality_By_Two_When_SellingDate_Passed()
        {
            // Arrange
            var days = 8;
            var target = new NormalItem { Name = "+5 Dexterity Vest", SellIn = 5, Quality = 20 };           

            // Act
            for (int i = 0; i < days; i++)
            {
                target.Update();
            }


            // Assert
            Assert.Equal("+5 Dexterity Vest", target.Name);

            //Only passed 8 days but 3 passed selling date, => 20 -5 -(3*2) = 9
            Assert.Equal(9, target.Quality);
        }
    }
}
