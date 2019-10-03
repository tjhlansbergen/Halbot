/// <summary>
///  ColumnChart
///  Tako Lansbergen
///  08-2019
///  
///  HTML generator for column charts
/// </summary>
/// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Halbot.Code.Charts
{
    public class ColumnChart : Chart
    {
        /// <summary>
        /// struct for one set of column chart data
        /// </summary>
        public class DataSet
        {

            // private property, a list of data with a label, value-text and a value
            public List<Tuple<string, string, double>> Data { get; private set; } 
            public string Name { get; private set; }

            // constructor
            public DataSet(string name)
            {
                Name = name;
                Data = new List<Tuple<string, string, double>>();
            }

            // setter for adding data
            public void Add(string label, string value_text, double value)
            { 
                Data.Add(new Tuple<string, string, double>(label, value_text, value));
            }
        }


        // private properties
        public List<DataSet> DataSets { get; private set; }
        public int ChartHeight { get; private set; }
        public int NumberOfColumns { get; private set; }
        public int BarsPerColumn { get; private set; }

        // constructor
        public ColumnChart(string name, int height_in_px)
        {
            Name = name;
            DataSets = new List<DataSet>();
            ChartHeight = height_in_px;
        }

        /// <summary>
        /// add's data to the chart. At least one set of data is needed to create the chart, when supplying multiple sets of data a multi-bar column charts is created.
        /// Number of columns and the columns label's are determined from the first set of data only.
        /// </summary>
        public void AddDataSet(DataSet data)
        {
            //add dataset
            DataSets.Add(data);

            //keep track of items
            NumberOfColumns = DataSets.First().Data.Count;
            BarsPerColumn = DataSets.Count;
        }

        public override string CreateHTML()
        {
            //bail out if no data
            if (NumberOfColumns < 1) return string.Empty;

            StringBuilder charthtml = new StringBuilder();
            int barwidth = 100 / (NumberOfColumns * BarsPerColumn);

            //start with the bars
            charthtml.AppendLine(string.Format("<tr class=\"{0} columns\">", Name));
            for (int column = 0; column < NumberOfColumns; column++)
            {
                for (int bar = 0; bar < BarsPerColumn; bar++)
                {
                    if (column < DataSets[bar].Data.Count)    //dont try to fetch non-existing data
                    {
                        //calculate height ratio
                        double height_ratio = DataSets[bar].Data.OrderByDescending(item => item.Item3).First().Item3 / 100.00;

                        // surrounding table-cell
                        charthtml.Append(string.Format("<td style=\"vertical-align: bottom; width: {0}%;\">", barwidth));
                        // the actual bar as div
                        charthtml.Append(string.Format("<div class=\"{0} {1}\" style=\"width: 100%; height: {2}%\"></div>", Name, DataSets[bar].Name, (int)(DataSets[bar].Data[column].Item3 / height_ratio)));
                        charthtml.AppendLine("</td>");
                    }
                    else //empty cell
                    {
                        charthtml.AppendLine("<td></td>");
                    }
                }
                //empty cell as padding between columns
                if (column < NumberOfColumns - 1) charthtml.AppendLine("<td></td>");
            }
            charthtml.AppendLine("</tr>");

            //row for labels, based on the labels provided in the first DataSet
            charthtml.AppendLine(string.Format("<tr class=\"{0} labels\" style=\"height: 0;\">", Name));
            for (int label = 0; label < NumberOfColumns; label++)
            {
                charthtml.Append(string.Format("<td colspan={0} class=\"{1} label\">", BarsPerColumn, Name));
                charthtml.AppendFormat("{0}", DataSets.First().Data[label].Item1);
                charthtml.AppendLine("</td>");

                //empty cell as padding between columns
                if (label < NumberOfColumns - 1) charthtml.AppendLine("<td></td>");

            }
            charthtml.AppendLine("</tr>");

            //rows for value-text
            for (int row = 0; row < BarsPerColumn; row++)
            {
                charthtml.AppendLine(string.Format("<tr class=\"{0} values\" style=\"height: 0;\">", Name));
                for (int column = 0; column < NumberOfColumns; column++)
                {
                    if (column < DataSets[row].Data.Count)    //dont try to fetch non-existing data
                    {
                        charthtml.Append(string.Format("<td colspan={0} class=\"{1} {2}\">", BarsPerColumn, Name, DataSets[row].Name));
                        charthtml.AppendFormat("{0}", DataSets[row].Data[column].Item2);
                        charthtml.AppendLine("</td>");
                    }
                    else //empty cell
                    {
                        charthtml.AppendLine(string.Format("<td colspan={0}></td>", BarsPerColumn));
                    }

                    //empty cell as padding between columns
                    if (column < NumberOfColumns - 1) charthtml.AppendLine("<td></td>");

                }
                charthtml.AppendLine("</tr>");
            }

            //wrap in table tags
            charthtml.Insert(0, string.Format("<table class=\"{0}\" style=\"height: {1}px; width: 100%;\">{2}", Name, ChartHeight, Environment.NewLine));
            charthtml.AppendLine("</table>");

            return charthtml.ToString();
        }
    }
}
