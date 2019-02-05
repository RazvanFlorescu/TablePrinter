using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AwesomeFunctions
{
    class Program
    {
        static void Main(string[] args)
        {
            var tablePrinter = new TablePrinter();
            tablePrinter.Print(new Table(
                new[] {"First Name", "Last Name", "Job"},
                new[]
                {
                    new Row("Adrian", "Stanescu", "Salahor"),
                    new Row("Ionut", "Dumitrescu", "Programator"),
                    new Row("Adrian", "Stanescu", "Salahor"),
                }));
        }
    }

    public class TableBuilder
    {
        public string BuildHeader(string[] headers)
        {
            StringBuilder headerBuilder = new StringBuilder(" | ");
        
            foreach (var element in headers)
            {
                headerBuilder.Append(element);
                headerBuilder.Append(" | ");
            }

            return headerBuilder.ToString();
        }

        public string BuildRows(Table table)
        {
            StringBuilder rowsBuilder = new StringBuilder();
            var rows = table.GetRows();

            foreach (var row in rows)
            {
                rowsBuilder.AppendFormat(" {0} {1} {2} ", row.FirstName, row.LastName, row.Job);
                rowsBuilder.AppendLine();
            }

            return rowsBuilder.ToString();
        }
    }

    public class TablePrinter
    {
        private readonly TableBuilder _tableBuilder;

        public TablePrinter()
        {
            _tableBuilder = new TableBuilder();
        }

        public void Print(Table table)
        {
            PrintHeader(table.GetHeaderRow());
            PrintRows(table);
        }

        private void PrintHeader(string[] headers)
        {
            var header = _tableBuilder.BuildHeader(headers);
       
            Console.WriteLine(header);
        }

        private void PrintRows(Table table)
        {
            var rows = _tableBuilder.BuildRows(table);

            Console.WriteLine(rows);
        }
    }

    public class Table
    {
        private readonly Row[] _rows;
        private readonly string[] _header;

        public Table(string[] header, IEnumerable<Row> rows)
        {
            _rows = rows.ToArray();
            _header = header;
        }

        public Row[] GetRows()
        {
            return _rows;
        }

        public string[] GetHeaderRow()
        {
            return _header;
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
