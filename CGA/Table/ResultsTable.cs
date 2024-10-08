using CGA.Table.Rows;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;

namespace CGA.Table
{
    public class ResultsTable
    {
        DataGrid table;
        ObservableCollection<DynamicRow> rows = new();

        public ResultsTable(DataGrid table)
        {
            this.table = table;
            table.ItemsSource = rows;
        }

        private void UpdateDataGridColumns<T>() where T : DynamicRow
        {
            rows.Clear();
            table.Columns.Clear();

            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                var column = new DataGridTextColumn
                {
                    Header = $"  {property.Name}  ",
                    Binding = new Binding(property.Name)
                };

                table.Columns.Add(column);
            }
        }

        public void AddRow<T>(T row) where T: DynamicRow
        {
            if (rows.Count == 0)
                UpdateDataGridColumns<T>();

            if (rows.Count > 0 && rows.First() is not T)
                UpdateDataGridColumns<T>();

            rows.Add(row);
        }
        
        public void Clear()
        {
            rows.Clear();
        }
    }
}
