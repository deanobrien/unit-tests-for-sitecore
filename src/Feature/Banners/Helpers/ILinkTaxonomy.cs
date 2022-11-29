using Sitecore.Data.Items;

namespace DeanObrien.Feature.Banners.Helpers
{
    public interface ILinkTaxonomy
    {
        string GetLinkUrl(Item item, string fieldName);
        string GetLinkTarget(Item item, string fieldName);
        string GetLinkLabel(Item item, string v);
    }
}
