namespace HISPlus
{
    using System.Collections;
    using System.Collections.Generic;

    public class DocNodeCollection : IList<BaseNode>,IEnumerable<BaseNode>
    {
        private readonly List<BaseNode> _items;
        private readonly IDocNodeParent _parentNode;

        public DocNodeCollection(IDocNodeParent parentNode)
        {
            this._parentNode = parentNode;
            this._items = new List<BaseNode>();
        }

        public void Add(BaseNode item)
        {
            item.Parent = this._parentNode;
            this._items.Add(item);
        }        

        public void Clear()
        {
            this._items.Clear();
        }

        public bool Contains(BaseNode item)
        {
            return this._items.Contains(item);
        }

        public void CopyTo(BaseNode[] array, int arrayIndex)
        {
            this._items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<BaseNode> GetEnumerator()
        {
            return this._items.GetEnumerator();
        }

        public int IndexOf(BaseNode item)
        {
            return this._items.IndexOf(item);
        }

        public void Insert(int index, BaseNode item)
        {
            item.Parent = this._parentNode;
            this._items.Insert(index, item);
        }

        public bool Remove(BaseNode item)
        {
            return this._items.Remove(item);
        }

        public void RemoveAt(int index)
        {
            this._items.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._items.GetEnumerator();
        }

        public int Count
        {
            get
            {
                return this._items.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }          
        }

        public BaseNode this[int index]
        {
            get
            {
                return this._items[index];
            }
            set
            {
                this._items[index] = value;
            }
        }
    }
}

