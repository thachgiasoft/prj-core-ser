using System.Collections;

namespace Vietbait.Lablink.TestResult
{
    public class ResultItemCollection : CollectionBase
    {
        #region Properties

        public ResultItem this[int index]
        {
            get { return ((ResultItem) List[index]); }

            set { List[index] = value; }
        }

        public int Add(ResultItem val)
        {
            return (List.Add(val));
        }

        public new void Clear()
        {
            List.Clear();
        }

        public new void RemoveAt(int index)
        {
            List.RemoveAt(index);
        }

        #endregion
    }
}