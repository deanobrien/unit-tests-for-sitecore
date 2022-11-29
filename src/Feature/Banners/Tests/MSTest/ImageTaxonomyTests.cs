using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sitecore.Mvc.Presentation;
using System;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Abstractions;
using Sitecore.Globalization;
using Sitecore.Collections;
using Sitecore.Data.Fields;
using DeanObrien.Feature.Banners.Helpers;
using DeanOBrien.Feature.Banners.MSTests;

namespace DeanObrien.Feature.Banners.MSTests
{
    [TestClass]
    public class ImageTaxonomyTests : TestsBase
    {
        [TestMethod]
        public void Ctor_MediaManagerIsNull_Throws()
        {
            // arrange
            Action sutAction = () => new ImageTaxonomy(null);

            // act, assert
            var ex = Assert.ThrowsException<ArgumentNullException>(sutAction);
            Assert.AreEqual("mediaManager", ex.ParamName);
        }

        [TestMethod]
        public void GetImageUrl_ItemIsNull_ReturnsEmpty()
        {
            // arrange
            var mediaManager = new Mock<BaseMediaManager>();
            var imageUrl = "~/Test-Image";
            mediaManager.Setup(_ => _.GetMediaUrl(It.IsAny<MediaItem>())).Returns(imageUrl);

            var sut = new ImageTaxonomy(mediaManager.Object);

            // act
            var results = sut.GetImageUrl(null, "Some Field");

            // assert
            Assert.AreEqual(String.Empty, results);
        }

        [TestMethod]
        public void GetImageUrl_FieldNameIsNull_ReturnsEmpty()
        {
            // arrange
            var database = new Mock<Database>();
            var mediaManager = new Mock<BaseMediaManager>();

            var item = CreateItem(database.Object);

            var sut = new ImageTaxonomy(mediaManager.Object);

            // act
            var results = sut.GetImageUrl(item.Object, null);

            // assert
            Assert.AreEqual(String.Empty, results);
        }

        [TestMethod]
        public void GetImageUrl_UnknownFieldName_ReturnsEmpty()
        {
            // arrange
            var database = new Mock<Database>();
            var mediaManager = new Mock<BaseMediaManager>();

            var item = CreateItem(database.Object);

            var sut = new ImageTaxonomy(mediaManager.Object);

            // act
            var results = sut.GetImageUrl(item.Object, "Some Unknown Field");

            // assert
            Assert.AreEqual(String.Empty, results);
        }

        [TestMethod]
        public void GetImageUrl_KnownFieldName_ReturnsValue()
        {
            // arrange
            var database = new Mock<Database>();

            var item = CreateItem(database.Object);
            SetItemField(item, "Some Known Field", $"<image mediaid='{mediaItemIDString}' />");

            var mediaItem = CreateMediaItem(database.Object);
            database.Setup(_ => _.GetItem(mediaItemID, It.IsAny<Language>(), Sitecore.Data.Version.Latest)).Returns(mediaItem);

            var mediaManager = new Mock<BaseMediaManager>();
            mediaManager.Setup(_ => _.GetMediaUrl(It.Is<MediaItem>(mi => mi.ID == mediaItem.ID))).Returns("/a/path/to/an/image.jpg");

            var sut = new ImageTaxonomy(mediaManager.Object);

            // act
            var results = sut.GetImageUrl(item.Object, "Some Known Field");

            // assert
            Assert.AreEqual("/a/path/to/an/image.jpg", results);
        }
    }
}
