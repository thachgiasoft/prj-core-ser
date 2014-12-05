namespace Vietbait.Config
{
    internal class ItemSettings
	{
		
		private ItemCollection _items;

		public ItemCollection Items
		{
			get
			{
				if (_items == null) { _items = new ItemCollection(); }
				return _items;
			}
			set{_items = value;}
		}
    }
}
