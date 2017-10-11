using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.Wer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

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
        
        public LibraryController(
            FilesRepository filesRepository,
            ReportRepository reportRepository,
            IRazorViewEngine viewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider
            )
        {
            this._reportRepository = reportRepository;
            this._viewEngine = viewEngine;
            this._filesRepository = filesRepository;
            this._tempDataProvider = tempDataProvider;
            this._serviceProvider = serviceProvider;
        }
        [HttpGet("library/week/{idWeek}")]
        [RequiresRole(WerRoles.Ceo,WerRoles.Director,WerRoles.Operator)]
        public IActionResult GetLibrary([FromRoute]long idCompany, [FromRoute] long idWeek)
        {
            return new ObjectResult(
                this._filesRepository.GetLibrary(idCompany,idWeek)
            ) {StatusCode = 200};
        }

        [HttpGet("week/{idWeek}/pdf")]
        [RequiresRole(WerRoles.Ceo,WerRoles.Director,WerRoles.Operator)]
        public async Task<IActionResult> GetPdf([FromRoute(Name = "idCompany")] long idCompany, [FromRoute(Name = "idWeek")] long idWeek)
        {
            try
            {
                var reports = _reportRepository.GetReportRecursive(idCompany, idWeek);
                var data = await this.RenderViewToStringAsync("PDFTemplate", reports.ToArray());
                var pdf = this.GetPDF(data.Replace("\n",""));
                return File(pdf,"application/pdf");
            }
            catch (Exception e)
            {
                return new ObjectResult(new {error = e.Message}){StatusCode = 500};
            }
        }
        
        public byte[] GetPDF(string pHTML) {
            byte[] bPDF = null;
            MemoryStream ms = new MemoryStream();
            TextReader txtReader = new StringReader(pHTML);
            Document doc = new Document(PageSize.LETTER, 30, 30, 30, 30);
            PdfWriter oPdfWriter = PdfWriter.GetInstance(doc, ms);
            doc.Open();
            var xmlWorker = HTMLWorker.ParseToList(txtReader,new StyleSheet());
            foreach (var item in xmlWorker)
            {
                if (item.Chunks.Count(cChunks => cChunks.Content.Contains("Semana:")) > 0)
                {
                    doc.NewPage();
                }
                doc.Add(item);
            }
            
            doc.Close();
            bPDF = ms.ToArray();
            return bPDF;
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