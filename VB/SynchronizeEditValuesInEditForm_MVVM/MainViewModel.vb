Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.Xpf
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq

Namespace SynchronizeEditValuesInEditForm_MVVM

    Public Class DataItem

        Public Property Amount As Integer

        Public Property Price As Integer

        Public ReadOnly Property PositionValue As Integer
            Get
                Return Price * Amount
            End Get
        End Property

        Public Property CanEdit As Boolean = True

        Public Sub New(ByVal random As Random)
            Amount = random.Next(1, 10)
            Price = random.Next(100, 1000)
        End Sub
    End Class

    Friend Class MainViewModel
        Inherits ViewModelBase

        Public ReadOnly Property Items As ObservableCollection(Of DataItem)

        Public Sub New()
            Items = New ObservableCollection(Of DataItem)(GetData(10))
        End Sub

        Private Shared Function GetData(ByVal amount As Integer) As IEnumerable(Of DataItem)
            Dim random = New Random()
            Return Enumerable.Range(0, amount).[Select](Function(i) New DataItem(random))
        End Function

        <Command>
        Public Sub SynchronizeValues(ByVal args As CellValueChangedArgs)
            Dim editFormArgs = CType(args, CellValueChangedInEditFormArgs)
            If editFormArgs Is Nothing Then
                Return
            End If

            If Equals(args.FieldName, NameOf(DataItem.CanEdit)) Then
                Dim priceData = editFormArgs.CellEditors.FirstOrDefault(Function(x) Equals(x.FieldName, NameOf(DataItem.Price)))
                priceData.[ReadOnly] = Not Boolean.Parse(args.Value.ToString())
                Return
            End If

            If Equals(args.FieldName, NameOf(DataItem.Price)) Then
                Dim positionValueData = editFormArgs.CellEditors.First(Function(d) Equals(d.FieldName, NameOf(DataItem.PositionValue)))
                Dim amountData = editFormArgs.CellEditors.First(Function(d) Equals(d.FieldName, NameOf(DataItem.Amount)))
                Dim price As Integer = 0
                Call Integer.TryParse(CStr(args.Value), price)
                positionValueData.Value = CInt(amountData.Value) * price
            End If
        End Sub

        <Command>
        Public Sub InitializeEditing(ByVal args As RowEditStartingArgs)
            Dim priceData = args.CellEditors.FirstOrDefault(Function(x) Equals(x.FieldName, NameOf(DataItem.Price)))
            Dim canEditData = args.CellEditors.FirstOrDefault(Function(x) Equals(x.FieldName, NameOf(DataItem.CanEdit)))
            priceData.[ReadOnly] = Not CBool(canEditData.Value)
        End Sub
    End Class
End Namespace
