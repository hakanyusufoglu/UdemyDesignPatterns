using System.Drawing;

namespace WebApp.Adapter.Services
{
    //Adapter işlemini gerçekleştiren sınıftır.
    public class AdvanceImageProcessAdapter : IImageProcess
    {
        private readonly IAdvanceImageProcess _advanceImageProcess;

        public AdvanceImageProcessAdapter(IAdvanceImageProcess advanceImageProcess)
        {
            _advanceImageProcess = advanceImageProcess;
        }

        public void AddWatermark(string text, string fileName, Stream imageStream)
        {
            //HomeControllerdaki _imageProcess.AddWatermark() metodunun içerisi artık aşağıdaki metodu çağıracaktır.
            _advanceImageProcess.AddWatermarkImage(imageStream, text, $"wwwroot/watermarks/{fileName}", Color.FromArgb(128, 255, 255, 255), Color.FromArgb(0, 255, 255, 255));
        }
    }
}
