using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using Sitecore.Data;
using Sitecore.Data.Items;
using Xunit;
using Sitecore.Abstractions;
using Sitecore.Globalization;
using DeanObrien.Feature.Banners.Helpers;

namespace DeanOBrien.Feature.Banners.XUnitTests
{
[TestClass]
public class ImageTaxonomyTests : TestsBase
{
    [Fact]
    public void Ctor_MediaManagerIsNull_Throws()
    {
        // arrange
        Action sutAction = () => new ImageTaxonomy(null);

        // act, assert
        var ex = Xunit.Assert.Throws<ArgumentNullException>(sutAction);
        Xunit.Assert.Equal("mediaManager", ex.ParamName);
    }

    [Fact]
    public void GetImageUrl_ItemIsNull_ReturnsEmpty()
    {
        // arrange
        var mediaManager = Substitute.For<BaseMediaManager>();
        var imageUrl = "~/Test-Image";
        mediaManager.GetMediaUrl(Arg.Any<MediaItem>()).Returns(imageUrl);
        var sut = new ImageTaxonomy(mediaManager);

        // act
        var results = sut.GetImageUrl(null, "Some Field");

        // assert
        Xunit.Assert.Empty(results);
    }

    [Fact]
    public void GetImageUrl_FieldNameIsNull_ReturnsEmpty()
    {
        // arrange
        var database = Substitute.For<Database>();
        var mediaManager = Substitute.For<BaseMediaManager>();

        var item = CreateItem(database);

        var sut = new ImageTaxonomy(mediaManager);

        // act
        var results = sut.GetImageUrl(item, null);

        // assert
        Xunit.Assert.Empty(results);
    }

    [Fact]
    public void GetImageUrl_UnknownFieldName_ReturnsEmpty()
    {
        // arrange
        var database = Substitute.For<Database>();
        var mediaManager = Substitute.For<BaseMediaManager>();

        var item = CreateItem(database);

        var sut = new ImageTaxonomy(mediaManager);

        // act
        var results = sut.GetImageUrl(item, "Some Unknown Field");

        // assert
        Xunit.Assert.Empty(results);
    }

    [Fact]
    public void GetImageUrl_KnownFieldName_ReturnsValue()
    {
        // arrange
        var database = Substitute.For<Database>();

        var item = CreateItem(database);
        SetItemField(item, "Some Known Field", $"<image mediaid='{mediaItemIDString}' />");

        var mediaItem = CreateMediaItem(database);
        database.GetItem(mediaItemID, Arg.Any<Language>(), Sitecore.Data.Version.Latest).Returns(mediaItem);

        var mediaManager = Substitute.For<BaseMediaManager>();
        mediaManager.GetMediaUrl(Arg.Is<MediaItem>(mi => mi.ID == mediaItem.ID)).Returns("/a/path/to/an/image.jpg");

        var sut = new ImageTaxonomy(mediaManager);

        // act
        var results = sut.GetImageUrl(item, "Some Known Field");

        // assert
        Xunit.Assert.Equal("/a/path/to/an/image.jpg", results);
    }
}
}
