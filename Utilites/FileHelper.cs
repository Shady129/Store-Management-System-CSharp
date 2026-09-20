using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Utilites
{
    public class FileHelper
    {

        private static readonly Encoding Utf8NoBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);


        public static async Task Create(string path, string content)
        {

            var full = Normalize(path);
            EnsureDirectory(full);
            await File.WriteAllTextAsync(full, content ?? string.Empty, Utf8NoBom);
        }


        public static async Task Append(string path, string text, bool addNewLine = false)
        {
            var full = Normalize(path);
            EnsureDirectory(full);

            var payload = addNewLine ? (text ?? string.Empty) + Environment.NewLine
                                     : (text ?? string.Empty);


            await File.AppendAllTextAsync(full, payload, Utf8NoBom);

        }



        public static async Task Update(string path, string text, bool addNewLine = false)
        {


            var full = Normalize(path);
            EnsureDirectory(full);

            var payload = addNewLine ? (text ?? string.Empty) + Environment.NewLine
                                : (text ?? string.Empty);


            await File.WriteAllTextAsync(full, payload, Utf8NoBom);
        }



        public static async Task<string> ReadAll(string path)
        {
            var full = Normalize(path);

            if (!File.Exists(full))
            {

                EnsureDirectory(full);
                await File.WriteAllTextAsync(full, string.Empty, Utf8NoBom);
                return string.Empty;

            }


            return await File.ReadAllTextAsync(full, Utf8NoBom);
        }



        public static async Task<string[]> ReadAllLines(string path)
        {

            var full = Normalize(path);


            if(!File.Exists(full))
            {
                EnsureDirectory(full);
                await File.WriteAllTextAsync(full, string.Empty, Utf8NoBom);
                return new string[1];
            }


            return await File.ReadAllLinesAsync(full, Utf8NoBom);
        }



        public static async Task<bool> Delete(string path)
        {
            var full = Normalize(path);

            if (!File.Exists(full))
                return false;

            File.Delete(full);

            return true;
        }



        public static async Task<bool> TryCreate(string path, string content)
        {
            try
            {
                await Create(path, content);
                return true;
            }
            catch
            {
                return false;
            }
        }


        private static string Normalize(string path) =>
       Path.GetFullPath(path ?? throw new ArgumentNullException(nameof(path)));

        private static void EnsureDirectory(string fullFilePath)
        {
            var dir = Path.GetDirectoryName(fullFilePath);
            if (!string.IsNullOrEmpty(dir))
                Directory.CreateDirectory(dir);
        }

    }
}