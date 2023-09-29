namespace WebApp.ChainOfResponsibility.ChainOfResponsiblity
{
    public interface IProcessHandler
    {
        //Zincirdeki bir sonraki halkayı temsil etmektedir.
        IProcessHandler SetNext(IProcessHandler processHandler);

        //Asıl işlemi gerçekleştirecek olan metottur. Object olmasının sebebi ise istediğim datayı gönderebilmeyi sağlıyorum. İstediğim her şeyi bu zincire ekleyebilirim.
        Object Handle(Object o);
    }
}
