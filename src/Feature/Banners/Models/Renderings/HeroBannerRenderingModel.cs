using Sitecore.Diagnostics;
using Sitecore.Mvc.Presentation;
using System;
using DeanObrien.Feature.Banners.Helpers;

namespace DeanObrien.Feature.Banners.Models.Renderings
{
    public class HeroBannerRenderingModel : RenderingModel
    {
        private IImageTaxonomy _imageTaxonomy = null;
        private ILinkTaxonomy _linkTaxonomy = null;

        public string BannerStyleString { get; set; }
        public string Button1Label { get; set; }
        public string Button1Url { get; set; }
        public string Button1Target { get; set; }
        public string Button2Label { get; set; }
        public string Button2Url { get; set; }
        public string Button2Target { get; set; }
        public string Button3Label { get; set; }
        public string Button3Url { get; set; }
        public string Button3Target { get; set; }
        public bool HasButtons { get; set; }
        public HeroBannerRenderingModel(IImageTaxonomy imageTaxonomy, ILinkTaxonomy linkTaxonomy)
        {
            Assert.ArgumentNotNull(imageTaxonomy, "imageTaxonomy");
            Assert.ArgumentNotNull(linkTaxonomy, "linkTaxonomy");

            _imageTaxonomy = imageTaxonomy;
            _linkTaxonomy = linkTaxonomy;
        }


        public override void Initialize(Rendering rendering)
        {
            base.Initialize(rendering);

            string backgroundImageUrl = _imageTaxonomy.GetImageUrl(Item, "Hero Banner Image");
            this.BannerStyleString = String.IsNullOrWhiteSpace(backgroundImageUrl) ? String.Empty : String.Format("style=background-image:url({0})", backgroundImageUrl);
            this.Button1Url = _linkTaxonomy.GetLinkUrl(Item, "Hero Button 1 Link");
            this.Button1Target = _linkTaxonomy.GetLinkTarget(Item, "Hero Button 1 Link");
            this.Button1Label = _linkTaxonomy.GetLinkLabel(Item, "Hero Button 1 Label");
            this.Button2Url = _linkTaxonomy.GetLinkUrl(Item, "Hero Button 2 Link");
            this.Button2Target = _linkTaxonomy.GetLinkTarget(Item, "Hero Button 2 Link");
            this.Button2Label = _linkTaxonomy.GetLinkLabel(Item, "Hero Button 2 Label");
            this.Button3Url = _linkTaxonomy.GetLinkUrl(Item, "Hero Button 3 Link");
            this.Button3Target = _linkTaxonomy.GetLinkTarget(Item, "Hero Button 3 Link");
            this.Button3Label = _linkTaxonomy.GetLinkLabel(Item, "Hero Button 3 Label");

            HasButtons = false;
            if ((!string.IsNullOrWhiteSpace(Button1Url) && !string.IsNullOrWhiteSpace(Button1Label)) ||
               (!string.IsNullOrWhiteSpace(Button1Url) && !string.IsNullOrWhiteSpace(Button1Label)) ||
               (!string.IsNullOrWhiteSpace(Button1Url) && !string.IsNullOrWhiteSpace(Button1Label)))
            {
                HasButtons = true;
            }

        }
    }
}
