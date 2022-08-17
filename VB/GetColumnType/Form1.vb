Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Windows.Forms

Namespace GetColumnType

    Public Partial Class Form1
        Inherits DevExpress.XtraBars.Ribbon.RibbonForm

        Public Sub New()
            InitializeComponent()
            CreateDataSources()
        End Sub

'#Region "#ShowColumnType"
        Private Sub ShowColumnType(ByVal dataSource As Object, ByVal dataMember As String, ByVal columnName As String)
            Dim srv As DevExpress.Snap.Services.DataAccessService = snapControl1.GetService(Of DevExpress.Snap.Services.DataAccessService)()
            Dim type As Type = srv.GetColumnType(dataSource, dataMember, columnName)
            If type IsNot Nothing Then
                MessageBox.Show(type.FullName, "Field Type Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Cannot determine field type", "Field Type Info", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Sub

        Private Sub btnGetColumnType1_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
            ' Get the type of the Id field in the DT1 table of the DS1 data source.
            ShowColumnType(snapControl1.DataSources(0).DataSource, "DT1", "Id")
        End Sub

        Private Sub btnGetColumnType2_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
            ' Get the type of the Id field in the DT2 table of the DS2 data source.
            ShowColumnType(snapControl1.Document.DataSources(1).DataSource, "DT2", "Id")
        End Sub

        Private Sub btnGetColumnType3_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
            ' Get the type of the Id field in the MyDataObjects data source.
            ShowColumnType(snapControl1.DataSources("MyDataObjects").DataSource, String.Empty, "Id")
        End Sub

        Private Sub btnGetColumnType4_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
            ' Get the type of the Id field in the table linked to the Parent table via Rel1 relation
            ' in the MasterDetail data source.
            ShowColumnType(snapControl1.DataSources("MasterDetail").DataSource, "Parent.Rel1", "Id")
        End Sub

'#End Region  ' #ShowColumnType
        Private Sub CreateDataSources()
            ' Create DataSet DS1 and add it to the data sources collection.
            Dim ds1 As DataSet = New DataSet("DS1")
            Dim dt1 As DataTable = New DataTable("DT1")
            dt1.Columns.Add("Id", GetType(String))
            dt1.Rows.Add("AAA")
            dt1.Rows.Add("BBB")
            dt1.Rows.Add("CCC")
            ds1.Tables.Add(dt1)
            snapControl1.DataSources.Add("DS1", ds1)
            ' Create DataSet DS2 and add it to the collection.
            Dim ds2 As DataSet = New DataSet("DS2")
            Dim dt2 As DataTable = New DataTable("DT2")
            dt2.Columns.Add("Id", GetType(Integer))
            dt2.Rows.Add(1)
            dt2.Rows.Add(2)
            dt2.Rows.Add(3)
            ds2.Tables.Add(dt2)
            snapControl1.Document.DataSources.Add("DS2", ds2)
            ' Create a list of custom objects CustomDTO and add it to the data source collection.
            Dim data As List(Of MyDataObject) = New List(Of MyDataObject)() From {New MyDataObject() With {.Id = 5}}
            snapControl1.DataSources.Add("MyDataObjects", data)
            ' Create a DataSet with relations and add it to the data source collection.
            Dim masterDetail As DataSet = New DataSet("md")
            Dim parent As DataTable = New DataTable("Parent")
            parent.Columns.Add("Id", GetType(Integer))
            parent.Rows.Add(1)
            Dim child As DataTable = New DataTable("Child")
            child.Columns.Add("ParentId", GetType(Integer))
            child.Columns.Add("Id", GetType(String))
            child.Rows.Add(1, "AAA")
            child.Rows.Add(1, "BBB")
            masterDetail.Tables.Add(parent)
            masterDetail.Tables.Add(child)
            masterDetail.Relations.Add("Rel1", masterDetail.Tables("Parent").Columns("Id"), masterDetail.Tables("Child").Columns("ParentId"))
            snapControl1.DataSources.Add("MasterDetail", masterDetail)
        End Sub
    End Class

    Friend Class MyDataObject

        Public Property Id As Short
    End Class
End Namespace
