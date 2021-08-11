using DevExpress.Mvvm;

namespace SynchronizeEditValuesInEditForm_MVVM {
    class DataItem : BindableBase {
        public int Amount { get; set; }

        public int Price { get; set; }

        public int PositionValue { get => Price * Amount; }
    }
}
