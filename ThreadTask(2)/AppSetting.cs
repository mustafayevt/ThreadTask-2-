using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTask_2_
{
    public class AppSetting
    {
        public ObservableCollection<string> IgnoredApps { get; set; } = new ObservableCollection<string>();
        public bool CanModerate { get; set; }
    }
}
