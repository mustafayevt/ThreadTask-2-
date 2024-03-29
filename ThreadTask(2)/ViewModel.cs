﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThreadTask_2_.Commands;

namespace ThreadTask_2_
{
    public class ViewModel : INotifyPropertyChanged
    {

        public ViewModel()
        {
            if (File.Exists("Settings.json"))
            {
                AppSetting = JsonConvert.DeserializeObject<AppSetting>(File.ReadAllText("Settings.json"));
            }
            else
            {
                AppSetting = new AppSetting();
            }


            AllProcesses = new ObservableCollection<Process>(Process.GetProcesses());
            UpdateProcesses = new Task(delegate
            {
                while (true)
                {
                    if (Process.GetProcesses().Count() != AllProcesses.Count)
                    {
                        AllProcesses = new ObservableCollection<Process>(Process.GetProcesses());
                    }
                }
            });
            UpdateProcesses.Start();
            KillIgnoredApps = new Task(delegate
            {
                while (true)
                {
                    if (AppSetting.IsModerate)
                        try
                        {
                            foreach (var item in AppSetting.IgnoredApps)
                            {
                                foreach (var process in AllProcesses)
                                {
                                    if (process.ProcessName == item)
                                    {
                                        process.Kill();
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                }
            });
            KillIgnoredApps.Start();

            ReportCommand = new ReportCommand(this);
            AddNewIgnoredApp = new AddNewIgnoredApp(this);


        }
        public ReportCommand ReportCommand { get; set; }
        public AddNewIgnoredApp AddNewIgnoredApp { get; set; }
        private string newIgnoredApp;
        public string NewIgnoredApp { get => newIgnoredApp; set { newIgnoredApp = value; OnNotifyPropertyChanged(nameof(NewIgnoredApp)); } }
        public AppSetting AppSetting { get; set; }
        private ObservableCollection<Process> allProcesses;

        public ObservableCollection<Process> AllProcesses
        {
            get { return allProcesses; }
            set { allProcesses = value; OnNotifyPropertyChanged(nameof(AllProcesses)); }
        }
        public Task UpdateProcesses { get; set; }
        public Task KillIgnoredApps { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotifyPropertyChanged(string param)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(param));
        }
    }
}
