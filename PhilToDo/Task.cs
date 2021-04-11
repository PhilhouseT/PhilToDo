using System;

namespace PhilToDo
{
    public class Task
    {
        public int Number { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool Completed { get; set; }

        public Task()
            => Populate(0, "", "", null, false);

        public Task(int number,
                    string title,
                    string description,
                    DateTime? dueDate,
                    bool completed)
            => Populate(number, title, description, dueDate, completed);

        public void Populate(int number,
                             string title,
                             string description,
                             DateTime? dueDate,
                             bool completed)
        {
            Number = number;
            Title = title;
            Description = description;
            DueDate = dueDate;
            Completed = completed;
        }
    }
}
