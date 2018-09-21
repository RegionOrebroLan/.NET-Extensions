namespace RegionOrebroLan
{
	public interface IApplicationDomain
	{
		#region Properties

		string BaseDirectory { get; }

		#endregion

		#region Methods

		object GetData(string name);
		void SetData(string name, object data);

		#endregion
	}
}