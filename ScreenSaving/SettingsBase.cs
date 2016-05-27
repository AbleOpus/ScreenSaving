﻿using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace ScreenSaving
{
    // *******************************************************************************
    // Use the SettingsBase class for user settings, as ApplicationSettingsBase does
    // not behave in a desirable manner with screen savers.
    // *******************************************************************************

    /// <summary>
    /// Provides base functionality for an user/application settings class.
    /// The implementation provides automated binary serialization to the users AppData .
    /// </summary>
    /// <typeparam name="T">The type which implements this class and is to be used as a singleton.</typeparam>
    [Serializable]
    public abstract class SettingsBase<T> where T : SettingsBase<T>, new()
    {
        /// <summary>
        /// Gets the only instance of this class.
        /// </summary>
        public static T Default { get; private set; }

        static SettingsBase()
        {
            Load();
        }

        private static void Load()
        {
            Default = new T(); // We have to instantiate to get the SavePath
            // Instance.ExploreDirectory();

            // If file exist, load from it, otherwise set this instance properties to default
            if (File.Exists(Default.GetSavePath()))
            {
                try
                {
                    using (FileStream stream = File.OpenRead(Default.GetSavePath()))
                        Default = (T)(new BinaryFormatter().Deserialize(stream));
                }
                catch (Exception ex)
                {
                    Default.OnLoadFailed(ex);
                    Default.Reset();
                }
            }
            else Default.Reset();
        }

        /// <summary>
        /// Loads settings from file into the default instance. Use this
        /// when the contents of the settings file changes in an untraditional manner.
        /// </summary>
        public void Reload()
        {
            Load();
        }

        /// <summary>
        /// Deletes the settings file and loads default values.
        /// </summary>
        public void Clear()
        {
            string fileName = GetSavePath();

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                Reset();
            }
        }

        /// <summary>
        /// Provides implementation for restoring settings to their default values.
        /// </summary>
        public abstract void Reset();

        /// <summary>
        /// Called when the settings file could not be loaded.
        /// </summary>
        /// <param name="ex">The <see cref="Exception"/> that causes the load failure.</param>
        protected virtual void OnLoadFailed(Exception ex) { }

        /// <summary>
        /// Gets the save path.
        /// </summary>
        public virtual string GetSavePath()
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            dir = Path.Combine(dir, Application.ProductName);
            int major = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.Major;
            return Path.Combine(dir, $@"UserSettings v{major}.dat");
        }

        /// <summary>
        /// Gets the save location of these settings.
        /// </summary>
        public string GetSaveDirectory()
        {
            return Path.GetDirectoryName(GetSavePath());
        }

        /// <summary>
        /// Opens the directory in which the settings are saved in windows explorer.
        /// </summary>
        public void ExploreDirectory()
        {
            string dir = Path.GetDirectoryName(GetSavePath());

            if (Directory.Exists(dir))
            {
                Process.Start(dir);
            }
        }

        /// <summary>
        /// Saves the settings (to a place specified by the DataSave property). If the 
        /// directory for the settings does not exist, then it is created.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Raises when serialization has failed.</exception>
        /// <exception cref="System.IO.EndOfStreamException">Raises when the settings file is corrupt.</exception>
        /// <exception cref="System.UnauthorizedAccessException"></exception>
        public void Save()
        {
            string fileName = GetSavePath();
            string directory = Path.GetDirectoryName(fileName);

            // The directory might not exist already
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            using (var fileStreamtream = File.OpenWrite(fileName))
                new BinaryFormatter().Serialize(fileStreamtream, this);
        }
    }
}