using System.Linq;
using System.Windows;
using System.Windows.Input;

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

            EditItem();
        }

        private void DgTasks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = DgTasks.SelectedIndex;

            if (index != -1)
            {
                EditItem();
            }
        }

        private void EditItem()
        {
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

        // Reload the display grid after data has changed.
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
    }
}
