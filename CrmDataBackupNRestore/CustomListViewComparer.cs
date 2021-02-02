using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrmDataBackupNRestore
{
    public class CustomListViewComparer : IComparer
    {
        private readonly int column;
        private readonly SortOrder sortOrder;

        public CustomListViewComparer()
        {
            column = 0;
            sortOrder = SortOrder.Ascending;
        }

        public CustomListViewComparer(int column, SortOrder sortOrder)
        {
            this.column = column;
            this.sortOrder = sortOrder;
        }

        public int Compare(object x, object y)
        {
            var rtn = -1;
            rtn = string.CompareOrdinal(((ListViewItem) x)?.SubItems[column].Text, ((ListViewItem) y)?.SubItems[column].Text);

            if (sortOrder == SortOrder.Descending)
            {
                rtn *= -1;
            }

            return rtn;
        }
    }
}
