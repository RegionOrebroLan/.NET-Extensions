namespace RegionOrebroLan
{
	public interface IUriBuilder
	{
		#region Properties

		string Fragment { get; set; }
		string Host { get; set; }
		string Password { get; set; }
		string Path { get; set; }
		int? Port { get; set; }
		string Query { get; set; }
		string Scheme { get; set; }
		IUri Uri { get; }
		string UserName { get; set; }

		#endregion
	}
}