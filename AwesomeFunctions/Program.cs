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

            tablePrinter.Print(new Table(
                new[] { "First Name", "Last Name", "Job", "Age" },
                new[]
                {
                    new Row("Adrian", "Stanescu", "Salahor", "12"),
                    new Row("Ionut", "Dumitrescu", "Programator","33"),
                    new Row("Adrian", "Stanescu", "Salahor"),
                }));

        }
    }

    public class TableBuilder
    {
        public string BuildTable(Table table)
        {
            var rowsBuilder = new StringBuilder();

            BuildHeader(table.GetHeaderRow(), rowsBuilder);
            BuildRows(table.GetRows(), rowsBuilder);

            return rowsBuilder.ToString();
        }

        private static void BuildRows(Row[] rows, StringBuilder rowsBuilder)
        {
            foreach (var row in rows)
            {
                foreach (var column in row.Columns)
                {
                    rowsBuilder.AppendFormat("{0} ", column);
                }

                rowsBuilder.AppendLine();
            }
        }

        private void BuildHeader(string[] headers, StringBuilder rowsBuilder)
        {
            rowsBuilder.Append("| ");
        
            foreach (var element in headers)
            {
                rowsBuilder.Append(element);
                rowsBuilder.Append("| ");
            }

            rowsBuilder.AppendLine();
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
            Console.WriteLine(_tableBuilder.BuildTable(table));
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
        public string[] Columns { get; set; }

        public Row(params string[] columns)
        {
            Columns = columns;
        }
    }
}
