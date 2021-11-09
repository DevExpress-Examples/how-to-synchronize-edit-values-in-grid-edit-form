<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/393277465/21.2.2%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1037808)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# Data Grid for WPF - How to Process Related Cells in the Edit Form

This example illustrates how to process related cells in the Edit Form. The Edit Form contains information about goods. A user can change the `Price` value if the `CanEdit` checkbox is checked. `PositionValue` is the result of `Price` and `Amount` multiplication. 
The following code sample disables the Price editor depending on the `CanEdit` value and assigns the result of `Price` and `Amount` multiplication to the `PositionValue` editor.
Handle the RowEditStarting event to initialize values in editors when a user starts to edit the row. 


<!-- default file list -->

## Files to Look At

### Code-Behind
- [MainViewModel.xaml.cs](./CS/SynchronizeEditValuesInEditForm_CodeBehind/MainWindow.xaml.cs#L34-L55) ([MainViewModel.xaml.vb](./VB/SynchronizeEditValuesInEditForm_CodeBehind/MainWindow.xaml.vb#L42-L61))
- [MainWindow.xaml](./CS/SynchronizeEditValuesInEditForm_CodeBehind/MainWindow.xaml#L19) ([MainWindow.xaml](./VB/SynchronizeEditValuesInEditForm_CodeBehind/MainWindow.xaml#L19))

### MVVM Pattern
- [MainViewModel.cs](./CS/SynchronizeEditValuesInEditForm_MVVM/MainViewModel.cs#L38-L60) ([MainViewModel.vb](./Vb/SynchronizeEditValuesInEditForm_MVVM/MainViewModel.vb#L45-L65))
- [MainWindow.xaml](./CS/SynchronizeEditValuesInEditForm_MVVM/MainWindow.xaml#L22) ([MainWindow.xaml](./VB/SynchronizeEditValuesInEditForm_MVVM/MainWindow.xaml#L22))

<!-- default file list end -->

## Documentation

- [Edit Form](https://docs.devexpress.com/WPF/401667/controls-and-libraries/data-grid/data-editing-and-validation/modify-cell-values/edit-entire-row?v=21.2#edit-form)
- [RowEditStarting](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TableView.RowEditStarting) / [NodeEditStarting](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TreeListView.NodeEditStarting)
- [RowEditStartingCommand](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TableView.RowEditStartingCommand) / [NodeEditStartingCommand](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TreeListView.NodeEditStarting)

## More Examples
- [Data Grid for WPF - How to Specify Edit Form Settings](https://github.com/DevExpress-Examples/wpf-data-grid-specify-edit-form-settings)
- [Data Grid for WPF - How to Pause Data Updates in the Edit Form](https://github.com/DevExpress-Examples/wpf-data-grid-edit-form-pause-updates)
