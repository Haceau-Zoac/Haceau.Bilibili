using System.Drawing;
using System.IO;

namespace Haceau.Bilibili
{
    internal static class Tools
    {
        public static string SetExtension(string src, string exten)
        {
            if (Path.GetExtension(src) != exten)
            {
                if (src.LastIndexOf('.') < 0)
                {
                    src += $"{exten}";
                }
                else
                {
                    src = src.Substring(0, src.LastIndexOf('.')) + $"{exten}";
                }
            }
            return src;
        }

        public static void Save(string src, byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                Image outputImg = Image.FromStream(ms);
                outputImg.Save(src);
            }
        }
    }
}
