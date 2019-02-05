using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AwesomeFunctions
{
    class Program
    {
        static void Main(string[] args)
        {
            var tablePrinter = new TablePrinter(new Table());
            tablePrinter.Print();
        }
    }

    public class TableBuilder
    {
        private Table _table;

        public TableBuilder(Table table)
        {
            _table = table;
        }

        public string BuildHeader()
        {
            var headers = GetHeadersName();
            StringBuilder headerBuilder = new StringBuilder(" | ");
        
            foreach (var element in headers)
            {
                headerBuilder.Append(element);
                headerBuilder.Append(" | ");
            }

            return headerBuilder.ToString();
        }

        public string BuildRows()
        {
            StringBuilder rowsBuilder = new StringBuilder();
            var rows = _table.GetRows();

            foreach (var row in rows)
            {
                rowsBuilder.AppendFormat(" {0} {1} {2} ", row.FirstName, row.LastName, row.Job);
                rowsBuilder.AppendLine();
            }

            return rowsBuilder.ToString();
        }

        private string[] GetHeadersName()
        {
            var headers = _table.GetRows()[0].GetType().GetProperties();
            return headers.Select(x => x.Name).ToArray();
        }
    }

    public class TablePrinter
    {
        private Table _table;
        private TableBuilder _tableBuilder;

        public TablePrinter(Table table)
        {
            _table = table;
            _tableBuilder = new TableBuilder(table);
        }

        public void Print()
        {
            PrintHeader();
            PrintRows();
        }

        private void PrintHeader()
        {
            var header = _tableBuilder.BuildHeader();
       
            Console.WriteLine(header);
        }

        private void PrintRows()
        {
            var rows = _tableBuilder.BuildRows();

            Console.WriteLine(rows);
        }
    }

    public class Table
    {
        private readonly Row[] rows =
        {
            new Row("Adrian", "Stanescu", "Salahor"),
            new Row("Ionut", "Dumitrescu", "Programator"),
            new Row("Adrian", "Stanescu", "Salahor"),
        };

        public Row[] GetRows()
        {
            return rows;
        }
    }

    public class Row
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Job { get; set; }

        public Row(string firstName, string lastName, string job)
        {
            FirstName = firstName;
            LastName = lastName;
            Job = job;
        }
    }
}
