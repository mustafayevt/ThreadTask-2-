using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ThreadTask_2_.Commands
{
    public class AddNewIgnoredApp : ICommand
    {
        public event EventHandler CanExecuteChanged;
        ViewModel Model;

        public AddNewIgnoredApp(ViewModel model)
        {
            Model = model;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (Model.NewIgnoredApp != "") ;
            Model.AppSetting.IgnoredApps.Add(Model.NewIgnoredApp);
            Model.NewIgnoredApp = "";
            File.WriteAllText("Settings.json", JsonConvert.SerializeObject(Model.AppSetting));
        }
    }
}
