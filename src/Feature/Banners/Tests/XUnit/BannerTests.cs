using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Sitecore.Mvc.Presentation;
using System;
using Sitecore.Data.Items;
using Xunit;
using DeanObrien.Feature.Banners.Helpers;
using DeanObrien.Feature.Banners.Models.Renderings;

namespace DeanOBrien.Feature.Banners.XUnitTests
{
    [TestClass]
    public class BannerTests : TestsBase
    {
        [Fact]
        public void Ctor_LinkTaxonomyIsNull_ThrowsException()
        {
            // arrange
            var imageTaxonomy = Substitute.For<IImageTaxonomy>();
            Action sutAction = () => new HeroBannerRenderingModel(imageTaxonomy, null);

            // act, assert
            var ex = Xunit.Assert.Throws<ArgumentNullException>(sutAction);
            Xunit.Assert.Equal("linkTaxonomy", ex.ParamName);
        }
        [Fact]
        public void Ctor_ImageTaxonomyIsNull_ThrowsException()
        {
            // arrange
            var linkTaxonomy = Substitute.For<ILinkTaxonomy>();
            Action sutAction = () => new HeroBannerRenderingModel(null, linkTaxonomy);

            // act, assert
            var ex = Xunit.Assert.Throws<ArgumentNullException>(sutAction);
            Xunit.Assert.Equal("imageTaxonomy", ex.ParamName);
        }
        [Fact]
        public void Initialize_BannerStyleStringIsEmpty()
        {
            // arrange
            var rendering = new Rendering();
            rendering.Item = CreateItem();
            var imageTaxonomy = Substitute.For<IImageTaxonomy>();
            imageTaxonomy.GetImageUrl(Arg.Any<Item>(), "Hero Banner Image").Returns(String.Empty);

            var linkTaxonomy = Substitute.For<ILinkTaxonomy>();

            var sut = new HeroBannerRenderingModel(imageTaxonomy, linkTaxonomy);

            // act
            sut.Initialize(rendering);

            // assert
            Xunit.Assert.Empty(sut.BannerStyleString);
        }
        [Fact]
        public void Initialize_SetsBannerStyleString()
        {
            // arrange
            var rendering = new Rendering();
            rendering.Item = CreateItem();

            var imageTaxonomy = Substitute.For<IImageTaxonomy>();
            imageTaxonomy.GetImageUrl(Arg.Any<Item>(), "Hero Banner Image").Returns("/a/path/to/an/image.jpg");

            var linkTaxonomy = Substitute.For<ILinkTaxonomy>();
            linkTaxonomy.GetLinkUrl(Arg.Any<Item>(), Arg.Any<String>()).Returns("/a/path/to/an/item");
            linkTaxonomy.GetLinkTarget(Arg.Any<Item>(), Arg.Any<String>()).Returns("_self");
            linkTaxonomy.GetLinkLabel(Arg.Any<Item>(), Arg.Any<String>()).Returns("Click here");

            var sut = new HeroBannerRenderingModel(imageTaxonomy, linkTaxonomy);

            // act
            sut.Initialize(rendering);

            // assert
            Xunit.Assert.Equal("style=background-image:url(/a/path/to/an/image.jpg)", sut.BannerStyleString);
        }
        [Fact]
        public void Initialize_SetsButtons()
        {
            // arrange
            var rendering = new Rendering();
            rendering.Item = CreateItem();

            var imageTaxonomy = Substitute.For<IImageTaxonomy>();
            imageTaxonomy.GetImageUrl(Arg.Any<Item>(), "Hero Banner Image").Returns("/a/path/to/an/image.jpg");

            var linkTaxonomy = Substitute.For<ILinkTaxonomy>();
            linkTaxonomy.GetLinkUrl(Arg.Any<Item>(), Arg.Any<String>()).Returns("/a/path/to/an/item");
            linkTaxonomy.GetLinkTarget(Arg.Any<Item>(), Arg.Any<String>()).Returns("_self");
            linkTaxonomy.GetLinkLabel(Arg.Any<Item>(), Arg.Any<String>()).Returns("Click here");

            var sut = new HeroBannerRenderingModel(imageTaxonomy, linkTaxonomy);

            // act
            sut.Initialize(rendering);

            // assert
            Xunit.Assert.Equal("/a/path/to/an/item", sut.Button1Url);
            Xunit.Assert.Equal("_self", sut.Button1Target);
            Xunit.Assert.Equal("Click here", sut.Button1Label);

        }
    }
}
