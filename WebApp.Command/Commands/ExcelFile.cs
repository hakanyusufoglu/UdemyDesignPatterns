using ClosedXML.Excel;
using System.Data;

namespace WebApp.Command.Commands
{
    //Diagramda receiver'a karşılık gelmektedir.
    public class ExcelFile<T>
    {
        public readonly List<T> _list;
        //dosyanın tipi ve ismini tutalım ki kaydedelim
        public string FileName => $"{typeof(T).Name}.xlsx";
        public string FileType => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public ExcelFile(List<T> list)
        {
            _list = list;
        }

        public MemoryStream Create()
        {
            var wb = new XLWorkbook();

            var ds = new DataSet(); //ds = bir veri tabanı gibi düşün

            ds.Tables.Add(GetTable());

            wb.Worksheets.Add(ds);

            var excelMemory = new MemoryStream();
            wb.SaveAs(excelMemory); //excel dosyaam artık memoryde

            return excelMemory;
        }


        private DataTable GetTable()
        {
            var table = new DataTable();

            var type = typeof(T);
            //reflaction yaparak içini dolaşıyoruz ve sütun ekleniyor
            type.GetProperties().ToList().ForEach(x => table.Columns.Add(x.Name, x.PropertyType));

            _list.ForEach(x =>
            {
                //tek bir propertynin tüm değerlerini dizi olarak aldım.
                var values = type.GetProperties().Select(propertyInfo => propertyInfo.GetValue(x, null)).ToArray();

                table.Rows.Add(values);
            });
            return table;
        }
    }
}
