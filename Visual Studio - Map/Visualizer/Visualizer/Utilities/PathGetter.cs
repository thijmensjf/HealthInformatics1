﻿// <copyright file="PathGetter.cs" company="HI1">
//     Copyright ©  2016
// </copyright>
// <summary>Path Getter class</summary>
// ***********************************************************************
namespace Visualizer.Utilities
{
    /// <summary>
    /// Class PathGetter.
    /// </summary>
    public class PathGetter
    {
        #region Fields

        /// <summary>
        /// The folder depth
        /// </summary>
        private static int folderDepth = 4;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Gets the logger path.
        /// </summary>
        /// <returns>path to the logger map</returns>
        public static string GetLoggerPath()
        {
            return GetAtbolutePath() + @"\logger\";
        }

        /// <summary>
        /// Gets the resource path.
        /// </summary>
        /// <returns>path to the recourse map</returns>
        public static string GetResourcePath()
        {
            return GetAtbolutePath() + @"\Resources\";
        }

        /// <summary>
        /// Gets the logger path.
        /// </summary>
        /// <returns>the path to the root folder</returns>
        private static string GetAtbolutePath()
        {
            string path = System.IO.Path.GetDirectoryName(
            System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string newPath = string.Empty;
            string[] paths = path.Split('\\');
            for (int i = 1; i < paths.Length - PathGetter.folderDepth; i++)
            {
                newPath = newPath + "\\" + paths[i];
            }

            return newPath = newPath.Substring(1);
        }

        #endregion Methods
    }
}