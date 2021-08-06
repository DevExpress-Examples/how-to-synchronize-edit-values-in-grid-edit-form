using DevExpress.Mvvm;

namespace SynchronizeEditValuesInEditForm {
    class DataItem : BindableBase {
        public int Amount { get; set; }

        public int Price { get; set; }

        public int PositionValue { get => Price * Amount; }
    }
}
