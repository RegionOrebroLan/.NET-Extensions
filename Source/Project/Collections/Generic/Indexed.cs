namespace RegionOrebroLan.Collections.Generic
{
	public class Indexed<T> : Indexed, IIndexed<T>
	{
		#region Constructors

		public Indexed(int index, T value) : base(index, value) { }

		#endregion

		#region Properties

		public new virtual T Value => (T) base.Value;

		#endregion
	}
}