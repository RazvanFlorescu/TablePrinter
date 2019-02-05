using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AwesomeFunctions
{
    class Program
    {
        static void Main(string[] args)
        {
            var consolePrinter = new TablePrinter(Console.Out);
            consolePrinter.Print(new Table(
                new[] {"First Name", "Last Name", "Job"},
                new[]
                {
                    new Row("Adrian", "Stanescu", "Salahor"),
                    new Row("Ionut", "Dumitrescu", "Programator"),
                    new Row("Adrian", "Stanescu", "Salahor"),
                }));

            consolePrinter.Print(new Table(
                new[] { "First Name", "Last Name", "Job", "Age" },
                new[]
                {
                    new Row("Adrian", "Stanescu", "Salahor", "12"),
                    new Row("Ionut", "Dumitrescu", "Programator","33"),
                    new Row("Adrian", "Stanescu", "Salahor"),
                }));


            using (var fileWriter = new StreamWriter(File.Create("out.txt")))
            {
                var filePrinter = new TablePrinter(fileWriter);
                filePrinter.Print(new Table(
                    new[] { "First Name", "Last Name", "Job" },
                    new[]
                    {
                    new Row("Adrian", "Stanescu", "Salahor"),
                    new Row("Ionut", "Dumitrescu", "Programator"),
                    new Row("Adrian", "Stanescu", "Salahor"),
                    })
                );
            }
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
        protected internal TextWriter TextWriter;

        public TablePrinter(TextWriter textWriter)
        {
            TextWriter = textWriter;
            _tableBuilder = new TableBuilder();
        }

        public void Print(Table table)
        {
            TextWriter.WriteLine(_tableBuilder.BuildTable(table));
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
