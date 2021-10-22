Imports DevExpress.Xpf.Grid
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows

Namespace SynchronizeEditValuesInEditForm_CodeBehind

    Public Class DataItem

        Public Property Amount As Integer

        Public Property Price As Integer

        Public Property CanEdit As Boolean = True

        Public ReadOnly Property PositionValue As Integer
            Get
                Return Price * Amount
            End Get
        End Property

        Public Sub New(ByVal random As Random)
            Amount = random.Next(1, 10)
            Price = random.Next(100, 1000)
        End Sub
    End Class

    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            InitializeComponent()
            grid.ItemsSource = GetData(10).ToList()
        End Sub

        Private Shared Function GetData(ByVal amount As Integer) As IEnumerable(Of DataItem)
            Dim random = New Random()
            Return Enumerable.Range(0, amount).[Select](Function(i) New DataItem(random))
        End Function

        Private Sub OnEditFormCellValueChanging(ByVal sender As Object, ByVal e As CellValueChangedEventArgs)
            Dim editFormArgs As CellValueChangedInEditFormEventArgs = TryCast(e, CellValueChangedInEditFormEventArgs)
            If editFormArgs Is Nothing Then
                Return
            End If

            If Equals(e.Cell.[Property], NameOf(DataItem.CanEdit)) Then
                Dim priceData = editFormArgs.CellEditors.FirstOrDefault(Function(x) Equals(x.FieldName, NameOf(DataItem.Price)))
                priceData.[ReadOnly] = Not Boolean.Parse(e.Cell.Value.ToString())
                Return
            End If

            If Equals(e.Cell.[Property], NameOf(DataItem.Price)) Then
                Dim positionValueData = editFormArgs.CellEditors.First(Function(d) Equals(d.FieldName, NameOf(DataItem.PositionValue)))
                Dim amountData = editFormArgs.CellEditors.First(Function(d) Equals(d.FieldName, NameOf(DataItem.Amount)))
                Dim price As Integer = 0
                Call Integer.TryParse(CStr(e.Value), price)
                positionValueData.Value = CInt(amountData.Value) * price
            End If
        End Sub

        Private Sub OnRowEditStarting(ByVal sender As Object, ByVal e As RowEditStartingEventArgs)
            Dim priceData = e.CellEditors.FirstOrDefault(Function(x) Equals(x.FieldName, NameOf(DataItem.Price)))
            Dim canEditData = e.CellEditors.FirstOrDefault(Function(x) Equals(x.FieldName, NameOf(DataItem.CanEdit)))
            priceData.[ReadOnly] = Not CBool(canEditData.Value)
        End Sub
    End Class
End Namespace
