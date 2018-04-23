using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace GetColumnType {
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Form1() {
            InitializeComponent();
            CreateDataSources();
        }

        #region #ShowColumnType
        void ShowColumnType(object dataSource, string dataMember, string columnName)
        {
            DevExpress.Snap.Services.DataAccessService srv = this.snapControl1.GetService<DevExpress.Snap.Services.DataAccessService>();
            Type type = srv.GetColumnType(dataSource, dataMember, columnName);
            if (type != null)
                MessageBox.Show(type.FullName, "Field Type Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Cannot determine field type", "Field Type Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnGetColumnType1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Get the type of the Id field in the DT1 table of the DS1 data source.
            ShowColumnType(this.snapControl1.DataSources[0].DataSource, "DT1", "Id");
        }
        private void btnGetColumnType2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Get the type of the Id field in the DT2 table of the DS2 data source.
            ShowColumnType(this.snapControl1.Document.DataSources[1].DataSource, "DT2", "Id");
        }
        private void btnGetColumnType3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Get the type of the Id field in the MyDataObjects data source.
            ShowColumnType(this.snapControl1.DataSources["MyDataObjects"].DataSource, string.Empty, "Id");
        }
        private void btnGetColumnType4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Get the type of the Id field in the table linked to the Parent table via Rel1 relation
            // in the MasterDetail data source.
            ShowColumnType(this.snapControl1.DataSources["MasterDetail"].DataSource, "Parent.Rel1", "Id");
        }
        #endregion #ShowColumnType

        private void CreateDataSources()
        {
            // Create DataSet DS1 and add it to the data sources collection.
            DataSet ds1 = new DataSet("DS1");
            DataTable dt1 = new DataTable("DT1");
            dt1.Columns.Add("Id", typeof(string));
            dt1.Rows.Add("AAA");
            dt1.Rows.Add("BBB");
            dt1.Rows.Add("CCC");
            ds1.Tables.Add(dt1);
            this.snapControl1.DataSources.Add("DS1", ds1);
            // Create DataSet DS2 and add it to the collection.
            DataSet ds2 = new DataSet("DS2");
            DataTable dt2 = new DataTable("DT2");
            dt2.Columns.Add("Id", typeof(int));
            dt2.Rows.Add(1);
            dt2.Rows.Add(2);
            dt2.Rows.Add(3);
            ds2.Tables.Add(dt2);
            this.snapControl1.Document.DataSources.Add("DS2", ds2);
            // Create a list of custom objects CustomDTO and add it to the data source collection.
            List<MyDataObject> data = new List<MyDataObject>() { new MyDataObject() { Id = 5 } };
            this.snapControl1.DataSources.Add("MyDataObjects", data);
            // Create a DataSet with relations and add it to the data source collection.
            DataSet masterDetail = new DataSet("md");
            DataTable parent = new DataTable("Parent");
            parent.Columns.Add("Id", typeof(int));
            parent.Rows.Add(1);
            DataTable child = new DataTable("Child");
            child.Columns.Add("ParentId", typeof(int));
            child.Columns.Add("Id", typeof(string));
            child.Rows.Add(1, "AAA");
            child.Rows.Add(1, "BBB");
            masterDetail.Tables.Add(parent);
            masterDetail.Tables.Add(child);
            masterDetail.Relations.Add("Rel1", masterDetail.Tables["Parent"].Columns["Id"], masterDetail.Tables["Child"].Columns["ParentId"]);
            this.snapControl1.DataSources.Add("MasterDetail", masterDetail);
        }
    }
    class MyDataObject {
        public short Id { get; set; }
    }
}
