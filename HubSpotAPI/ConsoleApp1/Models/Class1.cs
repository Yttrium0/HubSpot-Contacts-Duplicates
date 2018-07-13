using System.IO;

namespace ConsoleApp1.Models {
    class Writer
    {
        public static void ImportDataToFile(string data) {
            File.AppendAllText("./file.txt", data);
        }
    }
}
