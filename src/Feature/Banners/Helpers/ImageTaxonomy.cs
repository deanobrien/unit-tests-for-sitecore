using System;
using Sitecore.Abstractions;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links.UrlBuilders;


namespace DeanObrien.Feature.Banners.Helpers
{
    public class ImageTaxonomy : IImageTaxonomy
    {
        private BaseMediaManager _mediaManager;

        public ImageTaxonomy(BaseMediaManager mediaManager)
        {
            Assert.ArgumentNotNull(mediaManager, "mediaManager");
            _mediaManager = mediaManager;
        }
        public virtual string GetImageUrl(Item item, string fieldName, MediaUrlBuilderOptions options = null)
        {
            if (item == null)
                return String.Empty;

            if (string.IsNullOrWhiteSpace(fieldName))
                return String.Empty;

            var imgField = (ImageField)item.Fields[fieldName];

            if (imgField != null && imgField.MediaItem != null)
            {
                return (options != null) ?
                  _mediaManager.GetMediaUrl(imgField.MediaItem, options) :
                  _mediaManager.GetMediaUrl(imgField.MediaItem);
            }

            return String.Empty;
        }
    }
}