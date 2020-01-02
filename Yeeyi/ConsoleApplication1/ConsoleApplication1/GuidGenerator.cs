using System;

namespace ConsoleApplication1
{
    /// <summary>
    /// 生成guid的类
    /// </summary>

    public class GuidGenerator
    {
        private Guid _guid;

        public string GetNewGuid()
        {
            _guid = Guid.NewGuid();
            return _guid.ToString();
        }
    }
}