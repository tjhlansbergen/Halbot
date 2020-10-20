using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Halbot.Code.Charts;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;

namespace Halbot.Charts
{
    public struct MinMax
    {
        public double Min;
        public double Max;
    }

    public struct DataItem
    {
        public double A;
        public double B;
        public double C;
        public double D;

        public string FillColor;
        public string StrokeColor;
    }

    public class DataSet
    {
        // a list of dataitems
        public List<List<DataItem>> Rows { get; private set; }
        public string Name { get; private set; }

        // constructor
        public DataSet(string name)
        {
            Name = name;
            Rows = new List<List<DataItem>>();
        }

        // setter for adding data
        public void AddRow(List<DataItem> row)
        {
            Rows.Add(row);
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

        public SpiderChart(string name, int width, int height, int radius, int rows, int columns)
        {
            Name = name;
            Width = width;
            Height = height;
            Radius = radius;
            Rows = rows;
            Columns = columns;
        }

        public void SetDataSet(DataSet data)
        {
            Data = data;
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
            html.AppendLine("ctx.strokeStyle = \"#f0f0f0\";");

            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    html.AppendLine("ctx.beginPath();");

                    foreach (var rad in new List<int> { 60, 40, 20 })
                    {
                        html.AppendLine($"ctx.arc({Radius + (j * Radius * 2)}, {Radius + (i * Radius * 2)}, {rad}, 0, 2 * Math.PI);");
                        html.AppendLine("ctx.stroke();");
                    }
                }
            }

            html.AppendLine("// END CIRCLES");
            html.Append(Environment.NewLine);
            // END CIRCLES



            var aMinMax = new MinMax{ Max = Data.Rows.SelectMany(r => r.Select(c => c.A)).Max(), Min = Data.Rows.SelectMany(r => r.Select(c => c.A)).Min() };
            var bMinMax = new MinMax { Max = Data.Rows.SelectMany(r => r.Select(c => c.B)).Max(), Min = Data.Rows.SelectMany(r => r.Select(c => c.B)).Min() };
            var cMinMax = new MinMax { Max = Data.Rows.SelectMany(r => r.Select(c => c.C)).Max(), Min = Data.Rows.SelectMany(r => r.Select(c => c.C)).Min() };
            var dMinMax = new MinMax { Max = Data.Rows.SelectMany(r => r.Select(c => c.D)).Max(), Min = Data.Rows.SelectMany(r => r.Select(c => c.D)).Min() };

            var aFactor = 70 / (aMinMax.Max - aMinMax.Min);
            var bFactor = 70 / (bMinMax.Max - bMinMax.Min);
            var cFactor = 70 / (cMinMax.Max - cMinMax.Min);
            var dFactor = 70 / (dMinMax.Max - dMinMax.Min);
            
            // foreach row
            for (int i = 0; i < Data.Rows.Take(Rows).ToArray().Length; i++)
            {
                var rowItems = Data.Rows[i].Take(Columns).ToArray();

                html.AppendLine($"// Row: {i}");

                //foreach column
                //foreach (var dataItem in rowItems)
                for (int j = 0; j < rowItems.Count(); j++)
                {
                    var item = rowItems[j];

                    var x = Radius + (j * Radius * 2);
                    var y = Radius + (i * Radius * 2);

                    var a = Convert.ToInt32((item.A - aMinMax.Min) * aFactor) + 5;
                    var b = Convert.ToInt32((item.B - bMinMax.Min) * bFactor) + 5;
                    var c = Convert.ToInt32((item.C - cMinMax.Min) * cFactor) + 5;
                    var d = Convert.ToInt32((item.D - dMinMax.Min) * dFactor) + 5;

                    html.AppendLine($"ctx.fillStyle = \"{item.FillColor}\";");
                    html.AppendLine($"ctx.strokeStyle = \"{item.StrokeColor}\";");
                    html.AppendLine("ctx.beginPath();");

                    html.AppendLine($"ctx.moveTo({x}, {y} - {a});");     // move up by A (top)
                    html.AppendLine($"ctx.lineTo({x} + {b}, {y});");     // line to B (right)
                    html.AppendLine($"ctx.lineTo({x}, {y} + {c});");     // line to C (bottom)
                    html.AppendLine($"ctx.lineTo({x} - {d}, {y});");     // line to D (left)
                    html.AppendLine($"ctx.lineTo({x}, {y} - {a});");     // back to start (top)

                    html.AppendLine("ctx.fill();");
                    html.AppendLine("ctx.stroke();");
                }
            }

            html.AppendLine("</script>");
            html.Append(Environment.NewLine);


            return html.ToString();
        }
    }
}
