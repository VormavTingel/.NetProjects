using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using SalesReportCLI.Models;

namespace SalesReportCLI.Services
{
    public class SalesProcessor
    {
        public SalesProcessor()
        {
        }

        public List<SaleRecord> ReadSalesFromCsv(string inputFile)
        {
            if (!File.Exists(inputFile))
                throw new FileNotFoundException($"Arquivo não encontrado: {inputFile}");

            var config = new CsvConfiguration(new CultureInfo("pt-BR"))
            {
                Delimiter = ",",
                HasHeaderRecord = true
            };

            using var reader = new StreamReader(inputFile);
            using var csv = new CsvReader(reader, config);

            var salesRecords = csv.GetRecords<SaleRecord>().ToList();
            return salesRecords;
        }

        public decimal CalculateTotalSales(List<SaleRecord> sales)
        {
            return sales.Sum(s => s.TotalPrice);
        }

        public void SaveReport(string outputFile, decimal totalSales)
        {
            using var writer = new StreamWriter(outputFile);
            writer.WriteLine($"Total de vendas: {totalSales}");
        }
    }
}
