namespace RegionOrebroLan.Collections
{
	public class Indexed : IIndexed
	{
		#region Constructors

		public Indexed(int index, object value)
		{
			this.Index = index;
			this.Value = value;
		}

		#endregion

		#region Properties

		public virtual bool First => this.Index == 0;
		public virtual int Index { get; }
		public virtual bool Last { get; set; }
		public virtual object Value { get; }

		#endregion
	}
}