using System.Collections.Generic;
using System.Globalization;
using System.IO;


namespace Task3
{
    public class FileReader
    {
        public static List<Point> ReadCsv(string path)
        {
            List<Point> list = new List<Point>();

            using var reader = new StreamReader(path);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                list.Add(new Point(double.Parse(values[0], CultureInfo.InvariantCulture),
                                   double.Parse(values[1], CultureInfo.InvariantCulture)));
            }

            return list;
        }
    }
}
