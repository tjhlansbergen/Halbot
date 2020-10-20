using Halbot.Code.Charts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halbot.Charts
{
    public class LineChart : Chart
    {
        public class DataSet
        {
            public List<int> Values { get; set; }
            public string Name { get; set; }
            public string Color { get; set; }
        }


        public int Width { get; set; }
        public int Height { get; set; }
        public List<DataSet> DataSets { get; set; }

        public LineChart(string name, int width, int height)
        {
            Name = name;
            Width = width;
            Height = height;
            DataSets = new List<DataSet>();
        }

        public void AddDataSet(DataSet data)
        {
            DataSets.Add(data);
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

            int left = -10;

            foreach (var set in DataSets)
            {
                html.AppendLine(CreateSection(set, left));
                left += (set.Values.Count - 1) * 1;
            }

            html.AppendLine("</script>");
            html.Append(Environment.NewLine);

            return html.ToString();
        }

        private string CreateSection(DataSet dataSet, int left)
        {
            if (dataSet.Values.Count < 2) return string.Empty;

            StringBuilder html = new StringBuilder();

            html.AppendLine($"ctx.fillStyle = '{dataSet.Color}';");
            html.AppendLine("ctx.beginPath();");

            html.AppendLine($"ctx.moveTo({left}, {Height});");

            for (int i = 0; i < dataSet.Values.Count; i++)
            {
                html.AppendLine($"ctx.lineTo({left + (i * 1)}, {Height - dataSet.Values[i]});");
            }

            html.AppendLine($"ctx.lineTo({left + ((dataSet.Values.Count - 1) * 1)}, {Height});");

            html.AppendLine("ctx.fill();");
            return html.ToString();
        }
    }
}
