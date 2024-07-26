using FluentAssertions;
using static Bootcamp_UnitTest.Product;

namespace Bootcamp_UnitTest
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public double Price { get; set; }
        public Categories Category { get; set; }

        public Product(string name, double price, Categories category)
        {
            ValidateName(name);
            ValidatePrice(price);
            ValidateCategory(category);

            Name = name;
            Price = price;
            Category = category;
        }
        public enum Categories
        {
            Eletronics,
            Books,
            Pets
        }
        public void ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Product must have a name.");
            if (name.Length > 50)
                throw new ArgumentException("Product name cannot be longer than 30 characters.");
        }
        public void ValidatePrice(double price)
        {
            if (price <= 0) throw new ArgumentException("Product price cannot be less than zero.");
        }
        public void ValidateCategory(Categories category)
        {
            if (!Enum.IsDefined(typeof(Categories), category))
                throw new ArgumentException("Insert a valid category.");
        }
    }
    public class ProductUnitTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void WhenName_IsEmptyOrNull_ShouldReturnArgumentException(string invalidName)
        {
            var expectedProduct = new
            {
                Name = "Computador",
                Price = 10,
                Category = Categories.Eletronics
            };

            Assert.Throws<ArgumentException>(() =>
                new Product(invalidName, expectedProduct.Price, expectedProduct.Category));
        }

        [Fact]
        public void WhenName_IsLongerThan50_ShouldReturnArgumentException()
        {
            string name = new string('A', 51);

            Action product = () => new Product(name, 10, Categories.Pets);

            product.Should().Throw<ArgumentException>()
                .WithMessage("Product name cannot be longer than 30 characters.");
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(0)]
        public void WhenPrice_IsLessThanOrEqualZero_ShouldReturnArgumentException(double invalidPrice)
        {
            var expectedProduct = new
            {
                Name = "Computador",
                Price = 10,
                Category = Categories.Eletronics
            };

            Assert.Throws<ArgumentException>(() =>
            new Product(expectedProduct.Name, invalidPrice, expectedProduct.Category));
        }

        [Theory]
        [InlineData((Categories)999)]
        public void WhenCategory_IsNotValid_ShouldReturnArgumentException(Categories category)
        {
            Action product = () => new Product("Product", 10, category);

            product.Should().Throw<ArgumentException>()
                .WithMessage("Insert a valid category.");
        }
    }
}