using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using System.Linq;


namespace ConsoleApplication1
{
    /// <summary>
    /// 核心类, 用于读取文件并执行对应逻辑
    /// </summary>

    public class FileReader
    {
        private List<string[]> fileText;
        private List<string[]> mapText;
        private ImageDownloader imageDownloader;
        private GuidGenerator guidGenerator;
        private FileGenerator fileGenerator;
        private Coordinate coordinate;

        public FileReader()
        {
            fileText = new List<string[]>();
            mapText = new List<string[]>();
            imageDownloader = new ImageDownloader();
            guidGenerator = new GuidGenerator();
            fileGenerator = new FileGenerator();
            coordinate = new Coordinate();
        }

        public void ReadFile(string fileName)
        {
            //读取文件方法
            fileText.Clear();
            mapText.Clear();

            StreamReader reader = new StreamReader(fileName, Encoding.GetEncoding("gb2312")); 
            StringBuilder stringBuilder = new StringBuilder();
            string str;
            while ((str = reader.ReadLine()) != null)
            {
                stringBuilder.Clear();
                stringBuilder.Append(str);
                fileText.Add(stringBuilder.ToString().Split(','));
                mapText.Add( Regex.Split(stringBuilder.ToString(),
                    "\\G(?:^|,)(?:\"([^\"]*(?:\"\"[^\"]*)*)\"|([^\",]*))"));
            }
                        
            fileGenerator.InitiateLines();
            fileGenerator.AddLines("guid,lagitude,longitude," + string.Join(",", fileText[0]));
        }

        public void LineExecution(string folderPath, string fileName)
        {
            //依行执行方法
            for (int i = 1; i < fileText.Count; i++)
            {
                //控制台显示进度
                Console.Title = (string.Format("{0}% of file {1} is completed!!!\n\n", (float)i/(float)fileText.Count*100, fileName)); 
                Console.WriteLine("Line No.{0} of {1} of file {2}~~~", i, fileText.Count, fileName);

                //此行过短
                if (mapText[i].Length < 2 * Program.DEFAULT_ADDRESS_COLUMN)
                {
                    Console.WriteLine("This line is trashline\n");
                    continue;
                }

                if (!mapText[i][Program.DEFAULT_ADDRESS_COLUMN * 2 - 1].Equals(""))
                {
                    string temp = mapText[i][Program.DEFAULT_ADDRESS_COLUMN * 2 - 1];
                    string lat;
                    string lgt;
                    Console.WriteLine(temp);
                    Console.WriteLine(lat = coordinate.GetCoordinatesFromAddress(temp)[0]);
                    Console.WriteLine(lgt = coordinate.GetCoordinatesFromAddress(temp)[1]);

                    string guid = guidGenerator.GetNewGuid();
                    try
                    {
                        ExtractImage(i, guid,
                            folderPath.Split(new string[] { "Edited" }, StringSplitOptions.None).Last()
                            + fileName.Split('_')[0]);
                            
                        fileGenerator.AddLines(guid + ',' + lat.Trim() + ',' + lgt.Trim() + ',' + string.Join(",", fileText[i]) + "\"");
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Super trash line");
                        Console.WriteLine(e.StackTrace.GetTypeCode().ToString());

                        continue;
                    }
                }
                else
                {
                    //此行格式不正确
                    Console.WriteLine("This line is trashline\n");
                }
                
            }
            fileGenerator.Write(folderPath, fileName);
        }

        public void ExtractImage(int recordIndex, string guid, string fileName)
        {
            string imagePath = Program.PROJECT_PATH + Program.IMAGE_PATH + fileName;
            if (!Directory.Exists(imagePath))
                Directory.CreateDirectory(imagePath);
            
            string[] temp = fileText[recordIndex];
            
            for (int i = temp.Length-1, j=0; i >=0; i--, j++)
            {
                if (!temp[i].Equals(""))
                {
                    string extension = temp[i].Split('.').Last().ToLower();
                    //暂定只有这六种图片格式
                    if (extension.Equals("jpg") || extension.Equals("png") || extension.Equals("gif") ||
                        extension.Equals("jpeg") || extension.Equals("bmp") || extension.Equals("webp"))
                    {
                        try
                        {
                            imageDownloader.Download(temp[i], imagePath + '/' + guid +
                                                              string.Format("_{0}.{1}", j, extension));
                        }
                        catch (Exception e)
                        {
                            //偶尔会有图片404的情况
                            Console.WriteLine(string.Format("There is an error in file {0}, line {1}, block {2}",
                                fileName, recordIndex, i+1));
                            Console.WriteLine(string.Format("The error link is {0}", temp[i]));
                            Console.WriteLine(e.StackTrace.GetTypeCode().ToString());
                            
                            continue;
                        }
                        
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

    }
}