using System;
using System.IO;
using System.Net;

namespace ConsoleApplication1
{
    /// <summary>
    /// 利用Google API, 通过地址得到坐标的类
    /// </summary>
    
    class Coordinate
    {
        private const string GOOGLE_API = "AIzaSyDzVZIjleq2tN2SshLIWkfhXaKNfKiP8cw";

        public string[] GetCoordinatesFromAddress(string _address)
        {
            string[] coordinates = new string[2];
            string url =
            string.Format("https://maps.googleapis.com/maps/api/geocode/json?key={0}&address={1}", GOOGLE_API, _address);

            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            StreamReader reader = new StreamReader(data);

            string responseFromServer = reader.ReadToEnd();

            response.Close();

            coordinates[0] = GetLatitudeFromJson(responseFromServer);
            coordinates[1] = GetLongitudeFromJson(responseFromServer);
            return coordinates;
        }

        private static string GetLatitudeFromJson(string jsonText)
        {
            return jsonText.Split(new string[] { "\"location\"" }, StringSplitOptions.None)[1]
                .Split(new string[] { "\"lat\" : " }, StringSplitOptions.None)[1]
                .Split(',')[0];
        }
        private static string GetLongitudeFromJson(string jsonText)
        {
            return jsonText.Split(new string[] { "\"location\"" }, StringSplitOptions.None)[1]
                .Split(new string[] { "\"lng\" : " }, StringSplitOptions.None)[1]
                .Split(' ')[0];
        }
    }
}
