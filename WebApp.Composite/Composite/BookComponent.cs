namespace WebApp.Composite.Composite
{
    public class BookComponent : IComponent
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public BookComponent(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Count()
        {
            //Bu değerin altında kategori olamaz bu yüzden değeri 1'dir.
            return 1;
        }

        public string Display()
        {
            return $"<li class='list-group-item'>{Name}</li>";
        }
    }
}
