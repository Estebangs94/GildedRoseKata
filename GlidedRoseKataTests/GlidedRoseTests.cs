using ConsoleApp;
using ConsoleApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GlidedRoseKataTests
{
    public class GlidedRoseTests
    {
        private const int _normalQualityDecrease = 1;

        #region Methods

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Normal_Item_Should_Decrease_Quality_By_One(int days)
        {
            // Arrange
            IList<ItemCollaborator> items = new List<ItemCollaborator> {
                new NormalItem { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 }
            };

            GlidedRose target = CreateGlidedRose(items);

            // Act
            for (int i = 0; i < days; i++) 
            {
                target.UpdateQuality();
            }


            // Assert
            Assert.Equal("+5 Dexterity Vest", items[0].Name);
            Assert.Equal(20 - (_normalQualityDecrease * days), items[0].Quality);
        }

        [Theory]
        [InlineData(30)]
        [InlineData(60)]
        [InlineData(90)]
        public void Normal_Item_Should_Decrease_Quality_Up_To_Zero(int days)
        {
            // Arrange
            IList<ItemCollaborator> items = new List<ItemCollaborator> {
                new NormalItem { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 }
            };

            GlidedRose target = CreateGlidedRose(items);

            // Act
            for (int i = 0; i < days; i++)
            {
                target.UpdateQuality();
            }


            // Assert
            Assert.Equal("+5 Dexterity Vest", items[0].Name);
            Assert.Equal(0, items[0].Quality);
        }

        [Fact]
        public void Normal_Item_Should_Decrease_Quality_By_Two_When_SellingDate_Passed()
        {
            // Arrange
            var days = 8;
            IList<ItemCollaborator> items = new List<ItemCollaborator> {
                new NormalItem { Name = "+5 Dexterity Vest", SellIn = 5, Quality = 20 }
            };

            GlidedRose target = CreateGlidedRose(items);

            // Act
            for (int i = 0; i < days; i++)
            {
                target.UpdateQuality();
            }


            // Assert
            Assert.Equal("+5 Dexterity Vest", items[0].Name);

            //Only passed 8 days but 3 passed selling date, => 20 -5 -(3*2) = 9
            Assert.Equal(9, items[0].Quality);
        }

        [Fact]
        public void Legendary_Item_Should_Not_Decrease_Quality()
        {
            // Arrange
            var days = 10;
            IList<ItemCollaborator> items = new List<ItemCollaborator> {

                new ItemCollaborator {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                new ItemCollaborator {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80}
            };

            GlidedRose target = CreateGlidedRose(items);

            // Act
            for (int i = 0; i < days; i++)
            {
                target.UpdateQuality();
            }


            // Assert
            Assert.Equal("Sulfuras, Hand of Ragnaros", items[0].Name);
            Assert.Equal("Sulfuras, Hand of Ragnaros", items[1].Name);

            Assert.Equal(80, items[0].Quality);
            Assert.Equal(80, items[1].Quality);
        }

        [Fact]
        public void Legendary_Item_Should_Not_Decrease_SellingIn()
        {
            // Arrange
            var days = 10;
            IList<ItemCollaborator> items = new List<ItemCollaborator> {

                new ItemCollaborator {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                new ItemCollaborator {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80}
            };

            GlidedRose target = CreateGlidedRose(items);

            // Act
            for (int i = 0; i < days; i++)
            {
                target.UpdateQuality();
            }


            // Assert
            Assert.Equal("Sulfuras, Hand of Ragnaros", items[0].Name);
            Assert.Equal("Sulfuras, Hand of Ragnaros", items[1].Name);

            Assert.Equal(0, items[0].SellIn);
            Assert.Equal(-1, items[1].SellIn);
        }

        [Fact]
        public void AgedBrie_Should_Increase_Quality_By_One()
        {
            // Arrange
            var days = 10;
            IList<ItemCollaborator> items = new List<ItemCollaborator> {
                new ItemCollaborator {Name = "Aged Brie", SellIn = 20, Quality = 0},
            };

            GlidedRose target = CreateGlidedRose(items);

            // Act
            for (int i = 0; i < days; i++)
            {
                target.UpdateQuality();
            }


            // Assert
            Assert.Equal("Aged Brie", items[0].Name);

            Assert.Equal(10, items[0].Quality);
        }

        [Fact]
        public void AgedBrie_Should_Increase_Quality_By_Two_When_SellingDate_Passed()
        {
            // Arrange
            var days = 10;
            IList<ItemCollaborator> items = new List<ItemCollaborator> {
                new ItemCollaborator {Name = "Aged Brie", SellIn = 2, Quality = 0},
            };

            GlidedRose target = CreateGlidedRose(items);

            // Act
            for (int i = 0; i < days; i++)
            {
                target.UpdateQuality();
            }


            // Assert
            Assert.Equal("Aged Brie", items[0].Name);

            Assert.Equal(18, items[0].Quality);
        }

        [Fact]
        public void AgedBrie_Should_Increase_Quality_Up_To_Fifty()
        {
            // Arrange
            var days = 70;
            IList<ItemCollaborator> items = new List<ItemCollaborator> {
                new ItemCollaborator {Name = "Aged Brie", SellIn = 2, Quality = 0},
            };

            GlidedRose target = CreateGlidedRose(items);

            // Act
            for (int i = 0; i < days; i++)
            {
                target.UpdateQuality();
            }


            // Assert
            Assert.Equal("Aged Brie", items[0].Name);

            Assert.Equal(50, items[0].Quality);
        }

        [Fact]
        public void BackstagePasses_Should_Increase_Quality_By_One()
        {
            // Arrange
            var days = 5;
            IList<ItemCollaborator> items = new List<ItemCollaborator> {
                new ItemCollaborator
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 30,
                    Quality = 20
                },
            };

            GlidedRose target = CreateGlidedRose(items);

            // Act
            for (int i = 0; i < days; i++)
            {
                target.UpdateQuality();
            }


            // Assert
            Assert.Equal("Backstage passes to a TAFKAL80ETC concert", items[0].Name);

            Assert.Equal(25, items[0].Quality);
        }

        [Fact]
        public void BackstagePasses_Should_Increase_Quality_By_Two_When_SellIn_Between_Ten_And_Five_Days()
        {
            // Arrange
            var days = 5;
            IList<ItemCollaborator> items = new List<ItemCollaborator> {
                new ItemCollaborator
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 12,
                    Quality = 20
                },
            };

            GlidedRose target = CreateGlidedRose(items);

            // Act
            for (int i = 0; i < days; i++)
            {
                target.UpdateQuality();
            }


            // Assert
            Assert.Equal("Backstage passes to a TAFKAL80ETC concert", items[0].Name);

            Assert.Equal(28, items[0].Quality);
        }

        [Fact]
        public void BackstagePasses_Should_Increase_Quality_By_Three_When_SellIn_Below_Five_Days()
        {
            // Arrange
            var days = 5;
            IList<ItemCollaborator> items = new List<ItemCollaborator> {
                new ItemCollaborator
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 30,
                    Quality = 20
                },
            };

            GlidedRose target = CreateGlidedRose(items);

            // Act
            for (int i = 0; i < days; i++)
            {
                target.UpdateQuality();
            }


            // Assert
            Assert.Equal("Backstage passes to a TAFKAL80ETC concert", items[0].Name);

            Assert.Equal(25, items[0].Quality);
        }

        [Fact]
        public void BackstagePasses_Should_Have_Zero_Quality_When_SellIn_Passed()
        {
            // Arrange
            var days = 5;
            IList<ItemCollaborator> items = new List<ItemCollaborator> {
                new ItemCollaborator
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 4,
                    Quality = 100
                },
            };

            GlidedRose target = CreateGlidedRose(items);

            // Act
            for (int i = 0; i < days; i++)
            {
                target.UpdateQuality();
            }


            // Assert
            Assert.Equal("Backstage passes to a TAFKAL80ETC concert", items[0].Name);

            Assert.Equal(0, items[0].Quality);
        }

        private static GlidedRose CreateGlidedRose(IList<ItemCollaborator> items)
            => new GlidedRose(items);
        

        #endregion
    }
}
