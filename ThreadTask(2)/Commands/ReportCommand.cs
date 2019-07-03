using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ThreadTask_2_.Commands
{
    public class ReportCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        ViewModel Model;

        public ReportCommand(ViewModel model)
        {
            Model = model;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Task Report = new Task(delegate
            {
                MessageBox.Show("report will be ready in a minute");
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var item in Model.AllProcesses)
                {
                    try
                    {
                        stringBuilder.AppendLine($"{item.ProcessName} - {item.StartTime.ToString()}");

                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "Report";
                dlg.DefaultExt = ".text";
                dlg.Filter = "Text documents (.txt)|*.txt";

                Nullable<bool> result = dlg.ShowDialog();

                if (result == true)
                {
                    File.WriteAllText(dlg.FileName, stringBuilder.ToString());
                }
            });
            Report.Start();
        }
    }
}
