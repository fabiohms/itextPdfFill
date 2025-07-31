using iText.Forms;
using iText.Forms.Fields;
using iText.IO.Source;
using iText.Kernel.Pdf;
using System.Reflection;

namespace itextPdfFill.Utils
{
    internal static class FormUtils
	{
		public static IDictionary<String, PdfFormField> getFields(byte[] src) 
		{
			PdfDocument pdfDoc = new PdfDocument(
					new PdfReader(new RandomAccessSourceFactory().CreateSource(src)
					, new ReaderProperties()));

            return getFormFields(pdfDoc);
		}

        public static IDictionary<String, PdfFormField> getFields(PdfDocument src)
        {
            return getFormFields(src);
        }

        public static IDictionary<String, PdfFormField> getFields(String docPath)
        {
            if (File.Exists(docPath))
            {
                var src = File.ReadAllBytes(docPath);
                return getFields(src);
            }
            else
                throw new FileNotFoundException();
        }

        // Fill form dinamically using dict
        public static void setFields(ref PdfAcroForm form, Dictionary<string, Object> fields) 
        {
            // iterate fields dict and fill it in form
            foreach (var field in fields)
            {
                var formField = form.GetField(field.Key);
                Type fieldType = formField.GetType();

                if (formField is not null && !formField.IsReadOnly())
                {
                    if (fieldType.Name.Equals("PdfTextFormField"))
                    {
                        formField.SetValue(field.Value.ToString());
                    }
                }
            }
        }

        // Fill form dinamically using Object
        public static void setFields(ref PdfAcroForm form, Object record)
        {
            PropertyInfo[] properties = record.GetType().GetProperties();

            foreach (var property in properties)
            {
                var formField = form.GetField(property.Name);
                Type fieldType = formField.GetType();

                if (formField is not null && !formField.IsReadOnly())
                {
                    if (fieldType.Name.Equals("PdfTextFormField"))
                    {
                        formField.SetValue(property.GetValue(record, null).ToString());
                    }
                }
            }
        }

        // Get fields from a pdfDoc AcroForm
        private static IDictionary<String, PdfFormField> getFormFields(PdfDocument src)
        {
            PdfAcroForm pdfAcroForm = new PdfFormFactory().GetAcroForm(src, false);

            if (pdfAcroForm is null)
            {
                throw new ArgumentNullException(nameof(pdfAcroForm), "Não foram encontrados campos para preenchimento.");
            }

            return pdfAcroForm.GetAllFormFields();
        }
    }
}