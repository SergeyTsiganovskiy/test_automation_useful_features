using System.Linq;
using log4net;

namespace test_automation_useful_features.Helpers
{
    public class LogUtil
    {
        public static ILog log = LogManager.GetLogger("","CommonLog");

        /// <summary>
        /// Create the file name and sets the path. default path is bin\debug
        /// </summary>
        /// <param name="appenderName"></param>
        /// <param name="newFilename"></param>
        public static void CreateLogFile(string appenderName, string newFilename, bool minLock = false)
        {
            log4net.Repository.ILoggerRepository repository = LogManager.GetRepository("");
            foreach (log4net.Appender.IAppender appender in repository.GetAppenders())
            {
                if (appender.Name.CompareTo(appenderName) == 0 && appender is log4net.Appender.FileAppender)
                {
                    log4net.Appender.FileAppender fileAppender = (log4net.Appender.FileAppender)appender;
                    //It was appending existing path with new log file path
                    fileAppender.File = newFilename; // fileAppender.File = System.IO.Path.Combine(fileAppender.File, newFilename);
                    if(minLock) //Used only by DMT
                    {
                        fileAppender.LockingModel = new log4net.Appender.FileAppender.MinimalLock();
                    }
                    fileAppender.ActivateOptions();
                }
            }
        }

        /// <summary>
        /// Create the file name and sets the path for CommonLogAppender. default path is bin\debug
        /// </summary>
        /// <param name="newFilename"></param>
        public static void CreateLogFile(string newFilename)
        {
            log4net.Repository.ILoggerRepository repository = LogManager.GetRepository("");
            var appender = repository.GetAppenders()
                .First(i => i is log4net.Appender.FileAppender && i.Name.Equals("CommonLogAppender"));

            var fileAppender = (log4net.Appender.FileAppender)appender;
            fileAppender.File = newFilename;
            fileAppender.ActivateOptions();
        }

        public static void WriteDebug(string customMessage)
        {
            log.Debug(customMessage);
            //  Console.WriteLine(string.Format("{0} :{1}", DateTime.Now.ToString("HH:mm:ss.fff"), customMessage));
        }
    }
}
