using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace AwesomeFunctions
{
    class Program
    {
        static void Main(string[] args)
        {
            var consolePrinter = new TablePrinter(Console.Out, new TableRowsFormatter());
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
                var filePrinter = new TablePrinter(fileWriter, new TableRowsFormatter());
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

            using (var fileWriter = new StreamWriter(File.Create("out.json")))
            {
                var filePrinter = new TablePrinter(Console.Out, new TableJsonFormatter());
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

    public class TableJsonFormatter : ITableFormatter
    {
        public string Format(Table table)
        {
            return JsonConvert.SerializeObject(table);
        }
    }

    public class TableRowsFormatter : ITableFormatter
    {
        public string Format(Table table)
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
        private readonly ITableFormatter _tableFormatter;
        protected internal TextWriter TextWriter;

        public TablePrinter(TextWriter textWriter, ITableFormatter tableFormatter)
        {
            TextWriter = textWriter;
            _tableFormatter = tableFormatter;
        }

        public void Print(Table table)
        {
            TextWriter.WriteLine(_tableFormatter.Format(table));
        }
    }

    public interface ITableFormatter
    {
        string Format(Table table);
    }

    public class Table
    {
        public Row[] _rows { get; set; }
        public string[] _header { get; set; }

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
