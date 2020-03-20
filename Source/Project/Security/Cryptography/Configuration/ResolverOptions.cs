namespace RegionOrebroLan.Security.Cryptography.Configuration
{
	public abstract class ResolverOptions
	{
		#region Methods

		public abstract ICertificate Resolve(ICertificateResolver certificateResolver);

		#endregion
	}
}