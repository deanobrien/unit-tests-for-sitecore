using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeanObrien.Feature.Banners.Helpers;
using DeanObrien.Feature.Banners.Models.Renderings;
using Sitecore.Mvc.Presentation;

namespace DeanObrien.Feature.Banners.Controllers
{
    public class BannersController : Controller
    {
        private IImageTaxonomy _imageTaxonomy;
        private ILinkTaxonomy _linkTaxonomy;

        public BannersController(IImageTaxonomy imageTaxonomy, ILinkTaxonomy linkTaxonomy)
        {
            _imageTaxonomy = imageTaxonomy;
            _linkTaxonomy = linkTaxonomy;
        }
        public ActionResult HeroBanner()
        {
            var model = new HeroBannerRenderingModel(_imageTaxonomy, _linkTaxonomy);
            model.Initialize(RenderingContext.Current.Rendering);
            return View(model);
        }
    }
}
