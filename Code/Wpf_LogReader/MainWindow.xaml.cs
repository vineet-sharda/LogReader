using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Wpf_LogReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileHandlerCollection files;
        private IFileReader fileReader;

        public MainWindow()
        {
            InitializeComponent();
            this.ClearAll();
        }

        private void btnAddFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Multiselect = true };
            if (openFileDialog.ShowDialog() != true) return;

            this.files = this.files == null ? new FileHandlerCollection() : this.files;
            foreach (var fileName in openFileDialog.FileNames)
            {
                this.files.Add(new FileHandler() { Id = fileName });
            }

            this.lstFiles.Items.Clear();
            foreach (var file in this.files.Files.OrderBy(f => f.Name))
            {
                this.lstFiles.Items.Add(file.Name);
            }

            this.btnClear.Width = double.NaN;
            this.btnClear.Width = double.NaN;
            this.btnProcess.Width = double.NaN;
            this.btnExport.Width = double.NaN;
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            if (this.files == null) return;
            this.fileReader = new FileReader();
            foreach (FileHandler file in this.files.Files)
            {
                this.fileReader.FileHandler = file;
                this.fileReader.LoadLogs();
            }

            this.lstLogGroupCollection.Items.Clear();
            foreach (var group in this.fileReader.Logs.LogGroups.OrderBy(g => g.Message))
            {
                this.lstLogGroupCollection.Items.Add(group.Message);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            this.ClearAll();
        }

        private void lstLogGroupCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.lstLogGroupCollection.SelectedValue == null) return;
            string groupName = this.lstLogGroupCollection.SelectedValue.ToString();
            this.lblGroup.Content = groupName;
            var group = this.fileReader.Logs.FirstOrDefault(groupName);

            this.grdLog.ItemsSource = group.LogEntries.Select(e => new View.MainWindow.LogEntryCollection() { Count = e.LogEntries.Count, Message = e.Message }).OrderByDescending(le => le.Count);
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            if (this.fileReader == null) return;

            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "CSV Files (*.csv)|*.csv",
                DefaultExt = "csv",
                AddExtension = true
            };
            if (saveFileDialog.ShowDialog() != true) return;

            using (StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName))
            {
                foreach (LogGroup group in this.fileReader.Logs.LogGroups.OrderBy(g => g.Message))
                {
                    streamWriter.WriteLine($",");
                    streamWriter.WriteLine($",{group.Message}");
                    streamWriter.WriteLine($"Count,Message");
                    foreach (var log in group.LogEntries.OrderByDescending(le => le.LogEntries.Count))
                    {
                        streamWriter.WriteLine($"{log.LogEntries.Count},{log.Message.Replace(",", ";")}");
                    }
                }
            }
        }

        private void ClearAll()
        {
            this.lblGroup.Content = "";
            this.grdLog.ItemsSource = null;
            this.lstLogGroupCollection.Items.Clear();
            this.lstFiles.Items.Clear();
            this.btnClear.Width = 0;
            this.btnProcess.Width = 0;
            this.btnExport.Width = 0;

            this.files = null;
            this.fileReader = null;
        }

        private void grdLog_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            LogGroup groupCollection = this.fileReader.Logs.FirstOrDefault(this.lstLogGroupCollection.SelectedValue.ToString());
            LogEntryCollection logs = groupCollection.LogEntries.FirstOrDefault(le => le.Message == ((View.MainWindow.LogEntryCollection)e.Row.Item).Message);
            winLogDetail logDetail = new winLogDetail() { Log = logs, Reader = this.fileReader };
            logDetail.Show();
            e.Cancel = true;
        }
    }
}

