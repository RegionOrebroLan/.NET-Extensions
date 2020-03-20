using System;

namespace RegionOrebroLan.Security.Cryptography.Configuration
{
	public class FileResolverOptions : ResolverOptions
	{
		#region Properties

		public virtual string Password { get; set; }
		public virtual string Path { get; set; }

		#endregion

		#region Methods

		public override ICertificate Resolve(ICertificateResolver certificateResolver)
		{
			return (certificateResolver ?? throw new ArgumentNullException(nameof(certificateResolver))).Resolve(this);
		}

		#endregion
	}
}