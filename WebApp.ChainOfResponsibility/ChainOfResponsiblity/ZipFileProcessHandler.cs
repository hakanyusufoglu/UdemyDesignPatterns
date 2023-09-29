using System.IO.Compression;

namespace WebApp.ChainOfResponsibility.ChainOfResponsiblity
{
    public class ZipFileProcessHandler <T>: ProcessHandler
    {
        //Burada excelMemorystream geliyor
        public override object Handle(object o)
        {
            var excelMemoryStream = o as MemoryStream;

            excelMemoryStream.Position = 0;

            using(var zipStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(zipStream,ZipArchiveMode.Create,true))
                {
                    var zipFile = archive.CreateEntry($"{typeof(T).Name}.xlsx");

                    using (var zipEntry = zipFile.Open())
                    {
                        excelMemoryStream.CopyTo(zipEntry);
                    }
                }
                //bir sonraki zincire rar dosyasını gönderiyoruz 
                return base.Handle(zipStream);
            }

        }
    }
}
