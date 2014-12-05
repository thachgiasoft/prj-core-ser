using System.Collections;

namespace Vietbait.Config
{
    internal sealed class ItemCollection : CollectionBase
    {
        #region Public methods

        public int Add(Item val)
        {
            return (List.Add(val));
        }

        public new void Clear()
        {
            List.Clear();
        }

        public int IndexOf(Item val)
        {
            return (List.IndexOf(val));
        }

        public void Insert(int index, Item val)
        {
            List.Insert(index, val);
        }

        public void Remove(Item val)
        {
            List.Remove(val);
        }

        public new void RemoveAt(int index)
        {
            List.RemoveAt(index);
        }

        public void Replace(Item oldItem, Item newItem)
        {
            int index = IndexOf(oldItem);
            RemoveAt(index);
            Insert(index, newItem);
        }

        public bool Contains(Item val)
        {
            return (List.Contains(val));
        }

        #endregion

        public Item this[int index]
        {
            get { return ((Item) List[index]);}

            set { List[index] = value; }
        }
    }
}