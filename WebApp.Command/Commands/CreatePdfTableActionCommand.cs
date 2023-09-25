using Microsoft.AspNetCore.Mvc;

namespace WebApp.Command.Commands
{
    //Diagramda ICommand'ın Concreat'ine karşılık gelmektedir.
    public class CreatePdfTableActionCommand<T> : ITableActionCommand
    {
        private readonly PdfFile<T> _pdfFile;

        public CreatePdfTableActionCommand(PdfFile<T> pdfFile)
        {
            _pdfFile = pdfFile;
        }

        public IActionResult Execute()
        {
            var excelMemorySteam = _pdfFile.Create();

            return new FileContentResult(excelMemorySteam.ToArray(), _pdfFile.FileType) { FileDownloadName = _pdfFile.FileName };
        }
    }
}
