namespace RegionOrebroLan.Security.Cryptography.Configuration
{
	public class StoreResolverOptions : ResolverOptions
	{
		#region Properties

		/// <summary>
		/// Eg. CERT:\CurrentUser\My\{Thumbprint}
		/// </summary>
		public virtual string Path { get; set; }

		public virtual bool ValidOnly { get; set; } = true;

		#endregion
	}
}