using Sitecore.Data.Items;
using Sitecore.Data.Fields;

namespace DeanObrien.Feature.Banners.Helpers
{
    public class LinkTaxonomy : ILinkTaxonomy
    {
        public string GetLinkUrl(Item item, string fieldName)
        {
            string linkUrl = null;

            if (item.Fields[fieldName] != null)
            {
                LinkField linkField = item.Fields[fieldName];
                linkUrl = linkField.GetFriendlyUrl();
            }

            return linkUrl;
        }

        public string GetLinkTarget(Item item, string fieldName)
        {
            string linkTarget = "_self";

            if (item.Fields[fieldName] != null)
            {
                LinkField linkField = item.Fields[fieldName];
                if (!string.IsNullOrWhiteSpace(linkField.Target)) linkTarget = linkField.Target;
            }

            return linkTarget;
        }

        public string GetLinkLabel(Item item, string fieldName)
        {
            string label = null;

            if (item.Fields[fieldName] != null)
            {
                Field labelField = item.Fields[fieldName];
                label = labelField.Value;
            }

            return label;
        }
    }
}