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

namespace Wpf_LogReader
{
    /// <summary>
    /// Interaction logic for winLogDetail.xaml
    /// </summary>
    public partial class winLogDetail : Window
    {
        public winLogDetail()
        {
            InitializeComponent();
        }

        private LogEntryCollection logEntryCollection;
        public LogEntryCollection Log
        {
            get { return logEntryCollection; }
            set
            {
                logEntryCollection = value;
                this.Title = value.Message;
                this.lstLogEntry.Items.Clear();
                foreach (LogEntry log in value.LogEntries)
                {
                    this.lstLogEntry.Items.Add($"{log.TimeStamp:G} ({log.LineNumber}) {log.FileId}");
                }

            }
        }

        public IFileReader Reader { get; set; }

        private void lstLogEntry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string[] selection = this.lstLogEntry.SelectedItem.ToString().Split("(")[1].Split(") ");
            this.txtLog.Text = this.Reader.GetFullLog(selection[1], int.Parse(selection[0]));
        }
    }
}
