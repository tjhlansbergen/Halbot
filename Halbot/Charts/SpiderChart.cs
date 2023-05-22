using Halbot.Code.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Halbot.Charts
{
    public struct MinMax
    {
        public double Min;
        public double Max;
    }

    public struct DataItem
    {
        public int Value { get; set; }
        public DateTime Date { get; set; }
    }

    public class DataSet
    {
        // a list of data items
        public List<DataItem> Items { get; set; }
        public string Name { get; }

        // constructor
        public DataSet(string name)
        {
            Name = name;
        }
    }

    public class SpiderChart : Chart
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Radius { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public DataSet Data { get; set; }

        public SpiderChart(string name, int width, int rows, DataSet data)
        {
            Name = name;
            Width = width;
            Rows = rows;

            Data = data;

            Columns = 7;
            Radius = Width / Columns / 2;
            Height = Rows * Radius * 2;
        }

        public override string CreateHTML()
        {
            StringBuilder html = new StringBuilder();

            html.Append(Environment.NewLine);
            html.AppendLine($"<canvas id=\"{Name}\" width=\"{Width}\" height=\"{Height}\" style=\"border: 1px solid #d3d3d3;\">");
            html.AppendLine("Your browser does not support the HTML5 canvas tag.");
            html.AppendLine("</canvas>");

            html.Append(Environment.NewLine);
            html.AppendLine("<script>");
            html.Append(Environment.NewLine);

            html.AppendLine($"var c = document.getElementById(\"{Name}\");");
            html.AppendLine("var ctx = c.getContext(\"2d\");");
            html.Append(Environment.NewLine);

            // CIRCLES
            html.AppendLine("// CIRCLES");
            html.AppendLine("ctx.strokeStyle = \"#f2f2f2\";");

            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    html.AppendLine("ctx.beginPath();");

                    foreach (var rad in new List<int> { 60, 45, 30, 15 })
                    {
                        html.AppendLine($"ctx.arc({Radius + (j * Radius * 2)}, {Radius + (i * Radius * 2)}, {rad}, 0, 2 * Math.PI);");
                        html.AppendLine("ctx.stroke();");
                    }
                }
            }

            html.AppendLine("// END CIRCLES");
            html.Append(Environment.NewLine);
            // END CIRCLES



            var aMinMax = new MinMax{ Max = Data.Items.Select(r => r.Value).Max(), Min = Data.Items.Select(r => r.Value).Min() };
            var aFactor = 70 / (aMinMax.Max - aMinMax.Min);

            // get the most recent monday
            var lastMonday = DateTime.UtcNow;
            while (lastMonday.DayOfWeek != DayOfWeek.Monday)
            {
                lastMonday = lastMonday.AddDays(-1);
            }

            // foreach row
            for (int i = 0; i < Rows; i++)
            {

                html.AppendLine($"// Row: {i}");

                //foreach column
                for (int j = 0; j < Columns; j++)
                {
                    html.AppendLine($"// Column: {j}");

                    var days = (0 - (i * Columns)) + j;
                    var day = lastMonday.AddDays(days).Date;
                    var items = Data.Items.Where(itm => itm.Date.Date == day).OrderByDescending(itm => itm.Value).ToList();

                    if (!items.Any()) continue;

                    var x = Radius + (j * Radius * 2);
                    var y = Radius + (i * Radius * 2);

                    for (int k = 0; k < items.Count; k++)
                    {
                        x += k * 20;

                        var a = Convert.ToInt32((items[k].Value - aMinMax.Min) * aFactor) + 5;

                        html.AppendLine($"ctx.fillStyle = \"{GetColors(items.First().Date.DayOfWeek).Item1}\";");
                        html.AppendLine($"ctx.strokeStyle = \"{GetColors(items.First().Date.DayOfWeek).Item2}\";");
                        html.AppendLine("ctx.beginPath();");
                        html.AppendLine("ctx.lineWidth = 2;");
                        html.AppendLine($"ctx.arc({x}, {y}, {a}, 0, 2 * Math.PI);");
                        html.AppendLine("ctx.fill();");
                        html.AppendLine("ctx.stroke();");
                    }
                }
            }

            html.AppendLine("</script>");
            html.Append(Environment.NewLine);


            return html.ToString();
        }

        private static Tuple<string, string> GetColors(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday:
                    return new Tuple<string, string>("#deede4", "#89BD9E");
                case DayOfWeek.Tuesday:
                    return new Tuple<string, string>("#b4e4fd", "#058ED9");
                case DayOfWeek.Wednesday:
                    return new Tuple<string, string>("#f6d5df", "#8B1E3F");
                case DayOfWeek.Thursday:
                    return new Tuple<string, string>("#f9ebd2", "#F0C987");
                case DayOfWeek.Friday:
                    return new Tuple<string, string>("#f2d9f1", "#3C153B");
                case DayOfWeek.Saturday:
                    return new Tuple<string, string>("#b8f9f8", "#08605F");
                case DayOfWeek.Sunday:
                    return new Tuple<string, string>("#f7d7d4", "#DB4C40");
                default:
                    return new Tuple<string, string>("grey", "black");
            }
        }
    }
}
