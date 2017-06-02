using System;
using System.IO;

namespace boardgameshoppinglist
{
    public static class DatabaseFilePathRetriever
    {
        public static string GetPath()
        {
            const string filename = "BgSlSQLite.db3";
            var documentspath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentspath, filename);
            return path;
        }
    }
}