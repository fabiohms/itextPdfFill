using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using itextPdfFill.Interface;
using itextPdfFill.Utils;
using System.Reflection;

namespace itextPdfFill.Services
{
    internal class TemplateFillService : ITemplateFillService
    {

        public void BulkFill(string pathTemplate, string pathDest, string pathData, List<string> fields) 
        {
            Type model = DynamicModelUtils.CreateDynamicType(fields);
           
            var data = ReadCsvDynamic(pathData, model);

            foreach (var row in data)
            {
                PropertyInfo[] properties = row.GetType().GetProperties();
                string pdfPathDest = String.Concat(pathDest, properties[0].GetValue(row).ToString(), new Random().Next(99), ".pdf");

                PdfDocument pdfDoc = new PdfDocument(new PdfReader(pathTemplate), new PdfWriter(pdfPathDest));
                PdfAcroForm pdfAcroForm = new PdfFormFactory().GetAcroForm(pdfDoc, false);

                FormUtils.setFields(ref pdfAcroForm, row);

                pdfAcroForm.FlattenFields();
                pdfDoc.Close();
            }
        }

        public void Fill(string pathTemplate, string pathDest, Dictionary<string, Object> fields)
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfReader(pathTemplate), new PdfWriter(pathDest));
            PdfAcroForm pdfAcroForm = new PdfFormFactory().GetAcroForm(pdfDoc, false);

            FormUtils.setFields(ref pdfAcroForm, fields);

            pdfAcroForm.FlattenFields();
            pdfDoc.Close();
        }

        public List<string> GetFieldsNames(string pathTemplate) 
        {
            var acroFormFields = FormUtils.getFields(pathTemplate);

            return acroFormFields.Keys.ToList();
        }

        private IEnumerable<object> ReadCsvDynamic(string filePath, Type type)
        {
            // Get the MethodInfo for the generic ReadCsv<T> method
            MethodInfo method = typeof(CsvUtils).GetMethod(nameof(CsvUtils.ReadCsv));

            // Make the method generic with the runtime type
            MethodInfo genericMethod = method.MakeGenericMethod(type);

            // Invoke the method with the runtime type
            var result = (IEnumerable<object>)genericMethod.Invoke(null, new object[] { filePath });

            return result;
        }
    }
}
