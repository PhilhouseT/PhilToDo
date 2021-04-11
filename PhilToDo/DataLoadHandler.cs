using System;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace PhilToDo
{
    public static class DataFileHandler
    {
        // Check that the target directory for the data file exists. Create it if not.
        public static bool CheckDirectory()
        {
            try
            {
                DirectoryInfo di = Directory.CreateDirectory(Global.fileDirectory);
            }
            catch (Exception e)
            {
                String message = String.Format("Error creating save file directory: {0} - {1}. Application will close.",
                                               e.ToString(),
                                               Global.fileDirectory);
                MessageBox.Show(message, "Error", MessageBoxButton.OK);
                return false;
            }

            return true;
        }

        public static bool LoadTaskList(ref TaskList taskList)
        {
            bool success = true;
            taskList = new();

            if (File.Exists(Global.fileLocation))
            {
                XmlSerializer serializer = new XmlSerializer(taskList.GetType());
                StreamReader streamReader = new(Global.fileLocation);
                try
                {
                    taskList = (TaskList)serializer.Deserialize(streamReader);
                    streamReader.Close();
                }
                catch (System.InvalidOperationException e)
                {
                    streamReader.Close();
                    success = HandleLoadError(e);
                }
            }

            return success;
        }

        // Handles an exception having been thrown in loading from the data file.
        private static bool HandleLoadError(Exception e1)
        {
            string message = String.Format("Error importing existing XML data: {0}\nDelete file? This will erase all existing data.", e1.Message);
            MessageBoxResult result = MessageBox.Show(message, Global.errorText, MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    File.Delete(Global.fileLocation);
                }
                catch (Exception e2)
                {
                    MessageBox.Show(String.Format("Error deleting file: {0}", e2.Message), Global.errorText, MessageBoxButton.OK);
                    return false;
                }

                return true;
            }
            else
            {
                message = String.Format("You will need to attempt to repair the XML file manually: {0}", Global.fileLocation);
                MessageBox.Show(message, Global.errorText, MessageBoxButton.OK);
                return false;
            }
        }
    }
}
