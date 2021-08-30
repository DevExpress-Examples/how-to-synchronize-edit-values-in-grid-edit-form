using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.EditForm;
using System;
using System.Linq;
using System.Windows;

namespace SynchronizeEditValuesInEditForm_CodeBehind {
    public class DataItem {
        public int Amount { get; set; }

        public int Price { get; set; }

        public int PositionValue { get => Price * Amount; }

        public DataItem(Random random) {
            Amount = random.Next(1, 10);
            Price = random.Next(100, 1000);
        }
    }

    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            var random = new Random();
            grid.ItemsSource = Enumerable.Range(0, 10).Select(i => new DataItem(random)).ToList();
        }

        void OnEditFormCellValueChanging(object sender, CellValueChangedEventArgs e) {
            CellValueChangedInEditFormEventArgs editFormArgs = e as CellValueChangedInEditFormEventArgs;
            if(editFormArgs == null || editFormArgs.Cell.Property != nameof(DataItem.Price)) {
                return;
            }

            var positionValueData = editFormArgs.CellEditors.First(d => d.FieldName == nameof(DataItem.PositionValue));
            var amountData = editFormArgs.CellEditors.First(d => d.FieldName == nameof(DataItem.Amount));

            var amount = (int)amountData.Value;
            int price = 0;
            var stringPrice = (string)e.Value;

            if(int.TryParse(stringPrice, out price)) {
                positionValueData.Value = amount * price;
            }
            if(string.IsNullOrEmpty(stringPrice)) {
                positionValueData.Value = 0;
            }
        }
    }
}
