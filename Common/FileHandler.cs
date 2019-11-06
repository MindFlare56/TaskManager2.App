using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class FileHandler
    {

        public static string GetDesktopPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        public static string WriteJsonInFile<T>(List<T> data, string fileName = "Data.json", string path = "")
        {
            path = ReplaceEmptyPath(path, fileName);
            string json = JsonConvert.SerializeObject(data.ToArray());
            WriteInFile(path: path, text: json);
            return path;
        }

        public static string WriteJsonInFile<T>(T data, string fileName = "Data.json", string path = "")
        {
            path = ReplaceEmptyPath(path, fileName);
            string json = JsonConvert.SerializeObject(data);
            WriteInFile(path: path, text: json);
            return path;
        }

        public static List<T> ReadJsonInFile<T>(string fileName = "Data.json", string path = "")
        {
            path = ReplaceEmptyPath(path, fileName);
            using (StreamReader r = new StreamReader(path)) {
                string json = r.ReadToEnd();
                List<T> items = JsonConvert.DeserializeObject<List<T>>(json);
                return items;
            }
        }

        public static Dictionary<Key, Value> ReadJsonInFile<Key, Value>(string fileName = "Data.json", string path = "")
        {
            return JsonConvert.DeserializeObject<Dictionary<Key, Value>>(ReadInFile(path));
        }

        public static bool FileExist(String path)
        {
            return File.Exists(path);
        }

        public static void CreateFile(String path, String text_info)
        {
            try {
                if (File.Exists(path)) {
                    File.Delete(path);
                }
                using (FileStream fs = File.Create(path)) {
                    Byte[] info = new UTF8Encoding(true).GetBytes(text_info);
                    fs.Write(info, 0, info.Length);
                }
                using (StreamReader sr = File.OpenText(path)) {
                    string s = "";
                    while ((s = sr.ReadLine()) != null) {
                        Console.WriteLine(s);
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }

        public static T GetLastJsonElement<T>(string fileName = "Data.json", string path = "")
        {
            var items = ReadJsonInFile<T>(fileName, path);
            if (items.Count != 0) {
                return items.Last();
            }
            return default(T);
        }       

        public static void CreateFile(String path)
        {
            try {
                if (File.Exists(path)) {
                    File.Delete(path);
                }
                using (StreamReader sr = File.OpenText(path)) {
                    string s = "";
                    while ((s = sr.ReadLine()) != null) {
                        Console.WriteLine(s);
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void WriteInFile(string text = "", string fileName = "Data.json", string path = "")
        {
            path = ReplaceEmptyPath(path, fileName);
            using (StreamWriter writetext = new StreamWriter(path)) {
                writetext.WriteLine(text);
            }
        }

        public static void ClearFile(string fileName = "Data.json", string path = "")
        {
            WriteInFile(text: "", fileName: fileName, path: path);
        }

        public static void RewriteInFile(string text, string fileName = "Data.json", string path = "")
        {
            path = ReplaceEmptyPath(path, fileName);
            WriteInFile(path: path, text: ReadInFile(path) + text);
        }

        public static String ReadInFile(String path)
        {
            using (StreamReader streamReader = new StreamReader(path)) {
                return streamReader.ReadToEnd();
            }
        }

        public static string GetPathFromSolution(string path)
        {
            var solutionPath = GetSolutionPath();
            return Path.GetFullPath(Path.Combine(solutionPath, path));
        }

        public static string GetSolutionPath()
        {
            return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        }

        private static string ReplaceEmptyPath(string path, string fileName)
        {           
            if (path == "") {
                return Environment.CurrentDirectory + "\\" + fileName;
            }
            FileAttributes fileAttributes = File.GetAttributes(path);
            if ((fileAttributes & FileAttributes.Directory) == FileAttributes.Directory) {
                return path + "\\" + fileName;
            }
            return path;                                  
        }        
    }
}
