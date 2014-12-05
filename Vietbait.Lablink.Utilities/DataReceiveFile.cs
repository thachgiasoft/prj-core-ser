using System.IO;

namespace Vietbait.Lablink.Utilities
{
    public class DataReceiveFile
    {
        #region "Attributies"

        #endregion

        #region "Properties"

        public string SourceFolder { get; set; }
        public string FullFilePath { get; set; }
        public string LogPath { get; set; }
        public string FileName { get; set; }

        #endregion

        #region "Contructor"

        public DataReceiveFile(string sourceFolder,string fullFilePath,string logPath)
        {
            SourceFolder = sourceFolder;
            FullFilePath = fullFilePath;
            FileName = Path.GetFileName(FullFilePath);
            LogPath = logPath;
        }

        #endregion
    }
}