// See https://aka.ms/new-console-template for more information

using RogerTechTest.Models;
using RogerTechTest.Services;
using RogerTechTest.Utils;
using System.Text;


try
{
    string inputFolderPath = StaticData.GetPath("Cesta ke složce se vstupními soubory:");
    string outputFolderPath = StaticData.GetPath("Cesta k vystupni složce, kam se mají uložit výstupní soubory:");
    string errorFolderPath = StaticData.GetPath("Cesta k chybové složce, kam se mají uložit výstupní soubory, které neprojdou kontrolou proti seznamu zdrojů:");
    string sourceFolderPath = StaticData.GetPath("Cesta k souboru se seznamem zdrojů:");
    string date = DateTime.Today.ToString("yyyyMMdd");

    SourceService sourceService = new();
    ArticleService articleService = new();
    List<Article> articles = new();

    //Načtení konfiguračních dat
    List<Source> sourceList = sourceService.GetData(sourceFolderPath);

    //Načtení dat z monitoringu
    foreach (var file in Directory.GetFiles(inputFolderPath))
    {
        articles.AddRange(articleService.GetData(file));
    }

    //Najít průnik mezi dat z monitoringu a konfguračními daty
    foreach (var doc in articles)
    {
        var sourceItem = sourceList.FirstOrDefault(x => x.Id == doc.Source);
        if (sourceItem != null)
        {
            doc.Source = sourceItem.Name;
            doc.IsCorrect = true;
        }
    }

    //Konverze na json
    string correctData = articleService.ConvertData2Output(articles, true);
    string errorData = articleService.ConvertData2Output(articles, false);

    //Vytvoření složek pokud nejsou
    if (!Directory.Exists(outputFolderPath))
        Directory.CreateDirectory(outputFolderPath);

    if (!Directory.Exists(errorFolderPath))
        Directory.CreateDirectory(errorFolderPath);

    //Nastavení orderNumber
    int maxOrderNumberOutput = StaticData.GetMaxOrderNumber(outputFolderPath, date);
    int maxOrderNumberError = StaticData.GetMaxOrderNumber(errorFolderPath, date);
    int orderNumber = (maxOrderNumberOutput < maxOrderNumberError ? maxOrderNumberError : maxOrderNumberOutput) + 1;

    //Zápis dat do souborů
    File.WriteAllText($"{outputFolderPath}\\articles_{date}_{orderNumber.ToString().PadLeft(5, '0')}.json", correctData, encoding: Encoding.UTF8);
    File.WriteAllText($"{errorFolderPath}\\articles_{date}_{orderNumber.ToString().PadLeft(5, '0')}.json", errorData, encoding: Encoding.UTF8);

    //Ahoj :)
    Console.WriteLine("Hello, World!");
}
catch (Exception ex)
{
    Console.WriteLine("Neočekávaná chyba :(");
}


