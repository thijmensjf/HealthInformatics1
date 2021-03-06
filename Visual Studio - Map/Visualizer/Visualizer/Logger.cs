﻿// <copyright file="Logger.cs" company="HI1">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Visualizer
{
    using System;
    using System.IO;
    using System.Linq;
    using Visualizer.Models;
    using Visualizer.Utilities;

    /// <summary>
    /// The logger class is a singleton that manages logging.
    /// It creates two log files.
    /// A current file, this is for easy debugging in short amount of time.
    /// one backup file, this is for retrieving old data.
    /// The files are HTML files which we can use with CSS.
    /// </summary>
    public class Logger
    {
        #region Fields

        /// <summary>
        /// Colors used for marking the log levels
        /// </summary>
        private static readonly string Black = "black", Red = "red", Orange = "orange", Green = "green";

        /// <summary>
        /// Log levels
        /// </summary>
        private static readonly string DebugS = "DEBUG", InfoS = "INFO", ErrorS = "ERROR", WarningS = "WARNING";

        /// <summary>
        /// Amount of log files stored
        /// </summary>
        private static readonly int LogFiles = 101;

        /// <summary>
        /// Singleton instance
        /// </summary>
        private static readonly Logger Instance = new Logger();

        /// <summary>
        /// The relative path to store the log files
        /// </summary>
        private static string path;

        /// <summary>
        /// Writers to use
        /// </summary>
        private StreamWriter writerStore, writerCurrent;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="Logger"/> class from being created.
        /// </summary>
        private Logger()
        {
            path = PathGetter.GetLoggerPath();
            this.CreateFiles();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the Singleton instance of the Logger class.
        /// </summary>
        /// <returns> Singleton instance of the Logger </returns>
        public static Logger GetInstance()
        {
            return Instance;
        }

        /// <summary>
        /// Logs all the information messages
        /// Should be used for information purpose only
        /// </summary>
        /// <param name="message"> the message to log </param>
        public void Info(string message)
        {
            this.WriteLine(Black, InfoS, message);
        }

        /// <summary>
        /// Information log for the VR object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="message">The message.</param>

        public void Info(AbstractVRObject obj, string message)
        {
            this.Info(this.GetStartString(obj) + message);
        }

        /// <summary>
        /// Warning log for the VR object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="message">The message.</param>
        public void Warning(AbstractVRObject obj, string message)
        {
            this.Warning(this.GetStartString(obj) + message);
        }

        /// <summary>
        /// Logs all the debug messages
        /// Should be used for debug purpose only
        /// </summary>
        /// <param name="message"> the message to log</param>
        public void Debug(string message)
        {
            this.WriteLine(Green, DebugS, message);
        }

        /// <summary>
        /// Logs all the error messages
        /// Should be used for error purpose only
        /// </summary>
        /// <param name="message"> the message to log</param>
        public void Error(string message)
        {
            this.WriteLine(Red, ErrorS, message);
        }

        /// <summary>
        /// Logs all the warning messages
        /// Should be used for warning purpose only
        /// </summary>
        /// <param name="message"> the message to log</param>
        public void Warning(string message)
        {
            this.WriteLine(Orange, WarningS, message);
        }

        /// <summary>
        /// Gets the start string for a log sentence.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>string with the standard sentence</returns>
        private string GetStartString(AbstractVRObject obj)
        {
            return "Object " + obj.GetType().Name + " with id " + obj.Id + " ";
        }

        /// <summary>
        /// Writes the line to both writes and console
        /// If those are active
        /// </summary>
        /// <param name="color">The color of the line</param>
        /// <param name="level">The level of the line</param>
        /// <param name="message">the message of the line</param>
        private void WriteLine(string color, string level, string message)
        {
            string line = this.GetTime() + "<span style='color:" + color + ";'><b> " + level + "</b></span> " + message + "<br/>";
            this.WriteLine(this.writerCurrent, line);
            this.WriteLine(this.writerStore, line);
            Console.WriteLine(this.GetTime() + " " + level + " " + message);
        }

        /// <summary>
        /// Writes the line with the given writer
        /// </summary>
        /// <param name="writer"> writer to be used</param>
        /// <param name="message"> message to be written</param>
        private void WriteLine(StreamWriter writer, string message)
        {
            writer.WriteLine(message);
            writer.Flush();
        }

        /// <summary>
        /// Creates the writers and files.
        /// One current file and one file with the date.
        /// When there are more than logFiles (101) files the oldest files are deleted.
        /// </summary>
        private void CreateFiles()
        {
            Directory.CreateDirectory(path);
            string time = DateTime.Now.ToString("MM-dd-yy HH_mm_ss");
            this.writerCurrent = new StreamWriter(path + @"\current.html");
            this.WriteStart(this.writerCurrent);
            this.writerStore = new StreamWriter(path + @"\" + time + ".html");
            this.WriteStart(this.writerStore);
            foreach (var file in new DirectoryInfo(path).GetFiles().OrderByDescending(x => x.LastWriteTime).Skip(LogFiles))
            {
                file.Delete();
            }
        }

        /// <summary>
        /// Writes the header of the log file with the defined writer.
        /// </summary>
        /// <param name="writer"> writer to be used </param>
        private void WriteStart(StreamWriter writer)
        {
            writer.WriteLine("Log file of the run on " + this.GetTime() + "</br>");
            writer.WriteLine("<span style='color:red;'><b>red</b></span> text stands for ERROR</br>");
            writer.WriteLine("<span style='color:orange;'><b>orange</b></span> text stands for WARNING</br>");
            writer.WriteLine("<span style='color:green;'><b>green</b></span> text stands for DEBUG</br>");
            writer.WriteLine("<span style='color:black;'><b>black</b></span> text stands for INFO</br>");
            writer.Flush();
        }

        /// <summary>
        /// Get the current time in the preferred format
        /// </summary>
        /// <returns> String with the current time</returns>
        private string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        #endregion Methods
    }
}