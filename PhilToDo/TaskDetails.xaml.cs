using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PhilToDo
{
    /// <summary>
    /// Interaction logic for TaskDetails.xaml
    /// </summary>
    public partial class TaskDetails : Window
    {
        private Task task;
        private TaskList taskList;

        public TaskDetails(TaskList list)
        {
            task = null;
            taskList = list;
            InitializeComponent();
            Setup();
        }

        public TaskDetails(Task existingTask, TaskList list)
        {
            task = existingTask;
            taskList = list;
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            if (task == null)
            {
                DtDueDate.SelectedDate = DateTime.Now;
                DtDueDate.IsEnabled = (bool)CbUseDueDate.IsChecked;
            } 
            else
            {
                if (task.DueDate == null)
                {
                    DtDueDate.SelectedDate = DateTime.Now;
                    DtDueDate.IsEnabled = false;
                    CbUseDueDate.IsChecked = false;
                }
                else
                {
                    DtDueDate.SelectedDate = task.DueDate;
                    CbUseDueDate.IsChecked = true;
                }

                txtTitle.Text = task.Title;
                txtDescription.Text = task.Description;
                CbCompleted.IsChecked = task.Completed;
            }
        }

        private void BtnSaveAndClose_Click(object sender, RoutedEventArgs e)
        {
            bool success = SaveChanges();

            if (success)
            {
                taskList.SaveList();
                Close();
            }
        }

        private bool SaveChanges()
        {
            string title = txtTitle.Text;

            if (title.Trim() == "")
            {
                MessageBox.Show("Task title cannot be blank.", Global.errorText, MessageBoxButton.OK);
                return false;
            }

            string description = txtDescription.Text;
            DateTime? dueDate = (DtDueDate.IsEnabled ? DtDueDate.SelectedDate : null);
            bool completed = ((CbCompleted.IsChecked == null) ? false : (bool)CbCompleted.IsChecked);

            if (task == null)
            {
                task = new(++Global.TopNumber, title, description, dueDate, completed);
                taskList.AddTask(task);
            }
            else
            {
                task.Populate(task.Number, title, description, dueDate, completed);
            }

            return true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Unsaved changes will be lost. Continue?", "Confirm cancel", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void CbUseDueDate_Click(object sender, RoutedEventArgs e)
        {
            DtDueDate.IsEnabled = (bool)CbUseDueDate.IsChecked;
        }
    }
}
