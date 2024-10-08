namespace itextPdfFill.Interface
{
   public interface ITemplateFillService
    {
        void BulkFill(string pathTemplate, string pathDest, string pathData, List<string> fields);

        void Fill(string pathTemplate, string pathDest, Dictionary<string, Object> fields);

        List<string> GetFieldsNames(string pathTemplate);
    }
}
