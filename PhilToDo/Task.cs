using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilToDo
{
    public class Task
    {
        /*private string title;
        private string description;
        private DateTime? dueDate;
        private bool completed;

        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public DateTime? DueDate { get => dueDate; set => dueDate = value; }
        public bool Completed { get => completed; set => completed = value; }*/

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
