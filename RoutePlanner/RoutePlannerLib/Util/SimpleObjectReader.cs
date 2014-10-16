using System.Globalization;
using System.IO;
using System.Reflection;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util
{
    public class SimpleObjectReader
    {
        private StringReader stream;

        public SimpleObjectReader(StringReader stream)
        {
            this.stream = stream;
        }

        public object Next()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var readLine = stream.ReadLine();
            if (readLine == null) 
                return null;

            var obj = assembly.CreateInstance(readLine.Split(' ')[2]);
            if (obj == null) 
                return null;

            var type = obj.GetType();
            while ((readLine = stream.ReadLine()) != null && !readLine.Equals("End of instance"))
            {
                if (readLine.EndsWith("is a nested object..."))
                {
                    type.GetProperty(readLine.Split(' ')[0]).SetValue(obj, Next());
                }
                else
                {
                    var propertyValues = readLine.Split('=');
                    var property = type.GetProperty(propertyValues[0]);
                    switch (property.PropertyType.Name)
                    {
                        case "String":
                            property.SetValue(obj, propertyValues[1].Replace("\"", ""));
                            break;
                        case "Int32":
                            property.SetValue(obj, int.Parse(propertyValues[1]));
                            break;
                        case "Double":
                            property.SetValue(obj, double.Parse(propertyValues[1], CultureInfo.InvariantCulture));
                            break;
                    }
                }
            }
            return obj;
        }
    }
}
