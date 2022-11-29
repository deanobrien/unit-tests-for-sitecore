using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sitecore.Mvc.Presentation;
using System;
using Sitecore.Data;
using Sitecore.Data.Items;
using DeanObrien.Feature.Banners.Helpers;
using DeanObrien.Feature.Banners.Models.Renderings;
using DeanOBrien.Feature.Banners.MSTests;

namespace DeanObrien.Feature.Banners.MSTests
{
    [TestClass]
    public class BannerTests : TestsBase
    {
        [TestMethod]
        public void Ctor_LinkTaxonomyIsNull_ThrowsException()
        {
            var imageTaxonomy = new Mock<IImageTaxonomy>();
            Action sutAction = () => new HeroBannerRenderingModel(imageTaxonomy.Object, null);

            // act, assert
            var ex = Assert.ThrowsException<ArgumentNullException>(sutAction);
            Assert.AreEqual("linkTaxonomy", ex.ParamName);
        }

        [TestMethod]
        public void Ctor_ImageTaxonomyIsNull_ThrowsException()
        {
            // arrange
            var linkTaxonomy = new Mock<ILinkTaxonomy>(); 
            Action sutAction = () => new HeroBannerRenderingModel(null, linkTaxonomy.Object);

            // act, assert
            var ex = Assert.ThrowsException<ArgumentNullException>(sutAction);
            Assert.AreEqual("imageTaxonomy", ex.ParamName);
        }
        [TestMethod]
        public void Initialize_BannerStyleStringIsEmpty()
        {
            // arrange
            var rendering = new Rendering();
            rendering.Item = CreateItem(); // cleaner use of CreateItem() 
            var imageTaxonomy = new Mock<IImageTaxonomy>();
            imageTaxonomy.Setup(_ => _.GetImageUrl(It.IsAny<Item>(), "Hero Banner Image", null)).Returns(String.Empty);

            var linkTaxonomy = new Mock<ILinkTaxonomy>();

            var sut = new HeroBannerRenderingModel(imageTaxonomy.Object, linkTaxonomy.Object);

            // act
            sut.Initialize(rendering);

            // assert
            Assert.AreEqual(String.Empty,sut.BannerStyleString);
        }
        [TestMethod]
        public void Initialize_SetsBannerStyleString()
        {
            // arrange
            var rendering = new Rendering();
            rendering.Item = CreateItem();

            var imageTaxonomy = new Mock<IImageTaxonomy>();
            imageTaxonomy.Setup(_ => _.GetImageUrl(It.IsAny<Item>(), "Hero Banner Image", null)).Returns("/a/path/to/an/image.jpg");

            var linkTaxonomy = new Mock<ILinkTaxonomy>();
            linkTaxonomy.Setup(_ => _.GetLinkUrl(It.IsAny<Item>(), It.IsAny<String>())).Returns("/a/path/to/an/item");
            linkTaxonomy.Setup(_ => _.GetLinkTarget(It.IsAny<Item>(), It.IsAny<String>())).Returns("_self");
            linkTaxonomy.Setup(_ => _.GetLinkLabel(It.IsAny<Item>(), It.IsAny<String>())).Returns("Click here");

            var sut = new HeroBannerRenderingModel(imageTaxonomy.Object, linkTaxonomy.Object);

            // act
            sut.Initialize(rendering);

            // assert
            Assert.AreEqual("style=background-image:url(/a/path/to/an/image.jpg)", sut.BannerStyleString);
        }
        [TestMethod]
        public void Initialize_SetsButtons()
        {
            // arrange
            var database = new Mock<Database>();
            var item = CreateItem(database.Object);

            var rendering = new Rendering();
            rendering.Item = item.Object;

            var imageTaxonomy = new Mock<IImageTaxonomy>();
            imageTaxonomy.Setup(_ => _.GetImageUrl(It.IsAny<Item>(), "Hero Banner Image", null)).Returns("/a/path/to/an/image.jpg");

            var linkTaxonomy = new Mock<ILinkTaxonomy>();
            linkTaxonomy.Setup(_ => _.GetLinkUrl(It.IsAny<Item>(), It.IsAny<String>())).Returns("/a/path/to/an/item");
            linkTaxonomy.Setup(_ => _.GetLinkTarget(It.IsAny<Item>(), It.IsAny<String>())).Returns("_self");
            linkTaxonomy.Setup(_ => _.GetLinkLabel(It.IsAny<Item>(), It.IsAny<String>())).Returns("Click here");

            var sut = new HeroBannerRenderingModel(imageTaxonomy.Object, linkTaxonomy.Object);

            // act
            sut.Initialize(rendering);

            // assert
            Assert.AreEqual("/a/path/to/an/item", sut.Button1Url);
            Assert.AreEqual("_self", sut.Button1Target);
            Assert.AreEqual("Click here", sut.Button1Label);

        }

    }
}
