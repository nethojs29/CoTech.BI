using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Modules.Wer.Models.Responses;
using CoTech.Bi.Modules.Wer.Repositories;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace CoTech.Bi.Modules.Wer.Controllers
{
    [Route("/api/companies/{idCompany}/res")]
    public class LibraryController : Controller
    {
        private readonly FilesRepository _filesRepository;
        private readonly ReportRepository _reportRepository;
        private IRazorViewEngine _viewEngine;
        private ITempDataProvider _tempDataProvider;
        private IServiceProvider _serviceProvider;
        private IConverter _converter;
        
        public LibraryController(
            FilesRepository filesRepository,
            ReportRepository reportRepository,
            IRazorViewEngine viewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider,
            IConverter converter
            )
        {
            this._reportRepository = reportRepository;
            this._viewEngine = viewEngine;
            this._filesRepository = filesRepository;
            this._tempDataProvider = tempDataProvider;
            this._serviceProvider = serviceProvider;
            this._converter = converter;
        }
        [HttpGet("library")]
        public IActionResult GetLibrary([FromRoute]long idCompany)
        {
            return new ObjectResult(
                this._filesRepository.GetLibrary(idCompany)
            ) {StatusCode = 200};
        }

        [HttpGet("week/{idWeek}/pdf")]
        public async Task<IActionResult> GetPdf([FromRoute(Name = "idCompany")] long idCompany, [FromRoute(Name = "idWeek")] long idWeek)
        {
            var reports = _reportRepository.GetReportRecursive(idCompany, idWeek);
            var data = await this.RenderViewToStringAsync("PDFTemplate", reports.ToArray());
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.Letter,
                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = data,
                        WebSettings = { DefaultEncoding = "utf-8",MinimumFontSize = 16,enablePlugins = true,LoadImages = true},
                        HeaderSettings =
                        {
                            FontSize = 9,
                            Right = "Página [page] de [toPage]",
                            Left = DateTime.Now.ToString("dd/MM/yyyy"),
                            Line = true,
                            Spacing = 2.812
                        }
                    }
                }
            };
            byte[] pdf = this._converter.Convert(doc);
            return File(pdf, "application/pdf");
        }
        
        public async Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model)
        {
            var actionContext = GetActionContext();
            var view = FindView(actionContext, viewName);

            using (var output = new StringWriter())
            {
                var viewContext = new ViewContext(
                    actionContext,
                    view,
                    new ViewDataDictionary<TModel>(
                        metadataProvider: new EmptyModelMetadataProvider(),
                        modelState: new ModelStateDictionary())
                    {
                        Model = model
                    },
                    new TempDataDictionary(
                        actionContext.HttpContext,
                        _tempDataProvider),
                    output,
                    new HtmlHelperOptions());

                await view.RenderAsync(viewContext);

                return output.ToString();
            }
        }

        private IView FindView(ActionContext actionContext, string viewName)
        {
            var getViewResult = _viewEngine.GetView(executingFilePath: null, viewPath: viewName, isMainPage: true);
            if (getViewResult.Success)
            {
                return getViewResult.View;
            }

            var findViewResult = _viewEngine.FindView(actionContext, viewName, isMainPage: true);
            if (findViewResult.Success)
            {
                return findViewResult.View;
            }

            var searchedLocations = getViewResult.SearchedLocations.Concat(findViewResult.SearchedLocations);
            var errorMessage = string.Join(
                Environment.NewLine,
                new[] { $"Unable to find view '{viewName}'. The following locations were searched:" }.Concat(searchedLocations)); ;

            throw new InvalidOperationException(errorMessage);
        }

        private ActionContext GetActionContext()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.RequestServices = _serviceProvider;
            return new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        }
    }
}