using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoTech.Bi.Authorization;
using CoTech.Bi.Modules.Wer.Models.Entities;
using CoTech.Bi.Modules.Wer.Repositories;
using CoTech.Bi.Util;
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
using iTextSharp;

namespace CoTech.Bi.Modules.Wer.Controllers
{
    [Route("/api/companies/{idCompany}/res")]
    [RequiresRole(WerRoles.Ceo,WerRoles.Director,WerRoles.Operator)]
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
        [HttpPost("library/week/{idWeek}")]
        [RequiresRole(WerRoles.Ceo,WerRoles.Director,WerRoles.Operator)]
        public async Task<IActionResult> PostLibrary([FromRoute]long idCompany, [FromRoute] long idWeek,
            [FromForm(Name = "file")] IFormFile formFile)
        {
            try
            {
                if (formFile != null)
                {
                    var directory = Directory.GetCurrentDirectory()+ "/storage/wer/";
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    var filePath = directory + Guid.NewGuid() + Path.GetExtension(formFile.FileName);
                    if (formFile.Length > 0)
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                    var file = _filesRepository.createFile(new FileCompanyEntity()
                    {
                        Mime = MimeReader.GetMimeType(Path.GetExtension(formFile.FileName)),
                        Name = formFile.FileName,
                        Uri = filePath,
                        Extension = Path.GetExtension(formFile.FileName),
                        WeekId = idWeek,
                        CompanyId = idCompany,
                        UserId = HttpContext.UserId().Value
                    });
                    if (file != null)
                    {
                        return new ObjectResult(file){StatusCode = 201};
                    }
                    else
                    {
                        System.IO.File.Delete(filePath);
                        return StatusCode(500);
                    } 
                }else 
                    return new BadRequestResult();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return StatusCode(500); // 500 is generic server error
            }
        }
        [HttpGet("library/week/{idWeek}")]
        [RequiresRole(WerRoles.Ceo,WerRoles.Director,WerRoles.Operator)]
        public async Task<IActionResult> GetLibrary([FromRoute]long idCompany, [FromRoute] long idWeek)
        {
            try
            {
                return new ObjectResult(
                     this._filesRepository.GetLibrary(idCompany,idWeek)
                ) {StatusCode = 200};
            }
            catch (Exception e)
            {
                return new ObjectResult(
                    new { message = e.Message}
                ) {StatusCode = 500};
            }
        }
        [HttpGet("library")]
        [RequiresRole(WerRoles.Ceo,WerRoles.Director,WerRoles.Operator)]
        public async Task<IActionResult> GetLibrary([FromRoute]long idCompany)
        {
            try
            {
                return new ObjectResult(
                    this._filesRepository.GetLibraryCompany(idCompany)
                ) {StatusCode = 200};
            }
            catch (Exception e)
            {
                return new ObjectResult(
                    new { message = e.Message}
                ) {StatusCode = 500};
            }
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
        
        private byte[] GetPDF(string pHTML) {
            byte[] bPDF = null;
            MemoryStream ms = new MemoryStream();
            TextReader txtReader = new StringReader(pHTML);
            Document doc = new Document(PageSize.Letter, 30, 30, 30, 30);
            PdfWriter oPdfWriter = PdfWriter.GetInstance(doc, ms);
            doc.Open();
            var xmlWorker = HtmlWorker.ParseToList(txtReader,new StyleSheet());
            foreach (Paragraph item in xmlWorker)
            {
                if (item.Content.Contains("Semana:"))
                {
                    doc.NewPage();
                }
                doc.Add(item);
            }
            
            doc.Close();
            bPDF = ms.ToArray();
            return bPDF;
        }
        
        private async Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model)
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