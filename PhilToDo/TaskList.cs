using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace PhilToDo
{
    [XmlRoot]
    public class TaskList
    {
        public List<Task> Tasks { get; set; }

        public TaskList()
            => Tasks = new List<Task>();

        public void AddTask(Task task)
        {
            Tasks.Add(task);
        }

        public void DeleteTask(Task task)
        {
            Tasks.Remove(task);
            SaveList();
        }

        public void SaveList()
        {
            XmlSerializer serializer = new(this.GetType());
            StreamWriter streamWriter = new(Global.fileLocation);
            serializer.Serialize(streamWriter, this);
            streamWriter.Close();
        }
    }
}
