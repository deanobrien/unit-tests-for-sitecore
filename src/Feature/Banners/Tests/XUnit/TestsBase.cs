using Sitecore.Data;
using Sitecore.Data.Items;
using NSubstitute;
using Sitecore.Collections;
using Sitecore.Globalization;
using Sitecore.Data.Fields;

namespace DeanOBrien.Feature.Banners.XUnitTests
{
    public class TestsBase
    {
        protected string mediaItemIDString = "12345678-1234-1234-1234-123456789012";
        protected ID mediaItemID = new ID("12345678-1234-1234-1234-123456789012");

        protected Item CreateItem()
        {
            var database = Substitute.For<Database>();
            return Substitute.For<Item>(ID.NewID, ItemData.Empty, database);
        }
        protected Item CreateItem(Database database = null)
        {
            var item = Substitute.For<Item>(ID.NewID, ItemData.Empty, database);
            var fields = Substitute.For<FieldCollection>(item);
            item.Fields.Returns(fields);
            return item;
        }
        protected Item CreateMediaItem(Database database = null)
        {
            var definition = new ItemDefinition(mediaItemID, "Mock Media Item", ID.Null, ID.Null);
            var data = new ItemData(definition, Language.Current, Sitecore.Data.Version.First, new FieldList());
            var mediaItem = new Item(mediaItemID, data, database);
            return mediaItem;
        }
        protected void SetItemField(Item item, string fieldName, string fieldValue)
        {
            var field = Substitute.For<Field>(ID.NewID, item);
            field.Value = fieldValue;
            field.Database.Returns(item.Database);
            item.Fields[fieldName].Returns(field);
        }
    }
}
