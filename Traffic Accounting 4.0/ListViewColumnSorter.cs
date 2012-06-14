using System.Collections;
using System.Windows.Forms;
using System;

namespace Traffic_Accounting
{
    /// <summary>
    /// This class is an implementation of the 'IComparer' interface.
    /// </summary>
    public class ListViewColumnSorter : IComparer
    {
        private int ColumnToSort;
        private SortOrder OrderOfSort;
        private CaseInsensitiveComparer ObjectCompare;

        public ListViewColumnSorter()
        {
            ColumnToSort = 0;
            OrderOfSort = SortOrder.None;
            ObjectCompare = new CaseInsensitiveComparer();
        }

        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            switch(ColumnToSort)
            {
                case 0:
                    // sort by number
                    compareResult = Convert.ToInt32(listviewX.SubItems[ColumnToSort].Text).CompareTo(Convert.ToInt32(listviewY.SubItems[ColumnToSort].Text));
                    if (OrderOfSort == SortOrder.Ascending)
                    {
                        return compareResult;
                    }
                    else if (OrderOfSort == SortOrder.Descending)
                    {
                        return (-compareResult);
                    }
                    else
                    {
                        return 0;
                    }
                case 1:
                    compareResult = string.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);
                    if (OrderOfSort == SortOrder.Ascending)
                    {
                        return compareResult;
                    }
                    else if (OrderOfSort == SortOrder.Descending)
                    {
                        return (-compareResult);
                    }
                    else
                    {
                        return 0;
                    }
                case 2:
                    string xs = listviewX.SubItems[ColumnToSort].Text;
                    string ys = listviewY.SubItems[ColumnToSort].Text;
                    int ix = Convert.ToInt32(xs.Substring(0, xs.IndexOf(' ')));
                    string iix = xs.Substring(xs.IndexOf(' '), xs.Length - xs.IndexOf(' '));
                    int iy = Convert.ToInt32(ys.Substring(0, ys.IndexOf(' ')));
                    string iiy = ys.Substring(ys.IndexOf(' '), ys.Length - ys.IndexOf(' '));
                    if (iix.CompareTo(iiy) == 0)
                    {
                        compareResult = ix.CompareTo(iy);
                    }
                    else
                    {
                        compareResult = iix.CompareTo(iiy);
                    }
                    
                    if (OrderOfSort == SortOrder.Ascending)
                    {
                        return compareResult;
                    }
                    else if (OrderOfSort == SortOrder.Descending)
                    {
                        return (-compareResult);
                    }
                    else
                    {
                        return 0;
                    }
            }
            return 0;
        }

        public int SortColumn
        {
            set
            {
                ColumnToSort = value;
            }
            get
            {
                return ColumnToSort;
            }
        }

        public SortOrder Order
        {
            set
            {
                OrderOfSort = value;
            }
            get
            {
                return OrderOfSort;
            }
        }
    }
}