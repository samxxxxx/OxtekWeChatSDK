using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OxetekWeChatSDK.Utils
{
    public class FileUtil
    {
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="data"></param>
        /// <param name="savePath"></param>
        /// <returns></returns>
        public static async Task WriteFile(byte[] data, string savePath)
        {
            var path = Path.GetDirectoryName(savePath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (var fs = new FileStream(savePath, FileMode.Create))
            {
                await fs.WriteAsync(data, 0, data.Length);
                await fs.FlushAsync();
                fs.Close();
            }
        }
    }
}
