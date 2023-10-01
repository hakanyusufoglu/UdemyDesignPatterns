namespace WebApp.Composite.Models
{
    public class Category
    {
        // Id Name       UserId ReferenceId
        // 1  Kitaplar     1      0 (en üst kategori oldugu için 0)
        // 2 kitaplar1     1      1 (üstreki kitapları işaretliyor.)
        public int Id { get; set; }
        public string Name { get; set; }
        
        // Her bir kullanıcı kendi kategorisini ekleyeceği için userId ekledik. Genel bir sonsuz menü tasarlamak istiyorsak bu userId'ye gerek yok.
        public string UserId { get; set; }
        public ICollection<Book> Books { get; set; }

        public int ReferenceId { get; set; }

    }
}
