using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronizeEditValuesInEditForm_MVVM {
    class MainViewModel : ViewModelBase {
        public ObservableCollection<DataItem> Items { get; set; }

        public MainViewModel() {
            Items = new ObservableCollection<DataItem>();
            var random = new Random();
            for(var i = 0; i < 10; ++i) {
                Items.Add(new DataItem() { Amount = random.Next(1, 10), Price = random.Next(100, 1000) });
            }
        }
    }
}
