namespace RegionOrebroLan
{
	public interface IParser<T>
	{
		#region Methods

		bool CanParse(string value);
		T Parse(string value);
		bool TryParse(string value, out T result);

		#endregion
	}
}