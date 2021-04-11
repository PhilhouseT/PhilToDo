
namespace PhilToDo
{
    // Just contains some values useful for global access.
    public static class Global
    {
        public const string fileDirectory = "C:\\PhilToDo\\";
        public const string fileLocation = fileDirectory + "taskList.xml";
        public const string errorText = "Error";
        public static int TopNumber { get; set; }
    }
}
