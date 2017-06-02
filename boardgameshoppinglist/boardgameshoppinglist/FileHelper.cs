﻿using System;
using System.IO;

namespace boardgameshoppinglist
{
    public static class FileHelper
    {
        public static string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}