Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Grid.EditForm
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
            If editFormArgs Is Nothing OrElse editFormArgs.Cell.[Property] IsNot NameOf(DataItem.Price) AndAlso e.Cell.[Property] IsNot NameOf(DataItem.CanEdit) Then
                Return
            End If

            If e.Cell.[Property] Is NameOf(DataItem.CanEdit) Then
                editFormArgs.CellEditors.FirstOrDefault(Function(x) x.FieldName Is "Price").[ReadOnly] = Not Boolean.Parse(e.Cell.Value.ToString())
                Return
            End If

            Dim positionValueData = editFormArgs.CellEditors.First(Function(d) d.FieldName Is NameOf(DataItem.PositionValue))
            Dim amountData = editFormArgs.CellEditors.First(Function(d) d.FieldName Is NameOf(DataItem.Amount))
            Dim price As Integer = 0
            Call Integer.TryParse(CStr(e.Value), price)
            positionValueData.Value = CInt(amountData.Value) * price
        End Sub

        Private Sub OnRowEditStarting(ByVal sender As Object, ByVal e As RowEditStartingEventArgs)
            e.CellEditors.FirstOrDefault(Function(x) Equals(x.FieldName, "Price")).[ReadOnly] = Not CBool(e.CellEditors.FirstOrDefault(Function(x) Equals(x.FieldName, "CanEdit")).Value)
        End Sub
    End Class
End Namespace
