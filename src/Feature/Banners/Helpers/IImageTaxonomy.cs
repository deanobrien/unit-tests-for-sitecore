using Sitecore.Links.UrlBuilders;
using Sitecore.Data.Items;

namespace DeanObrien.Feature.Banners.Helpers
{
    public interface IImageTaxonomy
    {
        string GetImageUrl(Item item, string fieldName, MediaUrlBuilderOptions options = null);
    }
}
