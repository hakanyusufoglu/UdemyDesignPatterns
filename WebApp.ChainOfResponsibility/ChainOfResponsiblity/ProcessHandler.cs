namespace WebApp.ChainOfResponsibility.ChainOfResponsiblity
{
    public abstract class ProcessHandler : IProcessHandler
    {
        protected IProcessHandler nextProcessHandler;

        //işi gerçekleştiren sınıf, virtual olarak işaretlememin sebebi bir sonraki halkada ezilsin
        public virtual object Handle(object o)
        {
            if(nextProcessHandler!=null)
            {
                return nextProcessHandler.Handle(o); //zincirdeki halkalar yanyana eklenmiş olacak.
            }
            return null;
        }

        public IProcessHandler SetNext(IProcessHandler processHandler)
        {
            nextProcessHandler=processHandler;
            return processHandler;
        }
    }
}
