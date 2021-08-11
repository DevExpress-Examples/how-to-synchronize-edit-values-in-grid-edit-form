using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Xpf;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronizeEditValuesInEditForm_MVVM {
    public class DataItem {
        public int Amount { get; set; }

        public int Price { get; set; }

        public int PositionValue { get => Price * Amount; }

        public DataItem(Random random) {
            Amount = random.Next(1, 10);
            Price = random.Next(100, 1000);
        }
    }
    class MainViewModel : ViewModelBase {
        public ObservableCollection<DataItem> Items { get; }

        public MainViewModel() {
            var random = new Random();
            Items = new ObservableCollection<DataItem>(Enumerable.Range(0, 10).Select(i => new DataItem(random)));
        }

        [Command]
        public void SynchronizeValues(CellValueChangedArgs e) {

        }
    }
}
