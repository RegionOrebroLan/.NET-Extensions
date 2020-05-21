namespace RegionOrebroLan.Security.Cryptography.Configuration
{
	public class FileResolverOptions : ResolverOptions
	{
		#region Properties

		public virtual string Password { get; set; }
		public virtual string Path { get; set; }

		#endregion
	}
}