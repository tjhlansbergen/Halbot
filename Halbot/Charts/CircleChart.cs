using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Halbot.Code.Charts;

namespace Halbot.Charts
{
    public class CircleChart : Chart
    {
        public struct DataItem
        {
            public int X;
            public int Y;
            public int Size;
            public string Color;
        }

        public class DataSet
        {
            // a list of dataitems
            public List<DataItem> Circles { get; private set; }
            public string Name { get; private set; }

            public int YMin { get; set; }
            public int YMax { get; set; }

            public int XMin { get; set; }
            public int XMax { get; set; }

            // constructor
            public DataSet(string name, int xmin, int xmax, int ymin, int ymax)
            {
                Name = name;
                Circles = new List<DataItem>();
                XMin = xmin;
                XMax = xmax;
                YMin = ymin;
                YMax = ymax;
            }

            // setter for adding data
            public void Add(int x, int y, int size, string color)
            {
                Circles.Add(new DataItem { X = x, Y = y, Size = size, Color = color });
            }
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public DataSet Data { get; set; }


        public CircleChart(string name, int width, int height)
        {
            Name = name;
            Width = width;
            Height = height;
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

            foreach (var circle in Data.Circles)
            {
                html.AppendLine(CreateCircle(circle));
            }

            html.AppendLine("</script>");
            html.Append(Environment.NewLine);

            return html.ToString();
        }

        private string CreateCircle(DataItem item)
        {
            StringBuilder html = new StringBuilder();

            html.AppendLine($"ctx.fillStyle = '{item.Color}';");
            html.AppendLine("ctx.beginPath();");
            html.AppendLine($"ctx.arc({(item.X - Data.XMin) * Width / (Data.XMax - Data.XMin)}, { Data.YMax - ((item.Y - Data.YMin) * Height / (Data.YMax - Data.YMin)) }, {item.Size}, 0, 2 * Math.PI);");
            html.AppendLine("ctx.fill();");

            return html.ToString();
        }
    }
}
