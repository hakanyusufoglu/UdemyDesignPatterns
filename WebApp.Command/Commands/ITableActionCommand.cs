using Microsoft.AspNetCore.Mvc;

namespace WebApp.Command.Commands
{
    //Diagramda ICommand'a karşılık gelmektedir.
    public interface ITableActionCommand
    {
        IActionResult Execute();
    }
}
