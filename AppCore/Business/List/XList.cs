using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Business.List
{
    public interface IComparable<in U1, in U2>
    {
        int CompareTo(U1 key1, U2 key2);
    }
    public interface IComparable<in U1, in U2, in U3>
    {
        int CompareTo(U1 key1, U2 key2, U3 key3);
    }
    public interface IComparable<in U1, in U2, in U3, in U4>
    {
        int CompareTo(U1 key1, U2 key2, U3 key3, U4 key4);
    }
    public interface IComparable<in U1, in U2, in U3, in U4, in U5>
    {
        int CompareTo(U1 key1, U2 key2, U3 key3, U4 key4, U5 key5);
    }
    public interface IComparer<in T, in U>
    {
        int Compare(T obj, U key);
    }
    public interface IComparer<in T, in U1, in U2>
    {
        int Compare(T obj, U1 key1, U2 key2);
    }
    public interface IComparer<in T, in U1, in U2, in U3>
    {
        int Compare(T obj, U1 key1, U2 key2, U3 key3);
    }
    public interface IComparer<in T, in U1, in U2, in U3, in U4>
    {
        int Compare(T obj, U1 key1, U2 key2, U3 key3, U4 key4);
    }
    public interface IComparer<in T, in U1, in U2, in U3, in U4, in U5>
    {
        int Compare(T obj, U1 key1, U2 key2, U3 key3, U4 key4, U5 key5);
    }

    public class XList<T> : List<T>
    {
        public delegate int DCompare<in T, in U>(T obj, U key);
        public delegate int DCompare<in T, in U1, in U2>(T obj, U1 key1, U2 key2);
        public delegate int DCompare<in T, in U1, in U2, in U3>(T obj, U1 key1, U2 key2, U3 key3);
        public delegate int DCompare<in T, in U1, in U2, in U3, in U4>(T obj, U1 key1, U2 key2, U3 key3, U4 key4);
        public delegate int DCompare<in T, in U1, in U2, in U3, in U4, in U5>(T obj, U1 key1, U2 key2, U3 key3, U4 key4, U5 key5);
        //T methods
        public void AddSorted(T item)
        {
            int index = BinarySearch(item);
            if (index < 0) index = ~index;
            Insert(index, item);
        }
        public bool AddSortedUnique(T item)
        {
            int index = BinarySearch(item);
            if (index >= 0) return false;
            Insert(~index, item);
            return true;
        }
        public void AddSortedRange(List<T> items)
        {
            int i, cnt = items.Count;
            for (i = 0; i < cnt; ++i) AddSorted(items[i]);
        }
        public void AddSortedRangeUnique(List<T> items)
        {
            int i, cnt = items.Count;
            for (i = 0; i < cnt; ++i) AddSortedUnique(items[i]);
        }
        public bool RemoveSorted(T item)
        {
            int index = BinarySearch(item);
            if (index >= 0) RemoveAt(index);
            return (index >= 0);
        }

        #region 1 anahtarlı fonksiyonlar (U)
        public int FindIndex<U>(U key)
        {
            int r, n, n1, n2;
            if (Count < 8)
            {
                for (n1 = Count - 1; n1 >= 0; --n1)
                {
                    r = ((IComparable<U>)this[n1]).CompareTo(key);
                    if (r < 0)
                    {
                        ++n1; break;
                    }
                    if (r == 0) return n1;
                }
                if (n1 >= 0) n1 = ~n1;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = ((IComparable<U>)this[n]).CompareTo(key);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else return n;
                }
                n1 = ~n1;
            }
            return n1;
        }
        public int FindIndex<U>(IComparer<T, U> c, U key)
        {
            int r, n, n1, n2;
            if (Count < 8)
            {
                for (n1 = Count - 1; n1 >= 0; --n1)
                {
                    r = c.Compare(this[n1], key);
                    if (r < 0)
                    {
                        ++n1; break;
                    }
                    if (r == 0) return n1;
                }
                if (n1 >= 0) n1 = ~n1;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = c.Compare(this[n], key);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else return n;
                }
                n1 = ~n1;
            }
            return n1;
        }
        public int FindIndex<U>(DCompare<T, U> d, U key)
        {
            int r, n, n1, n2;
            if (Count < 8)
            {
                for (n1 = Count - 1; n1 >= 0; --n1)
                {
                    r = d(this[n1], key);
                    if (r < 0)
                    {
                        ++n1; break;
                    }
                    if (r == 0) return n1;
                }
                if (n1 >= 0) n1 = ~n1;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = d(this[n], key);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else return n;
                }
                n1 = ~n1;
            }
            return n1;
        }
        public bool FindRange<U>(U key, out int index1, out int index2)
        {
            int r, n, n1, n2;
            index1 = 0; index2 = 0;
            if (Count < 8)
            {
                for (n = Count - 1; n >= 0; --n)
                {
                    r = ((IComparable<U>)this[n]).CompareTo(key);
                    if (r < 0) return false;
                    if (r == 0) break;
                }
                if (n == -1) return false;
                index2 = n + 1;
                for (--n; n >= 0; --n)
                {
                    if (((IComparable<U>)this[n]).CompareTo(key) != 0) break;
                }
                index1 = n + 1;
                return true;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = ((IComparable<U>)this[n]).CompareTo(key);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else
                    {
                        n1 = n; n2 = Count;
                        for (n = n1 - 1; n >= 0; --n)
                        {
                            if (((IComparable<U>)this[n]).CompareTo(key) != 0) break;
                        }
                        index1 = n + 1;
                        for (n = n1 + 1; n < n2; ++n)
                        {
                            if (((IComparable<U>)this[n]).CompareTo(key) != 0) break;
                        }
                        index2 = n;
                        return true;
                    }
                }
                return false;
            }
        }
        public bool FindRange<U>(IComparer<T, U> c, U key, out int index1, out int index2)
        {
            int r, n, n1, n2;
            index1 = 0; index2 = 0;
            if (Count < 8)
            {
                for (n = Count - 1; n >= 0; --n)
                {
                    r = c.Compare(this[n], key);
                    if (r < 0) return false;
                    if (r == 0) break;
                }
                if (n == -1) return false;
                index2 = n + 1;
                for (--n; n >= 0; --n)
                {
                    if (c.Compare(this[n], key) != 0) break;
                }
                index1 = n + 1;
                return true;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = c.Compare(this[n], key);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else
                    {
                        n1 = n; n2 = Count;
                        for (n = n1 - 1; n >= 0; --n)
                        {
                            if (c.Compare(this[n], key) != 0) break;
                        }
                        index1 = n + 1;
                        for (n = n1 + 1; n < n2; ++n)
                        {
                            if (c.Compare(this[n], key) != 0) break;
                        }
                        index2 = n;
                        return true;
                    }
                }
                return false;
            }
        }
        public bool FindRange<U>(DCompare<T, U> d, U key, out int index1, out int index2)
        {
            int r, n, n1, n2;
            index1 = 0; index2 = 0;
            if (Count < 8)
            {
                for (n = Count - 1; n >= 0; --n)
                {
                    r = d(this[n], key);
                    if (r < 0) return false;
                    if (r == 0) break;
                }
                if (n == -1) return false;
                index2 = n + 1;
                for (--n; n >= 0; --n)
                {
                    if (d(this[n], key) != 0) break;
                }
                index1 = n + 1;
                return true;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = d(this[n], key);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else
                    {
                        n1 = n; n2 = Count;
                        for (n = n1 - 1; n >= 0; --n)
                        {
                            if (d(this[n], key) != 0) break;
                        }
                        index1 = n + 1;
                        for (n = n1 + 1; n < n2; ++n)
                        {
                            if (d(this[n], key) != 0) break;
                        }
                        index2 = n;
                        return true;
                    }
                }
                return false;
            }
        }

        public int SearchIndex<U>(U key)
        {
            int i;
            for (i = Count - 1; i >= 0; --i)
            {
                if (((IComparable<U>)this[i]).CompareTo(key) == 0) return i;
            }
            return -1;
        }
        public int SearchIndex<U>(IComparer<T, U> c, U key)
        {
            int i;
            for (i = Count - 1; i >= 0; --i)
            {
                if (c.Compare(this[i], key) == 0) return i;
            }
            return -1;
        }
        public int SearchIndex<U>(DCompare<T, U> d, U key)
        {
            int i;
            for (i = Count - 1; i >= 0; --i)
            {
                if (d(this[i], key) == 0) return i;
            }
            return -1;
        }
        public T Find<U>(U key)
        {
            int index = FindIndex(key);
            if (index >= 0) return this[index];
            return default;
        }
        public T Find<U>(IComparer<T, U> c, U key)
        {
            int index = FindIndex(c, key);
            if (index >= 0) return this[index];
            return default;
        }
        public T Find<U>(DCompare<T, U> d, U key)
        {
            int index = FindIndex(d, key);
            if (index >= 0) return this[index];
            return default;
        }
        public T Search<U>(U key)
        {
            int index = SearchIndex(key);
            if (index >= 0) return this[index];
            return default;
        }
        public T Search<U>(IComparer<T, U> c, U key)
        {
            int index = SearchIndex(c, key);
            if (index >= 0) return this[index];
            return default;
        }
        public T Search<U>(DCompare<T, U> d, U key)
        {
            int index = SearchIndex(d, key);
            if (index >= 0) return this[index];
            return default;
        }
        public void RemoveSorted<U>(U key)
        {
            int index = FindIndex(key);
            if (index >= 0) RemoveAt(index);
        }
        #endregion

        #region 2 anahtarlı fonksiyonlar (U1, U2)
        public int FindIndex<U1, U2>(U1 key1, U2 key2)
        {
            int r, n, n1, n2;
            if (Count < 8)
            {
                for (n1 = Count - 1; n1 >= 0; --n1)
                {
                    r = ((IComparable<U1, U2>)this[n1]).CompareTo(key1, key2);
                    if (r < 0)
                    {
                        ++n1; break;
                    }
                    if (r == 0) return n1;
                }
                if (n1 >= 0) n1 = ~n1;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = ((IComparable<U1, U2>)this[n]).CompareTo(key1, key2);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else return n;
                }
                n1 = ~n1;
            }
            return n1;
        }
        public int FindIndex<U1, U2>(IComparer<T, U1, U2> c, U1 key1, U2 key2)
        {
            int r, n, n1, n2;
            if (Count < 8)
            {
                for (n1 = Count - 1; n1 >= 0; --n1)
                {
                    r = c.Compare(this[n1], key1, key2);
                    if (r < 0)
                    {
                        ++n1; break;
                    }
                    if (r == 0) return n1;
                }
                if (n1 >= 0) n1 = ~n1;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = c.Compare(this[n], key1, key2);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else return n;
                }
                n1 = ~n1;
            }
            return n1;
        }
        public int FindIndex<U1, U2>(DCompare<T, U1, U2> d, U1 key1, U2 key2)
        {
            int r, n, n1, n2;
            if (Count < 8)
            {
                for (n1 = Count - 1; n1 >= 0; --n1)
                {
                    r = d(this[n1], key1, key2);
                    if (r < 0)
                    {
                        ++n1; break;
                    }
                    if (r == 0) return n1;
                }
                if (n1 >= 0) n1 = ~n1;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = d(this[n], key1, key2);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else return n;
                }
                n1 = ~n1;
            }
            return n1;
        }
        public bool FindRange<U1, U2>(U1 key1, U2 key2, out int index1, out int index2)
        {
            int r, n, n1, n2;
            index1 = 0; index2 = 0;
            if (Count < 8)
            {
                for (n = Count - 1; n >= 0; --n)
                {
                    r = ((IComparable<U1, U2>)this[n]).CompareTo(key1, key2);
                    if (r < 0) return false;
                    if (r == 0) break;
                }
                if (n == -1) return false;
                index2 = n + 1;
                for (--n; n >= 0; --n)
                {
                    if (((IComparable<U1, U2>)this[n]).CompareTo(key1, key2) != 0) break;
                }
                index1 = n + 1;
                return true;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = ((IComparable<U1, U2>)this[n]).CompareTo(key1, key2);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else
                    {
                        n1 = n; n2 = Count;
                        for (n = n1 - 1; n >= 0; --n)
                        {
                            if (((IComparable<U1, U2>)this[n]).CompareTo(key1, key2) != 0) break;
                        }
                        index1 = n + 1;
                        for (n = n1 + 1; n < n2; ++n)
                        {
                            if (((IComparable<U1, U2>)this[n]).CompareTo(key1, key2) != 0) break;
                        }
                        index2 = n;
                        return true;
                    }
                }
                return false;
            }
        }
        public bool FindRange<U1, U2>(IComparer<T, U1, U2> c, U1 key1, U2 key2, out int index1, out int index2)
        {
            int r, n, n1, n2;
            index1 = 0; index2 = 0;
            if (Count < 8)
            {
                for (n = Count - 1; n >= 0; --n)
                {
                    r = c.Compare(this[n], key1, key2);
                    if (r < 0) return false;
                    if (r == 0) break;
                }
                if (n == -1) return false;
                index2 = n + 1;
                for (--n; n >= 0; --n)
                {
                    if (c.Compare(this[n], key1, key2) != 0) break;
                }
                index1 = n + 1;
                return true;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = c.Compare(this[n], key1, key2);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else
                    {
                        n1 = n; n2 = Count;
                        for (n = n1 - 1; n >= 0; --n)
                        {
                            if (c.Compare(this[n], key1, key2) != 0) break;
                        }
                        index1 = n + 1;
                        for (n = n1 + 1; n < n2; ++n)
                        {
                            if (c.Compare(this[n], key1, key2) != 0) break;
                        }
                        index2 = n;
                        return true;
                    }
                }
                return false;
            }
        }
        public bool FindRange<U1, U2>(DCompare<T, U1, U2> d, U1 key1, U2 key2, out int index1, out int index2)
        {
            int r, n, n1, n2;
            index1 = 0; index2 = 0;
            if (Count < 8)
            {
                for (n = Count - 1; n >= 0; --n)
                {
                    r = d(this[n], key1, key2);
                    if (r < 0) return false;
                    if (r == 0) break;
                }
                if (n == -1) return false;
                index2 = n + 1;
                for (--n; n >= 0; --n)
                {
                    if (d(this[n], key1, key2) != 0) break;
                }
                index1 = n + 1;
                return true;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = d(this[n], key1, key2);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else
                    {
                        n1 = n; n2 = Count;
                        for (n = n1 - 1; n >= 0; --n)
                        {
                            if (d(this[n], key1, key2) != 0) break;
                        }
                        index1 = n + 1;
                        for (n = n1 + 1; n < n2; ++n)
                        {
                            if (d(this[n], key1, key2) != 0) break;
                        }
                        index2 = n;
                        return true;
                    }
                }
                return false;
            }
        }

        public int SearchIndex<U1, U2>(U1 key1, U2 key2)
        {
            int i;
            for (i = Count - 1; i >= 0; --i)
            {
                if (((IComparable<U1, U2>)this[i]).CompareTo(key1, key2) == 0) return i;
            }
            return -1;
        }
        public int SearchIndex<U1, U2>(IComparer<T, U1, U2> c, U1 key1, U2 key2)
        {
            int i;
            for (i = Count - 1; i >= 0; --i)
            {
                if (c.Compare(this[i], key1, key2) == 0) return i;
            }
            return -1;
        }
        public int SearchIndex<U1, U2>(DCompare<T, U1, U2> d, U1 key1, U2 key2)
        {
            int i;
            for (i = Count - 1; i >= 0; --i)
            {
                if (d(this[i], key1, key2) == 0) return i;
            }
            return -1;
        }

        public T Find<U1, U2>(U1 key1, U2 key2)
        {
            int index = FindIndex(key1, key2);
            if (index >= 0) return this[index];
            return default;
        }
        public T Find<U1, U2>(IComparer<T, U1, U2> c, U1 key1, U2 key2)
        {
            int index = FindIndex(c, key1, key2);
            if (index >= 0) return this[index];
            return default;
        }
        public T Find<U1, U2>(DCompare<T, U1, U2> d, U1 key1, U2 key2)
        {
            int index = FindIndex(d, key1, key2);
            if (index >= 0) return this[index];
            return default;
        }
        public T Search<U1, U2>(U1 key1, U2 key2)
        {
            int index = SearchIndex(key1, key2);
            if (index >= 0) return this[index];
            return default;
        }
        public T Search<U1, U2>(IComparer<T, U1, U2> c, U1 key1, U2 key2)
        {
            int index = SearchIndex(c, key1, key2);
            if (index >= 0) return this[index];
            return default;
        }
        public T Search<U1, U2>(DCompare<T, U1, U2> d, U1 key1, U2 key2)
        {
            int index = SearchIndex(d, key1, key2);
            if (index >= 0) return this[index];
            return default;
        }

        public void RemoveSorted<U1, U2>(U1 key1, U2 key2)
        {
            int index = FindIndex(key1, key2);
            if (index >= 0) RemoveAt(index);
        }
        #endregion

        #region 3 anahtarlı fonksiyonlar (U1, U2, U3)
        public int FindIndex<U1, U2, U3>(U1 key1, U2 key2, U3 key3)
        {
            int r, n, n1, n2;
            if (Count < 8)
            {
                for (n1 = Count - 1; n1 >= 0; --n1)
                {
                    r = ((IComparable<U1, U2, U3>)this[n1]).CompareTo(key1, key2, key3);
                    if (r < 0)
                    {
                        ++n1; break;
                    }
                    if (r == 0) return n1;
                }
                if (n1 >= 0) n1 = ~n1;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = ((IComparable<U1, U2, U3>)this[n]).CompareTo(key1, key2, key3);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else return n;
                }
                n1 = ~n1;
            }
            return n1;
        }
        public int FindIndex<U1, U2, U3>(IComparer<T, U1, U2, U3> c, U1 key1, U2 key2, U3 key3)
        {
            int r, n, n1, n2;
            if (Count < 8)
            {
                for (n1 = Count - 1; n1 >= 0; --n1)
                {
                    r = c.Compare(this[n1], key1, key2, key3);
                    if (r < 0)
                    {
                        ++n1; break;
                    }
                    if (r == 0) return n1;
                }
                if (n1 >= 0) n1 = ~n1;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = c.Compare(this[n], key1, key2, key3);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else return n;
                }
                n1 = ~n1;
            }
            return n1;
        }
        public int FindIndex<U1, U2, U3>(DCompare<T, U1, U2, U3> d, U1 key1, U2 key2, U3 key3)
        {
            int r, n, n1, n2;
            if (Count < 8)
            {
                for (n1 = Count - 1; n1 >= 0; --n1)
                {
                    r = d(this[n1], key1, key2, key3);
                    if (r < 0)
                    {
                        ++n1; break;
                    }
                    if (r == 0) return n1;
                }
                if (n1 >= 0) n1 = ~n1;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = d(this[n], key1, key2, key3);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else return n;
                }
                n1 = ~n1;
            }
            return n1;
        }
        public bool FindRange<U1, U2, U3>(U1 key1, U2 key2, U3 key3, out int index1, out int index2)
        {
            int r, n, n1, n2;
            index1 = 0; index2 = 0;
            if (Count < 8)
            {
                for (n = Count - 1; n >= 0; --n)
                {
                    r = ((IComparable<U1, U2, U3>)this[n]).CompareTo(key1, key2, key3);
                    if (r < 0) return false;
                    if (r == 0) break;
                }
                if (n == -1) return false;
                index2 = n + 1;
                for (--n; n >= 0; --n)
                {
                    if (((IComparable<U1, U2, U3>)this[n]).CompareTo(key1, key2, key3) != 0) break;
                }
                index1 = n + 1;
                return true;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = ((IComparable<U1, U2, U3>)this[n]).CompareTo(key1, key2, key3);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else
                    {
                        n1 = n; n2 = Count;
                        for (n = n1 - 1; n >= 0; --n)
                        {
                            if (((IComparable<U1, U2, U3>)this[n]).CompareTo(key1, key2, key3) != 0) break;
                        }
                        index1 = n + 1;
                        for (n = n1 + 1; n < n2; ++n)
                        {
                            if (((IComparable<U1, U2, U3>)this[n]).CompareTo(key1, key2, key3) != 0) break;
                        }
                        index2 = n;
                        return true;
                    }
                }
                return false;
            }
        }
        public bool FindRange<U1, U2, U3>(IComparer<T, U1, U2, U3> c, U1 key1, U2 key2, U3 key3, out int index1, out int index2)
        {
            int r, n, n1, n2;
            index1 = 0; index2 = 0;
            if (Count < 8)
            {
                for (n = Count - 1; n >= 0; --n)
                {
                    r = c.Compare(this[n], key1, key2, key3);
                    if (r < 0) return false;
                    if (r == 0) break;
                }
                if (n == -1) return false;
                index2 = n + 1;
                for (--n; n >= 0; --n)
                {
                    if (c.Compare(this[n], key1, key2, key3) != 0) break;
                }
                index1 = n + 1;
                return true;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = c.Compare(this[n], key1, key2, key3);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else
                    {
                        n1 = n; n2 = Count;
                        for (n = n1 - 1; n >= 0; --n)
                        {
                            if (c.Compare(this[n], key1, key2, key3) != 0) break;
                        }
                        index1 = n + 1;
                        for (n = n1 + 1; n < n2; ++n)
                        {
                            if (c.Compare(this[n], key1, key2, key3) != 0) break;
                        }
                        index2 = n;
                        return true;
                    }
                }
                return false;
            }
        }
        public bool FindRange<U1, U2, U3>(DCompare<T, U1, U2, U3> d, U1 key1, U2 key2, U3 key3, out int index1, out int index2)
        {
            int r, n, n1, n2;
            index1 = 0; index2 = 0;
            if (Count < 8)
            {
                for (n = Count - 1; n >= 0; --n)
                {
                    r = d(this[n], key1, key2, key3);
                    if (r < 0) return false;
                    if (r == 0) break;
                }
                if (n == -1) return false;
                index2 = n + 1;
                for (--n; n >= 0; --n)
                {
                    if (d(this[n], key1, key2, key3) != 0) break;
                }
                index1 = n + 1;
                return true;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = d(this[n], key1, key2, key3);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else
                    {
                        n1 = n; n2 = Count;
                        for (n = n1 - 1; n >= 0; --n)
                        {
                            if (d(this[n], key1, key2, key3) != 0) break;
                        }
                        index1 = n + 1;
                        for (n = n1 + 1; n < n2; ++n)
                        {
                            if (d(this[n], key1, key2, key3) != 0) break;
                        }
                        index2 = n;
                        return true;
                    }
                }
                return false;
            }
        }
        public int SearchIndex<U1, U2, U3>(U1 key1, U2 key2, U3 key3)
        {
            int i;
            for (i = Count - 1; i >= 0; --i)
            {
                if (((IComparable<U1, U2, U3>)this[i]).CompareTo(key1, key2, key3) == 0) return i;
            }
            return -1;
        }
        public int SearchIndex<U1, U2, U3>(IComparer<T, U1, U2, U3> c, U1 key1, U2 key2, U3 key3)
        {
            int i;
            for (i = Count - 1; i >= 0; --i)
            {
                if (c.Compare(this[i], key1, key2, key3) == 0) return i;
            }
            return -1;
        }
        public int SearchIndex<U1, U2, U3>(DCompare<T, U1, U2, U3> d, U1 key1, U2 key2, U3 key3)
        {
            int i;
            for (i = Count - 1; i >= 0; --i)
            {
                if (d(this[i], key1, key2, key3) == 0) return i;
            }
            return -1;
        }

        public T Find<U1, U2, U3>(U1 key1, U2 key2, U3 key3)
        {
            int index = FindIndex(key1, key2, key3);
            if (index >= 0) return this[index];
            return default;
        }
        public T Find<U1, U2, U3>(IComparer<T, U1, U2, U3> c, U1 key1, U2 key2, U3 key3)
        {
            int index = FindIndex(c, key1, key2, key3);
            if (index >= 0) return this[index];
            return default;
        }
        public T Find<U1, U2, U3>(DCompare<T, U1, U2, U3> d, U1 key1, U2 key2, U3 key3)
        {
            int index = FindIndex(d, key1, key2, key3);
            if (index >= 0) return this[index];
            return default;
        }
        public T Search<U1, U2, U3>(U1 key1, U2 key2, U3 key3)
        {
            int index = SearchIndex(key1, key2, key3);
            if (index >= 0) return this[index];
            return default;
        }
        public T Search<U1, U2, U3>(IComparer<T, U1, U2, U3> c, U1 key1, U2 key2, U3 key3)
        {
            int index = SearchIndex(c, key1, key2, key3);
            if (index >= 0) return this[index];
            return default;
        }
        public T Search<U1, U2, U3>(DCompare<T, U1, U2, U3> d, U1 key1, U2 key2, U3 key3)
        {
            int index = SearchIndex(d, key1, key2, key3);
            if (index >= 0) return this[index];
            return default;
        }

        public void RemoveSorted<U1, U2, U3>(U1 key1, U2 key2, U3 key3)
        {
            int index = FindIndex(key1, key2, key3);
            if (index >= 0) RemoveAt(index);
        }
        #endregion

        #region 4 anahtarlı fonksiyonlar (U1, U2, U3, U4)
        public int FindIndex<U1, U2, U3, U4>(U1 key1, U2 key2, U3 key3, U4 key4)
        {
            int r, n, n1, n2;
            if (Count < 8)
            {
                for (n1 = Count - 1; n1 >= 0; --n1)
                {
                    r = ((IComparable<U1, U2, U3, U4>)this[n1]).CompareTo(key1, key2, key3, key4);
                    if (r < 0)
                    {
                        ++n1; break;
                    }
                    if (r == 0) return n1;
                }
                if (n1 >= 0) n1 = ~n1;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = ((IComparable<U1, U2, U3, U4>)this[n]).CompareTo(key1, key2, key3, key4);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else return n;
                }
                n1 = ~n1;
            }
            return n1;
        }
        public int FindIndex<U1, U2, U3, U4>(IComparer<T, U1, U2, U3, U4> c, U1 key1, U2 key2, U3 key3, U4 key4)
        {
            int r, n, n1, n2;
            if (Count < 8)
            {
                for (n1 = Count - 1; n1 >= 0; --n1)
                {
                    r = c.Compare(this[n1], key1, key2, key3, key4);
                    if (r < 0)
                    {
                        ++n1; break;
                    }
                    if (r == 0) return n1;
                }
                if (n1 >= 0) n1 = ~n1;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = c.Compare(this[n], key1, key2, key3, key4);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else return n;
                }
                n1 = ~n1;
            }
            return n1;
        }
        public int FindIndex<U1, U2, U3, U4>(DCompare<T, U1, U2, U3, U4> d, U1 key1, U2 key2, U3 key3, U4 key4)
        {
            int r, n, n1, n2;
            if (Count < 8)
            {
                for (n1 = Count - 1; n1 >= 0; --n1)
                {
                    r = d(this[n1], key1, key2, key3, key4);
                    if (r < 0)
                    {
                        ++n1; break;
                    }
                    if (r == 0) return n1;
                }
                if (n1 >= 0) n1 = ~n1;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = d(this[n], key1, key2, key3, key4);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else return n;
                }
                n1 = ~n1;
            }
            return n1;
        }
        public bool FindRange<U1, U2, U3, U4>(U1 key1, U2 key2, U3 key3, U4 key4, out int index1, out int index2)
        {
            int r, n, n1, n2;
            index1 = 0; index2 = 0;
            if (Count < 8)
            {
                for (n = Count - 1; n >= 0; --n)
                {
                    r = ((IComparable<U1, U2, U3, U4>)this[n]).CompareTo(key1, key2, key3, key4);
                    if (r < 0) return false;
                    if (r == 0) break;
                }
                if (n == -1) return false;
                index2 = n + 1;
                for (--n; n >= 0; --n)
                {
                    if (((IComparable<U1, U2, U3, U4>)this[n]).CompareTo(key1, key2, key3, key4) != 0) break;
                }
                index1 = n + 1;
                return true;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = ((IComparable<U1, U2, U3, U4>)this[n]).CompareTo(key1, key2, key3, key4);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else
                    {
                        n1 = n; n2 = Count;
                        for (n = n1 - 1; n >= 0; --n)
                        {
                            if (((IComparable<U1, U2, U3, U4>)this[n]).CompareTo(key1, key2, key3, key4) != 0) break;
                        }
                        index1 = n + 1;
                        for (n = n1 + 1; n < n2; ++n)
                        {
                            if (((IComparable<U1, U2, U3, U4>)this[n]).CompareTo(key1, key2, key3, key4) != 0) break;
                        }
                        index2 = n;
                        return true;
                    }
                }
                return false;
            }
        }
        public bool FindRange<U1, U2, U3, U4>(IComparer<T, U1, U2, U3, U4> c, U1 key1, U2 key2, U3 key3, U4 key4, out int index1, out int index2)
        {
            int r, n, n1, n2;
            index1 = 0; index2 = 0;
            if (Count < 8)
            {
                for (n = Count - 1; n >= 0; --n)
                {
                    r = c.Compare(this[n], key1, key2, key3, key4);
                    if (r < 0) return false;
                    if (r == 0) break;
                }
                if (n == -1) return false;
                index2 = n + 1;
                for (--n; n >= 0; --n)
                {
                    if (c.Compare(this[n], key1, key2, key3, key4) != 0) break;
                }
                index1 = n + 1;
                return true;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = c.Compare(this[n], key1, key2, key3, key4);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else
                    {
                        n1 = n; n2 = Count;
                        for (n = n1 - 1; n >= 0; --n)
                        {
                            if (c.Compare(this[n], key1, key2, key3, key4) != 0) break;
                        }
                        index1 = n + 1;
                        for (n = n1 + 1; n < n2; ++n)
                        {
                            if (c.Compare(this[n], key1, key2, key3, key4) != 0) break;
                        }
                        index2 = n;
                        return true;
                    }
                }
                return false;
            }
        }
        public bool FindRange<U1, U2, U3, U4>(DCompare<T, U1, U2, U3, U4> d, U1 key1, U2 key2, U3 key3, U4 key4, out int index1, out int index2)
        {
            int r, n, n1, n2;
            index1 = 0; index2 = 0;
            if (Count < 8)
            {
                for (n = Count - 1; n >= 0; --n)
                {
                    r = d(this[n], key1, key2, key3, key4);
                    if (r < 0) return false;
                    if (r == 0) break;
                }
                if (n == -1) return false;
                index2 = n + 1;
                for (--n; n >= 0; --n)
                {
                    if (d(this[n], key1, key2, key3, key4) != 0) break;
                }
                index1 = n + 1;
                return true;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = d(this[n], key1, key2, key3, key4);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else
                    {
                        n1 = n; n2 = Count;
                        for (n = n1 - 1; n >= 0; --n)
                        {
                            if (d(this[n], key1, key2, key3, key4) != 0) break;
                        }
                        index1 = n + 1;
                        for (n = n1 + 1; n < n2; ++n)
                        {
                            if (d(this[n], key1, key2, key3, key4) != 0) break;
                        }
                        index2 = n;
                        return true;
                    }
                }
                return false;
            }
        }
        public int SearchIndex<U1, U2, U3, U4>(U1 key1, U2 key2, U3 key3, U4 key4)
        {
            int i;
            for (i = Count - 1; i >= 0; --i)
            {
                if (((IComparable<U1, U2, U3, U4>)this[i]).CompareTo(key1, key2, key3, key4) == 0) return i;
            }
            return -1;
        }
        public int SearchIndex<U1, U2, U3, U4>(IComparer<T, U1, U2, U3, U4> c, U1 key1, U2 key2, U3 key3, U4 key4)
        {
            int i;
            for (i = Count - 1; i >= 0; --i)
            {
                if (c.Compare(this[i], key1, key2, key3, key4) == 0) return i;
            }
            return -1;
        }
        public int SearchIndex<U1, U2, U3, U4>(DCompare<T, U1, U2, U3, U4> d, U1 key1, U2 key2, U3 key3, U4 key4)
        {
            int i;
            for (i = Count - 1; i >= 0; --i)
            {
                if (d(this[i], key1, key2, key3, key4) == 0) return i;
            }
            return -1;
        }

        public T Find<U1, U2, U3, U4>(U1 key1, U2 key2, U3 key3, U4 key4)
        {
            int index = FindIndex(key1, key2, key3, key4);
            if (index >= 0) return this[index];
            return default;
        }
        public T Find<U1, U2, U3, U4>(IComparer<T, U1, U2, U3, U4> c, U1 key1, U2 key2, U3 key3, U4 key4)
        {
            int index = FindIndex(c, key1, key2, key3, key4);
            if (index >= 0) return this[index];
            return default;
        }
        public T Find<U1, U2, U3, U4>(DCompare<T, U1, U2, U3, U4> d, U1 key1, U2 key2, U3 key3, U4 key4)
        {
            int index = FindIndex(d, key1, key2, key3, key4);
            if (index >= 0) return this[index];
            return default;
        }
        public T Search<U1, U2, U3, U4>(U1 key1, U2 key2, U3 key3, U4 key4)
        {
            int index = SearchIndex(key1, key2, key3, key4);
            if (index >= 0) return this[index];
            return default;
        }
        public T Search<U1, U2, U3, U4>(IComparer<T, U1, U2, U3, U4> c, U1 key1, U2 key2, U3 key3, U4 key4)
        {
            int index = SearchIndex(c, key1, key2, key3, key4);
            if (index >= 0) return this[index];
            return default;
        }
        public T Search<U1, U2, U3, U4>(DCompare<T, U1, U2, U3, U4> d, U1 key1, U2 key2, U3 key3, U4 key4)
        {
            int index = SearchIndex(d, key1, key2, key3, key4);
            if (index >= 0) return this[index];
            return default;
        }

        public void RemoveSorted<U1, U2, U3, U4>(U1 key1, U2 key2, U3 key3, U4 key4)
        {
            int index = FindIndex(key1, key2, key3, key4);
            if (index >= 0) RemoveAt(index);
        }
        #endregion

        #region 5 anahtarlı fonksiyonlar (U1, U2, U3, U4)
        public int FindIndex<U1, U2, U3, U4, U5>(U1 key1, U2 key2, U3 key3, U4 key4, U5 key5)
        {
            int r, n, n1, n2;
            if (Count < 8)
            {
                for (n1 = Count - 1; n1 >= 0; --n1)
                {
                    r = ((IComparable<U1, U2, U3, U4, U5>)this[n1]).CompareTo(key1, key2, key3, key4, key5);
                    if (r < 0)
                    {
                        ++n1; break;
                    }
                    if (r == 0) return n1;
                }
                if (n1 >= 0) n1 = ~n1;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = ((IComparable<U1, U2, U3, U4, U5>)this[n]).CompareTo(key1, key2, key3, key4, key5);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else return n;
                }
                n1 = ~n1;
            }
            return n1;
        }
        public int FindIndex<U1, U2, U3, U4, U5>(IComparer<T, U1, U2, U3, U4, U5> c, U1 key1, U2 key2, U3 key3, U4 key4, U5 key5)
        {
            int r, n, n1, n2;
            if (Count < 8)
            {
                for (n1 = Count - 1; n1 >= 0; --n1)
                {
                    r = c.Compare(this[n1], key1, key2, key3, key4, key5);
                    if (r < 0)
                    {
                        ++n1; break;
                    }
                    if (r == 0) return n1;
                }
                if (n1 >= 0) n1 = ~n1;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = c.Compare(this[n], key1, key2, key3, key4, key5);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else return n;
                }
                n1 = ~n1;
            }
            return n1;
        }
        public int FindIndex<U1, U2, U3, U4, U5>(DCompare<T, U1, U2, U3, U4, U5> d, U1 key1, U2 key2, U3 key3, U4 key4, U5 key5)
        {
            int r, n, n1, n2;
            if (Count < 8)
            {
                for (n1 = Count - 1; n1 >= 0; --n1)
                {
                    r = d(this[n1], key1, key2, key3, key4, key5);
                    if (r < 0)
                    {
                        ++n1; break;
                    }
                    if (r == 0) return n1;
                }
                if (n1 >= 0) n1 = ~n1;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = d(this[n], key1, key2, key3, key4, key5);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else return n;
                }
                n1 = ~n1;
            }
            return n1;
        }
        public bool FindRange<U1, U2, U3, U4, U5>(U1 key1, U2 key2, U3 key3, U4 key4, U5 key5, out int index1, out int index2)
        {
            int r, n, n1, n2;
            index1 = 0; index2 = 0;
            if (Count < 8)
            {
                for (n = Count - 1; n >= 0; --n)
                {
                    r = ((IComparable<U1, U2, U3, U4, U5>)this[n]).CompareTo(key1, key2, key3, key4, key5);
                    if (r < 0) return false;
                    if (r == 0) break;
                }
                if (n == -1) return false;
                index2 = n + 1;
                for (--n; n >= 0; --n)
                {
                    if (((IComparable<U1, U2, U3, U4, U5>)this[n]).CompareTo(key1, key2, key3, key4, key5) != 0) break;
                }
                index1 = n + 1;
                return true;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = ((IComparable<U1, U2, U3, U4, U5>)this[n]).CompareTo(key1, key2, key3, key4, key5);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else
                    {
                        n1 = n; n2 = Count;
                        for (n = n1 - 1; n >= 0; --n)
                        {
                            if (((IComparable<U1, U2, U3, U4, U5>)this[n]).CompareTo(key1, key2, key3, key4, key5) != 0) break;
                        }
                        index1 = n + 1;
                        for (n = n1 + 1; n < n2; ++n)
                        {
                            if (((IComparable<U1, U2, U3, U4, U5>)this[n]).CompareTo(key1, key2, key3, key4, key5) != 0) break;
                        }
                        index2 = n;
                        return true;
                    }
                }
                return false;
            }
        }
        public bool FindRange<U1, U2, U3, U4, U5>(IComparer<T, U1, U2, U3, U4, U5> c, U1 key1, U2 key2, U3 key3, U4 key4, U5 key5, out int index1, out int index2)
        {
            int r, n, n1, n2;
            index1 = 0; index2 = 0;
            if (Count < 8)
            {
                for (n = Count - 1; n >= 0; --n)
                {
                    r = c.Compare(this[n], key1, key2, key3, key4, key5);
                    if (r < 0) return false;
                    if (r == 0) break;
                }
                if (n == -1) return false;
                index2 = n + 1;
                for (--n; n >= 0; --n)
                {
                    if (c.Compare(this[n], key1, key2, key3, key4, key5) != 0) break;
                }
                index1 = n + 1;
                return true;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = c.Compare(this[n], key1, key2, key3, key4, key5);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else
                    {
                        n1 = n; n2 = Count;
                        for (n = n1 - 1; n >= 0; --n)
                        {
                            if (c.Compare(this[n], key1, key2, key3, key4, key5) != 0) break;
                        }
                        index1 = n + 1;
                        for (n = n1 + 1; n < n2; ++n)
                        {
                            if (c.Compare(this[n], key1, key2, key3, key4, key5) != 0) break;
                        }
                        index2 = n;
                        return true;
                    }
                }
                return false;
            }
        }
        public bool FindRange<U1, U2, U3, U4, U5>(DCompare<T, U1, U2, U3, U4, U5> d, U1 key1, U2 key2, U3 key3, U4 key4, U5 key5, out int index1, out int index2)
        {
            int r, n, n1, n2;
            index1 = 0; index2 = 0;
            if (Count < 8)
            {
                for (n = Count - 1; n >= 0; --n)
                {
                    r = d(this[n], key1, key2, key3, key4, key5);
                    if (r < 0) return false;
                    if (r == 0) break;
                }
                if (n == -1) return false;
                index2 = n + 1;
                for (--n; n >= 0; --n)
                {
                    if (d(this[n], key1, key2, key3, key4, key5) != 0) break;
                }
                index1 = n + 1;
                return true;
            }
            else
            {
                n1 = 0;
                n2 = Count;
                while (n1 < n2)
                {
                    n = (n1 + n2) >> 1;
                    r = d(this[n], key1, key2, key3, key4, key5);
                    if (r > 0) n2 = n;
                    else if (r < 0) n1 = n + 1;
                    else
                    {
                        n1 = n; n2 = Count;
                        for (n = n1 - 1; n >= 0; --n)
                        {
                            if (d(this[n], key1, key2, key3, key4, key5) != 0) break;
                        }
                        index1 = n + 1;
                        for (n = n1 + 1; n < n2; ++n)
                        {
                            if (d(this[n], key1, key2, key3, key4, key5) != 0) break;
                        }
                        index2 = n;
                        return true;
                    }
                }
                return false;
            }
        }
        public int SearchIndex<U1, U2, U3, U4, U5>(U1 key1, U2 key2, U3 key3, U4 key4, U5 key5)
        {
            int i;
            for (i = Count - 1; i >= 0; --i)
            {
                if (((IComparable<U1, U2, U3, U4, U5>)this[i]).CompareTo(key1, key2, key3, key4, key5) == 0) return i;
            }
            return -1;
        }
        public int SearchIndex<U1, U2, U3, U4, U5>(IComparer<T, U1, U2, U3, U4, U5> c, U1 key1, U2 key2, U3 key3, U4 key4, U5 key5)
        {
            int i;
            for (i = Count - 1; i >= 0; --i)
            {
                if (c.Compare(this[i], key1, key2, key3, key4, key5) == 0) return i;
            }
            return -1;
        }
        public int SearchIndex<U1, U2, U3, U4, U5>(DCompare<T, U1, U2, U3, U4, U5> d, U1 key1, U2 key2, U3 key3, U4 key4, U5 key5)
        {
            int i;
            for (i = Count - 1; i >= 0; --i)
            {
                if (d(this[i], key1, key2, key3, key4, key5) == 0) return i;
            }
            return -1;
        }

        public T Find<U1, U2, U3, U4, U5>(U1 key1, U2 key2, U3 key3, U4 key4, U5 key5)
        {
            int index = FindIndex(key1, key2, key3, key4, key5);
            if (index >= 0) return this[index];
            return default;
        }
        public T Find<U1, U2, U3, U4, U5>(IComparer<T, U1, U2, U3, U4, U5> c, U1 key1, U2 key2, U3 key3, U4 key4, U5 key5)
        {
            int index = FindIndex(c, key1, key2, key3, key4, key5);
            if (index >= 0) return this[index];
            return default;
        }
        public T Find<U1, U2, U3, U4, U5>(DCompare<T, U1, U2, U3, U4, U5> d, U1 key1, U2 key2, U3 key3, U4 key4, U5 key5)
        {
            int index = FindIndex(d, key1, key2, key3, key4, key5);
            if (index >= 0) return this[index];
            return default;
        }
        public T Search<U1, U2, U3, U4, U5>(U1 key1, U2 key2, U3 key3, U4 key4, U5 key5)
        {
            int index = SearchIndex(key1, key2, key3, key4, key5);
            if (index >= 0) return this[index];
            return default;
        }
        public T Search<U1, U2, U3, U4, U5>(IComparer<T, U1, U2, U3, U4, U5> c, U1 key1, U2 key2, U3 key3, U4 key4, U5 key5)
        {
            int index = SearchIndex(c, key1, key2, key3, key4, key5);
            if (index >= 0) return this[index];
            return default;
        }
        public T Search<U1, U2, U3, U4, U5>(DCompare<T, U1, U2, U3, U4, U5> d, U1 key1, U2 key2, U3 key3, U4 key4, U5 key5)
        {
            int index = SearchIndex(d, key1, key2, key3, key4, key5);
            if (index >= 0) return this[index];
            return default;
        }

        public void RemoveSorted<U1, U2, U3, U4, U5>(U1 key1, U2 key2, U3 key3, U4 key4, U5 key5)
        {
            int index = FindIndex(key1, key2, key3, key4, key5);
            if (index >= 0) RemoveAt(index);
        }
        #endregion
    }
}
