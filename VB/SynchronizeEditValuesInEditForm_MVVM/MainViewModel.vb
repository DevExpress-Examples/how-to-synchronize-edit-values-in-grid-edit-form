Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.Xpf
Imports DevExpress.Xpf.Grid
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
            If editFormArgs Is Nothing OrElse args.FieldName IsNot NameOf(DataItem.Price) AndAlso args.FieldName IsNot NameOf(DataItem.CanEdit) Then
                Return
            End If

            If args.FieldName Is NameOf(DataItem.CanEdit) Then
                editFormArgs.CellEditors.FirstOrDefault(Function(x) x.FieldName Is "Price").[ReadOnly] = Not Boolean.Parse(args.Value.ToString())
                Return
            End If

            Dim positionValueData = editFormArgs.CellEditors.First(Function(d) d.FieldName Is NameOf(DataItem.PositionValue))
            Dim amountData = editFormArgs.CellEditors.First(Function(d) d.FieldName Is NameOf(DataItem.Amount))
            Dim price As Integer = 0
            Call Integer.TryParse(CStr(args.Value), price)
            positionValueData.Value = CInt(amountData.Value) * price
        End Sub

        <Command>
        Public Sub InitializeEditing(ByVal args As RowEditStartingArgs)
            args.CellEditors.FirstOrDefault(Function(x) Object.Equals(x.FieldName, "Price")).[ReadOnly] = Not CBool(args.CellEditors.FirstOrDefault(Function(x) Object.Equals(x.FieldName, "CanEdit")).Value)
        End Sub
    End Class
End Namespace
