using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ookplab3
{
    internal class TableOperations
    {
        public void AddItem(Item it)
        {
            using (var db = new ItemsContext())
            {
                db.Items.Add(it);
                db.SaveChanges();
            }
        }
        public void AddItem(Item it, string addCol, string addVal)
        {
            var Date = "\'" + it.SupplyDate.Date.Year.ToString()+"-" + it.SupplyDate.Date.Month.ToString() + "-" + it.SupplyDate.Date.Day.ToString() + "\'";
            string vals = "\'" + it.Name + "\'" + ", " + it.Price + ", " + Date + ", " + it.Weight.ToString() + ", "
                + it.Width.ToString() + ", " + it.Length.ToString() + ", " + it.Height.ToString() + ", " + "\'" + addVal + "\'";
            using (DbConnection connection = new SqlConnection("Data Source=DESKTOP-UTL78IJ;Initial Catalog=waresDB;Integrated Security=True"))
            {
                connection.Open();
                string commString = "INSERT INTO Items (Name, Price, SupplyDate, Weight, Width, Length, Height, " + addCol + ") VALUES (" + vals + ")";
                using (DbCommand command = new SqlCommand(commString))
                {
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }
            }
        }
        public void Remove(int ind)
        {
            using (var db = new ItemsContext())
            {
                var thisObj = db.Items.SingleOrDefault(obj => obj.Id == ind);
                db.Items.Remove(thisObj);
                db.SaveChanges();
            }
        }
        public void AddColumn(string newCol)
        {
            using (DbConnection connection = new SqlConnection("Data Source=DESKTOP-UTL78IJ;Initial Catalog=waresDB;Integrated Security=True"))
            {
                connection.Open();
                using (DbCommand command = new SqlCommand("alter table [Items] add [" + newCol + "] nvarchar(50)"))
                {
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }
            }
        }
        public void RemoveColumn(string Col)
        {
            using (DbConnection connection = new SqlConnection("Data Source=DESKTOP-UTL78IJ;Initial Catalog=waresDB;Integrated Security=True"))
            {
                connection.Open();
                using (DbCommand command = new SqlCommand("alter table [Items] drop column [" + Col + "]"))
                {
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }
            }
        }
        public int Count(string name)
        {
            int result;
            using (DbConnection connection = new SqlConnection("Data Source=DESKTOP-UTL78IJ;Initial Catalog=waresDB;Integrated Security=True"))
            {
                connection.Open();
                using (DbCommand command = new SqlCommand("SELECT COUNT(Name) FROM Items WHERE Name = \'" + name + "\'"))
                {
                    command.Connection = connection;
                    result = (Int32)command.ExecuteScalar();
                }
            }
            return result;
        }
    }
    internal class ViewOperations
    {
        public void ShowTable(DataGridView dataGV)
        {
            using (var connection = new SqlConnection("Data Source=DESKTOP-UTL78IJ;Initial Catalog=waresDB;Integrated Security=True"))
            using (var adapter = new SqlDataAdapter("SELECT * FROM Items", connection))
            {
                var table = new DataTable();
                adapter.Fill(table);
                dataGV.DataSource = table;
            }
        }
        public void ShowFilteredTable(DataGridView dataGV, string filter, string val)
        {
            string comm;
            if (filter == "Name")
                comm = "SELECT * FROM Items WHERE " + filter + " LIKE \'" + val + "%\'";
            else
                comm = "SELECT * FROM Items WHERE " + filter + " LIKE " + val;
            using (var connection = new SqlConnection("Data Source=DESKTOP-UTL78IJ;Initial Catalog=waresDB;Integrated Security=True"))
            using (var adapter = new SqlDataAdapter(comm, connection))
            {
                var table = new DataTable();
                adapter.Fill(table);
                dataGV.DataSource = table;
            }
        }
        public string[] GetColVal(DataGridView dataGV, int col)
        {
            string[] array = new string[dataGV.Rows.Count - 1];
            for (int i = 0; i < dataGV.Rows.Count - 1; i++)
            {
                array[i] = dataGV.Rows[i].Cells[col].Value.ToString();
            }
            return array;
        }
        public string[] GetColNames(DataGridView dataGV)
        {
            string[] array = new string[dataGV.Columns.Count];
            for (int i = 0; i < dataGV.Columns.Count; i++)
            {
                array[i] = dataGV.Columns[i].Name;
            }
            return array;
        }
    }
    internal class Facade
    {
        ViewOperations VO;
        TableOperations TO;
        public Facade()
        {
            VO = new ViewOperations();
            TO = new TableOperations();
        }
        public void AddItem(DataGridView dataGV, string N, DateTime D, decimal P, decimal W, decimal Wi, decimal L, decimal H, string newcol = "", string newval = "")
        {
            var it = new Item
            {
                Name = N,
                SupplyDate = D.Date,
                Price = P,
                Weight = Decimal.ToDouble(W),
                Width = Decimal.ToDouble(Wi),
                Length = Decimal.ToDouble(L),
                Height = Decimal.ToDouble(H),
            };

            if ((newcol != "") && (newval != ""))
            {
                TO.AddColumn(newcol);
                TO.AddItem(it, newcol, newval);
            }
            else
            {
                TO.AddItem(it);
            }
        }

        public void Remove(DataGridView dataGV, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGV.Columns["Shipment"].Index)
            {
                int ind = Convert.ToInt32(dataGV.Rows[dataGV.CurrentCell.RowIndex].Cells[0].Value);
                TO.Remove(ind);
            }
        }

        public void Show(DataGridView dataGV, string filter = null, string val = null)
        {
            if ((filter == "") || (val == ""))
            {
                VO.ShowTable(dataGV);
            }
            else
            {
                VO.ShowFilteredTable(dataGV, filter, val);
            }
            if (dataGV.Columns.Contains("Shipment") == false)
            {
                DataGridViewButtonColumn col = new DataGridViewButtonColumn();
                col.Name = "Shipment";
                col.HeaderText = "Delete";
                col.Text = "Відвантажити";
                col.UseColumnTextForButtonValue = true;
                dataGV.Columns.Add(col);
            }
            foreach (DataGridViewColumn col in dataGV.Columns)
            {
                if (col.Name != "Shipment")
                {
                    object result = null;
                    using (var connection = new SqlConnection("Data Source=DESKTOP-UTL78IJ;Initial Catalog=waresDB;Integrated Security=True"))
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT* FROM Items WHERE " + col.Name + " IS NOT NULL", connection))
                        {
                            connection.Open();
                            result = cmd.ExecuteScalar();
                            connection.Close();
                        }
                    }
                    if (result == null)
                    {
                        TO.RemoveColumn(col.Name);
                    }
                }
            }
        }

        public void AutoComplete(DataGridView dataGV, System.Windows.Forms.TextBox textBox1, int col)
        {
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            var array = VO.GetColVal(dataGV, col);
            source.AddRange(array);
            textBox1.AutoCompleteCustomSource = source;
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        public void AddListBox(DataGridView dataGV, ListBox listb)
        {
            var arr = VO.GetColNames(dataGV);
            for(int i = 0; i < arr.Length; i++)
                listb.Items.Add(arr[i]);
        }

        public void DisplayCount(DataGridView dataGV, string name, System.Windows.Forms.TextBox textBox)
        {
            textBox.Text = TO.Count(name).ToString();
        }
    }
}
