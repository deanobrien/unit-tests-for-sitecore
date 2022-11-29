
using Sitecore.Abstractions;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links.UrlBuilders;
using Sitecore.Resources.Media;

namespace DeanObrien.Feature.Banners.Helpers
{
    public class ImageTaxonomyWithMediaProtection : ImageTaxonomy, IImageTaxonomy
    {
        private BaseMediaManager _mediaManager;

        public ImageTaxonomyWithMediaProtection(BaseMediaManager mediaManager) : base(mediaManager)
        {
            Assert.ArgumentNotNull(mediaManager, "mediaManager");
            _mediaManager = mediaManager;
        }
        public override string GetImageUrl(Item item, string fieldName, MediaUrlBuilderOptions options = null)
        {
            return HashingUtils.ProtectAssetUrl(base.GetImageUrl(item, fieldName, options));
        }
    }
}