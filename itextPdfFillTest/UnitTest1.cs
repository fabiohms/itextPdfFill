using itextPdfFill.Utils;
using itextPdfFillTest.Models;
using itextPdfFill.Services;

namespace itextPdfFillTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "modelo_campos.pdf");

            Assert.True(File.Exists(path), $"Arquivo n�o encontrado: {path}");

            var list = new TemplateFillService().GetFieldsNames(path);

            Assert.NotEmpty(list);
            Assert.Contains("nome", list);
        }

        [Fact]
        public void Test2()
        {
            Dictionary<string, Object> fields = new Dictionary<string,Object>();

            fields.Add("nome", "F�bio");

            string pathTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "modelo_campos.pdf");

            Assert.True(File.Exists(pathTemplate), $"Arquivo n�o encontrado: {pathTemplate}");

            string pathDest = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Dest");

            new TemplateFillService().Fill(pathTemplate, pathDest, fields);
        }

        [Fact]
        public void Test3()
        {
            string pathData = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "membros.csv");
            Assert.True(File.Exists(pathData), $"Arquivo n�o encontrado: {pathData}");

            var records = CsvUtils.ReadCsv<Membro>(pathData);
        }

        [Fact]
        public void Test4() 
        {

            string pathTemplate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "modelo_campos.pdf");
            Assert.True(File.Exists(pathTemplate), $"Arquivo n�o encontrado: {pathTemplate}");

            string pathDest = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Dest");

            string pathData = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "membros.csv");
            Assert.True(File.Exists(pathData), $"Arquivo n�o encontrado: {pathData}");

            new TemplateFillService().BulkFill(pathTemplate, pathDest, pathData, new List<string>() { "nome", "evento", "dia", "mes" });
        }
    }
}