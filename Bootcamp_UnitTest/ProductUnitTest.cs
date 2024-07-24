using FluentAssertions;

namespace Bootcamp_UnitTest
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }

        public Product(string name, double price, string category)
        {
            ValidateName(name);
            ValidatePrice(price);
            ValidateCategory(category);

            Name = name;
            Price = price;
            Category = category;
        }

        public void ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Product must have a name.");
        }
        public void ValidatePrice(double price)
        {
            if (price < 0) throw new ArgumentException("Product price cannot be less than zero.");
        }
        public void ValidateCategory(string category)
        {
            if (category != "pets" && category != "eletronics" && category != "books")
                throw new ArgumentException("Insert a valid category.");
        }
    }
    public class ProductUnitTest
    {
        [Fact]
        public void WhenName_IsEmptyOrNull_ShouldReturnArgumentException()
        {
            Action product = () => new Product("", 10, "pets");

            product.Should().Throw<ArgumentException>()
                .WithMessage("Product must have a name.");
        }

        [Fact]
        public void WhenPrice_IsLessThanZero_ShouldReturnArgumentException()
        {
            Action product = () => new Product("Product", -10, "pets");

            product.Should().Throw<ArgumentException>()
                .WithMessage("Product price cannot be less than zero.");
        }

        [Fact]
        public void WhenCategory_IsNotValid_ShouldReturnArgumentException()
        {
            Action product = () => new Product("Product", 10, "invalid");

            product.Should().Throw<ArgumentException>()
                .WithMessage("Insert a valid category.");
        }
    }
}