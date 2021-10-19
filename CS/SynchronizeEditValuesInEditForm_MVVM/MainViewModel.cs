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

        public bool CanEdit { get; set; } = true;

        public DataItem(Random random) {
            Amount = random.Next(1, 10);
            Price = random.Next(100, 1000);
        }
    }
    class MainViewModel : ViewModelBase {
        public ObservableCollection<DataItem> Items { get; }

        public MainViewModel() {
            Items = new ObservableCollection<DataItem>(GetData(10));
        }

        static IEnumerable<DataItem> GetData(int amount) {
            var random = new Random();
            return Enumerable.Range(0, amount).Select(i => new DataItem(random));
        }

        [Command]
        public void SynchronizeValues(CellValueChangedArgs args) {
            var editFormArgs = (CellValueChangedInEditFormArgs)args;
            if(editFormArgs == null || (args.FieldName != nameof(DataItem.Price) && args.FieldName != nameof(DataItem.CanEdit))) {
                return;
            }

            if(args.FieldName == nameof(DataItem.CanEdit)) {
                editFormArgs.CellEditors.FirstOrDefault(x => x.FieldName == "Price").ReadOnly = !bool.Parse(args.Value.ToString());
                return;
            }

            var positionValueData = editFormArgs.CellEditors.First(d => d.FieldName == nameof(DataItem.PositionValue));
            var amountData = editFormArgs.CellEditors.First(d => d.FieldName == nameof(DataItem.Amount));

            int price = 0;

            int.TryParse((string)args.Value, out price);
            positionValueData.Value = (int)amountData.Value * price;
        }

        [Command]
        public void InitializeEditing(RowEditStartingArgs args) {
            args.CellEditors.FirstOrDefault(x => string.Equals(x.FieldName, "Price")).ReadOnly = !(bool)args.CellEditors.FirstOrDefault(x => string.Equals(x.FieldName, "CanEdit")).Value;
        }
    }
}
