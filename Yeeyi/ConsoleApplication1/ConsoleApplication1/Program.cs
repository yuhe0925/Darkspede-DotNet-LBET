using System;
using System.IO;
using System.Linq;

namespace ConsoleApplication1
{
    /// <summary>
    /// Main类
    /// </summary>

    internal class Program
    {
        public const string PROJECT_PATH = "CSV/";
            //全路径为: "ConsoleApplication1/ConsoleApplication1/CSV/";
        //图片保存路径
        public const string IMAGE_PATH = "Image/";
        //修改过后的文件保存路径
        public const string UPDATED_FILE_PATH = "Edited/";
        //原文件默认是第五列为地址
        public const int DEFAULT_ADDRESS_COLUMN = 5;

        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.GetEncoding("gb2312");
            FileReader fileReader = new FileReader();

            string[] csvList = GetAllCsv();
            
            Execution(csvList, fileReader);
        }

        private static string[] GetAllCsv()
        {
            string[] csvList;
            csvList = Directory.GetFiles(PROJECT_PATH, "*.csv", SearchOption.AllDirectories);
            return csvList;
        }

        private static void Execution(string[] csvList, FileReader fileReader)
        {
            string filePath, fileName;
            for (int i = 0; i < csvList.Length; i++)
            {
                Console.WriteLine("\n\n--------------------------Start New File: {0}--------------------------\n\n", csvList[i]);
                string temp = csvList[i].Replace("CSV/", "");
                if (temp.Split('/').Length != 1)
                {
                    filePath = PROJECT_PATH + UPDATED_FILE_PATH + temp.Split('/')[0] + '/';
                    fileName = string.Format("{0}_edited.csv", temp.Split('.')[0].Split('/').Last());
                }
                else
                {
                    filePath = PROJECT_PATH + UPDATED_FILE_PATH;
                    fileName = string.Format("{0}_edited.csv", temp.Split('.')[0]);
                }

                fileReader.ReadFile(csvList[i]);
                fileReader.LineExecution(filePath, fileName);
            }
        }
    }
}