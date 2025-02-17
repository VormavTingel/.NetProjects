using System;
using SalesReportCLI.Services;
using System.IO;

namespace SalesReportCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                PrintHelp();
                return;
            }

            var command = args[0].ToLower();

            switch (command)
            {
                case "generate":
                    HandleGenerateCommand(args);
                    break;
                default:
                    Console.WriteLine($"Comando '{command}' não é válido.");
                    PrintHelp();
                    break;
            }
        }

        static void PrintHelp()
        {
            Console.WriteLine("Uso:");
            Console.WriteLine("  SalesReportCLI generate -i <arquivoCSV> -o <arquivoSaida>");
            Console.WriteLine("Exemplo:");
            Console.WriteLine("  dotnet run -- generate -i vendas.csv -o relatorio.txt");
        }

        static void HandleGenerateCommand(string[] args)
        {
            string inputFile = null;
            string outputFile = null;

            for (int i = 1; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-i":
                        if (i + 1 < args.Length)
                            inputFile = args[++i];
                        break;
                    case "-o":
                        if (i + 1 < args.Length)
                            outputFile = args[++i];
                        break;
                }
            }

            if (string.IsNullOrEmpty(inputFile))
            {
                Console.WriteLine("Arquivo de entrada não informado. Use -i <arquivoCSV>.");
                return;
            }

            // Cria instância do nosso serviço
            var processor = new SalesProcessor();

            try
            {
                // 1) Ler as vendas do CSV
                var sales = processor.ReadSalesFromCsv(inputFile);

                // 2) Calcular total
                var totalSales = processor.CalculateTotalSales(sales);

                // 3) Mostrar no console
                Console.WriteLine($"Total de vendas: {totalSales}");

                // 4) Se tiver arquivo de saída, salvar o relatório
                if (!string.IsNullOrEmpty(outputFile))
                {
                    processor.SaveReport(outputFile, totalSales);
                    Console.WriteLine($"Relatório salvo em '{outputFile}'");
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar: {ex.Message}");
            }
        }
    }
}
