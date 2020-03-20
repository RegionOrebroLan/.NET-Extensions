using System;

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

		#region Methods

		public override ICertificate Resolve(ICertificateResolver certificateResolver)
		{
			return (certificateResolver ?? throw new ArgumentNullException(nameof(certificateResolver))).Resolve(this);
		}

		#endregion
	}
}