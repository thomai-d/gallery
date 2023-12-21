using FluentAssertions;
using Gallery.Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.Server.Tests.Domain
{
    public class ImageTests
    {
        [Theory]
        [InlineData("image.png")]
        [InlineData("image.jpg")]
        [InlineData("image.jpeg")]
        [InlineData("image-123.jpeg")]
        public void Valid_Images_Should_Be_Recognized_Valid(string fileName)
        {
            Image.IsValidImage(fileName).Should().BeTrue();
        }
        
        [Theory]
        [InlineData("image.txt")]
        [InlineData("image")]
        [InlineData("image.png.exe")]
        public void Valid_Images_Should_Be_Recognized_Invalid(string fileName)
        {
            Image.IsValidImage(fileName).Should().BeFalse();
        }

        [Theory]
        [InlineData("image.png", "image/png")]
        [InlineData(".png", "image/png")]
        [InlineData(".jpg", "image/jpeg")]
        [InlineData("image.jpg", "image/jpeg")]
        [InlineData(".jpeg", "image/jpeg")]
        [InlineData("image.jpeg", "image/jpeg")]
        public void GetMimeTypes_Should_Be_Valid_MimeTypes(string input, string expectation)
        {
            Image.GetMimeType(input)
                .Should().Be(expectation);
        }
        
        [Theory]
        [InlineData(".notexisting")]
        public void GetMimeTypes_Should_Throw_For_Invalid_MimeTypes(string input)
        {
            Action test = () => Image.GetMimeType(input);
            test.Should().Throw<ArgumentException>();
        }
    }
}
