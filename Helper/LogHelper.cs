/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2016/10/20 星期四
 * Time: 下午 4:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;

using log4net;
using log4net.Config;
using log4net.Appender;
using log4net.Layout;
using log4net.Core;
using log4net.Repository.Hierarchy;
using System.IO;

namespace Helper
{
    /// <summary>
    /// Description of AutoLog.
    /// </summary>
    public class LogHelper
    {
        private static readonly ILog loginfo = log4net.LogManager.GetLogger("loginfo");

        private static readonly ILog logdebug = log4net.LogManager.GetLogger("logdebug");

        private static readonly ILog logwarn = log4net.LogManager.GetLogger("logwarn");

        private static readonly ILog logerror = log4net.LogManager.GetLogger("logerror");

        private static readonly ILog logfatal = log4net.LogManager.GetLogger("logfatal");

        public static void Initialize(string config)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(config));
        }

        public static void Info(Object message)
        {
            loginfo.Info(message);
        }

        public static void Info(Object message, Exception exception)
        {
            loginfo.Info(message, exception);
        }

        public static void Debug(Object message)
        {
            logdebug.Debug(message);
        }

        public static void Debug(Object message, Exception exception)
        {
            logdebug.Debug(message, exception);
        }

        public static void Warning(Object message)
        {
            logwarn.Warn(message);
        }

        public static void Warning(Object message, Exception exception)
        {
            logwarn.Warn(message, exception);
        }

        public static void Error(Object message)
        {
            logerror.Error(message);
        }

        public static void Error(Object message, Exception exception)
        {
            logerror.Error(message, exception);
        }

        public static void Fatal(Object message)
        {
            logfatal.Fatal(message);
        }

        public static void Fatal(Object message, Exception exception)
        {
            logfatal.Fatal(message, exception);
        }
    }
}
