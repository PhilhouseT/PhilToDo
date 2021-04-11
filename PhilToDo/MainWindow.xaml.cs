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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace PhilToDo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TaskList taskList;

        public MainWindow()
        {
            InitializeComponent();

            if (!DataFileHandler.CheckDirectory() || !DataFileHandler.LoadTaskList(ref taskList))
            {
                Close(); 
            }

            DgTasks.ItemsSource = taskList.Tasks;

            if (taskList.Tasks.Count > 0)
            {
                Global.TopNumber = taskList.Tasks.Last().Number;
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            TaskDetails taskDetails = new(taskList);
            taskDetails.ShowDialog();
            ReloadGrid(false);
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DgTasks.SelectedIndex == -1)
            {
                MessageBox.Show("No task selected.", "Error", MessageBoxButton.OK);
                return;
            }

            Task task = (Task)DgTasks.SelectedItem;
            TaskDetails taskDetails = new(task, taskList);
            taskDetails.ShowDialog();
            ReloadGrid(false);
        }

        private void BtnCompleted_Click(object sender, RoutedEventArgs e)
        {
            if (DgTasks.SelectedIndex == -1)
            {
                MessageBox.Show("No task selected.", "Error", MessageBoxButton.OK);
                return;
            }

            Task task = (Task)DgTasks.SelectedItem;
            task.Completed = !task.Completed;
            taskList.SaveList();
            ReloadGrid(false);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DgTasks.SelectedIndex == -1)
            {
                MessageBox.Show("No task selected.", "Error", MessageBoxButton.OK);
                return;
            }
            else
            {
                taskList.DeleteTask((Task)DgTasks.SelectedItem);
                ReloadGrid(true);
            }
        }

        private void ReloadGrid(bool deleted)
        {
            int index = DgTasks.SelectedIndex;
            DgTasks.ItemsSource = null;
            DgTasks.ItemsSource = taskList.Tasks;
            DgTasks.IsReadOnly = true;
            if (!deleted)
            {
                DgTasks.SelectedIndex = index;
            }
        }

        //private void SaveTaskList()
        //{
        // TODO: Save to file
        //}
    }
}
