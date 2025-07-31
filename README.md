# INetPdfUtil Library

The `INetPdfUtil` library simplifies working with PDF forms that use AcroForms. It provides services to fill PDF templates with data from various sources and handle form fields efficiently.

## Features

- **Bulk Fill PDF Forms**: Populate multiple PDFs using a CSV file as data source.
- **Single PDF Fill**: Populate a single PDF form using a dictionary of field values.
- **Retrieve Field Names**: Get all the AcroForm fields from a PDF template.

## Installation

Install the package via NuGet:

dotnet add package INetPdfUtil

Or by using the NuGet Package Manager in Visual Studio:

Install-Package itextPdfFill

## Usage
Here are examples of how to use the services provided by the package.

1. Bulk Fill PDF Forms
You can generate multiple PDFs by filling a template with data from a CSV file. The first column of the CSV will be used to name the output PDFs.

Example:

using itextPdfFill.Interface;
using itextPdfFill.Services;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        ITemplateFillService fillService = new TemplateFillService();
        
        string pathTemplate = "template.pdf"; // Path to your PDF template
        string pathDest = "output/";          // Directory for the generated PDFs
        string pathData = "data.csv";         // Path to your CSV data file
        List<string> fields = new List<string> { "FirstName", "LastName", "Email" }; // Fields to map

        fillService.BulkFill(pathTemplate, pathDest, pathData, fields);
    }
}

2. Fill a Single PDF Form

You can also fill a single PDF form using a Dictionary<string, object> to provide field values.

Example:

using itextPdfFill.Interface;
using itextPdfFill.Services;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        ITemplateFillService fillService = new TemplateFillService();

        string pathTemplate = "template.pdf"; // Path to your PDF template
        string pathDest = "output/filledForm.pdf"; // Path to the filled PDF

        Dictionary<string, object> fields = new Dictionary<string, object>
        {
            { "FirstName", "John" },
            { "LastName", "Doe" },
            { "Email", "johndoe@example.com" }
        };

        fillService.Fill(pathTemplate, pathDest, fields);
    }
}

3. Retrieve Field Names from a PDF Template
You can retrieve all field names from a PDF template to know what fields you need to populate.

Example:

using itextPdfFill.Interface;
using itextPdfFill.Services;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        ITemplateFillService fillService = new TemplateFillService();
        
        string pathTemplate = "template.pdf"; // Path to your PDF template
        List<string> fieldNames = fillService.GetFieldsNames(pathTemplate);

        foreach (var field in fieldNames)
        {
            Console.WriteLine(field); // Outputs the field names
        }
    }
}

## Prerequisites

iText7: This library uses iText7 for handling PDF forms.
CSV Data: For BulkFill, the CSV file should match the fields expected in the PDF template.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Contributing
If you'd like to contribute, please fork the repository and use a feature branch. Pull requests are welcome.

Happy PDF Filling! 🎉

### Key sections:
1. **Installation**: Instructions for installing the package via NuGet.
2. **Usage**: Detailed code examples for each method (`BulkFill`, `Fill`, and `GetFieldsNames`).
3. **Prerequisites**: List of necessary tools or frameworks (like iText7).
4. **License**: Mention the license for the project.

Feel free to adjust the content based on any additional details or features of your library.
