using log4net;
using log4net.Config;
using System;
using System.IO;

namespace Common
{
    public class LogHelp
    {
        private static readonly object locker = new object();
        private static ILog logerror;
        private static ILog loggerinfo;
        private static ILog loggermit;
        static LogHelp()
        {
            if (logerror == null)
            {
                lock (locker)
                {
                    if (logerror==null)
                    {
                        log4net.Repository.ILoggerRepository repository = log4net.LogManager.CreateRepository("NETCoreRepository");
                        var fileInfo = new FileInfo("Log4Net.config");
                        XmlConfigurator.Configure(repository, fileInfo);
                        BasicConfigurator.Configure(repository);
                       
                        logerror = LogManager.GetLogger(repository.Name, "logerror");
                        loggermit = LogManager.GetLogger(repository.Name, "logmonitor");
                        loggerinfo = LogManager.GetLogger(repository.Name, "loginfo");
                    }
                }
            }
        }
        /// <summary>
        /// 普通日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Info(string message, Exception exception = null)
        {
            if (exception == null)
                loggerinfo.Info(message);
            else
                loggerinfo.Info(message, exception);
        }


        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Error(string message, Exception exception = null)
        {
            if (exception == null)
                logerror.Error(message);
            else
                logerror.Error(message, exception);
        }

        /// <summary>
        ///监控日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Monitor(string message, Exception exception = null)
        {
            if (exception == null)
                loggermit.Info(message);
            else
                loggermit.Info(message, exception);
        }
    }
}
