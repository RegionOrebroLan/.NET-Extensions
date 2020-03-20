using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace RegionOrebroLan.Security.Cryptography
{
	public class X509Certificate2Wrapper : X509CertificateWrapper<X509Certificate2>, ICertificate
	{
		#region Constructors

		public X509Certificate2Wrapper(X509Certificate2 certificate) : base(certificate) { }

		#endregion

		#region Properties

		public virtual bool Archived => this.WrappedInstance.Archived;
		public virtual string FriendlyName => this.WrappedInstance.FriendlyName;
		public virtual bool HasPrivateKey => this.WrappedInstance.HasPrivateKey;
		public virtual IDistinguishedName IssuerName => (X500DistinguishedNameWrapper) this.WrappedInstance.IssuerName;
		public virtual DateTime NotAfter => this.WrappedInstance.NotAfter;
		public virtual DateTime NotBefore => this.WrappedInstance.NotBefore;
		public virtual IEnumerable<byte> RawData => this.WrappedInstance.RawData;
		public virtual string SerialNumber => this.WrappedInstance.SerialNumber;
		public virtual IOid SignatureAlgorithm => (OidWrapper) this.WrappedInstance.SignatureAlgorithm;
		public virtual IDistinguishedName SubjectName => (X500DistinguishedNameWrapper) this.WrappedInstance.SubjectName;
		public virtual string Thumbprint => this.WrappedInstance.Thumbprint;
		public virtual int Version => this.WrappedInstance.Version;

		#endregion

		#region Methods

		public static X509Certificate2Wrapper FromX509Certificate2(X509Certificate2 certificate)
		{
			return certificate;
		}

		#region Implicit operators

		public static implicit operator X509Certificate2Wrapper(X509Certificate2 certificate)
		{
			return certificate != null ? new X509Certificate2Wrapper(certificate) : null;
		}

		#endregion

		#endregion
	}
}