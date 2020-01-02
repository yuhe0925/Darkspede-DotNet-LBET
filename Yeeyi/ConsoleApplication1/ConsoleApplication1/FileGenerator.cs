using System.IO;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    /// <summary>
    /// 将修改后的文本写出的类
    /// </summary>

    public class FileGenerator
    {
        private List<string> _fileLines;

        public FileGenerator()
        {    
            _fileLines = new List<string>();
        }

        public void InitiateLines()
        {
            _fileLines = new List<string>();
        }

        public void AddLines(string line)
        {
            this._fileLines.Add(line);
        }
        public void Write(string filePath, string fileName)
        {
            string folderPath = filePath;
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            File.WriteAllLines(folderPath + fileName, _fileLines);
        }
        
    }
}