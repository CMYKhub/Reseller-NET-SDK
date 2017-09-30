using System.IO;
using System.Reflection;

namespace CMYKhub.ResellerApi.Client.Tests
{
    internal static class StringExtensions
    {
        public static string ReadStringResource(this string name)
        {
            using (Stream stream = name.ReadResourceStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static Stream ReadResourceStream(this string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"CMYKhub.ResellerApi.Client.Tests.{name}";

            return assembly.GetManifestResourceStream(resourceName);
        }
    }
}
