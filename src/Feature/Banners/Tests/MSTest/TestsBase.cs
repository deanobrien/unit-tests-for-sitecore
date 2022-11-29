using Moq;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeanOBrien.Feature.Banners.MSTests
{
    public class TestsBase
    {
        protected string mediaItemIDString = "12345678-1234-1234-1234-123456789012";
        protected ID mediaItemID = new ID("12345678-1234-1234-1234-123456789012");
        protected Item CreateItem()
        {
            // this results in cleaner code
            var database = new Mock<Database>();
            return new Mock<Item>(ID.NewID, ItemData.Empty, database.Object).Object;
        }
        protected Mock<Item> CreateItem(Database database = null)
        {
            var item = new Mock<Item>(ID.NewID, ItemData.Empty, database);

            var fields = new Mock<FieldCollection>(item.Object);
            item.SetupGet(x => x.Fields).Returns(fields.Object);
            return item;
        }
        protected Item CreateMediaItem(Database database = null)
        {
            var definition = new ItemDefinition(mediaItemID, "Mock Media Item", ID.Null, ID.Null);
            var data = new ItemData(definition, Language.Current, Sitecore.Data.Version.First, new FieldList());
            var mediaItem = new Item(mediaItemID, data, database);
            return mediaItem;
        }
        protected void SetItemField(Mock<Item> item, string fieldName, string fieldValue)
        {
            var field = new Mock<Field>(ID.NewID, item.Object);
            field.SetupGet(x => x.Value).Returns(fieldValue);
            field.SetupGet(x => x.Database).Returns(item.Object.Database);
            item.SetupGet(x => x.Fields[fieldName]).Returns(field.Object);

        }
    }
}
