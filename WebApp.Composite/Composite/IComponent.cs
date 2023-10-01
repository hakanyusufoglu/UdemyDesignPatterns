namespace WebApp.Composite.Composite
{
    public interface IComponent
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Bu componenti implemente eden değer hep artacak
        int Count();
        string Display();
    }
}
