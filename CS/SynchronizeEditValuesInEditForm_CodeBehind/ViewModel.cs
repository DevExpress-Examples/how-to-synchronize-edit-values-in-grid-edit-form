using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SynchronizeEditValuesInEditForm_CodeBehind {
    class ViewModel {
        public ObservableCollection<DataItem> Items { get; set; }

        public ViewModel() {
            Items = new ObservableCollection<DataItem>();
            var random = new Random();
            for (var i = 0; i < 10; ++i) {
                Items.Add(new DataItem() { Amount = random.Next(1, 10), Price = random.Next(100, 1000)});
            }
        }
    }
}
