using Microsoft.AspNetCore.Mvc;

namespace WebApp.Command.Commands
{
    //Diagramda ICommand'ın Concreat'ine karşılık gelmektedir.
    public class CreateExcelTableActionCommand<T> : ITableActionCommand
    {
        //ExcelFile'ı bir interface yaparak implement edebilirdim ki yumuşak bağlılık sağlansın.
        private readonly ExcelFile<T> _excelFile;

        public CreateExcelTableActionCommand(ExcelFile<T> excelFile)
        {
            _excelFile = excelFile;
        }

        public IActionResult Execute()
        {
            var excelMemorySteam = _excelFile.Create();

            return new FileContentResult(excelMemorySteam.ToArray(), _excelFile.FileType) { FileDownloadName=_excelFile.FileName};
        }
    }
}
