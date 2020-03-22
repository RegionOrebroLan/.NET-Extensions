using System.Security.Cryptography.X509Certificates;
using RegionOrebroLan.Abstractions;

namespace RegionOrebroLan.Security.Cryptography
{
	public class X509CertificateWrapper : X509CertificateWrapper<X509Certificate>
	{
		#region Constructors

		public X509CertificateWrapper(X509Certificate certificate) : base(certificate) { }

		#endregion

		#region Methods

		public static X509CertificateWrapper FromX509Certificate(X509Certificate certificate)
		{
			return certificate;
		}

		#region Implicit operators

		public static implicit operator X509CertificateWrapper(X509Certificate certificate)
		{
			return certificate != null ? new X509CertificateWrapper(certificate) : null;
		}

		#endregion

		#endregion
	}

	public abstract class X509CertificateWrapper<T> : Wrapper<T> where T : X509Certificate
	{
		#region Constructors

		protected X509CertificateWrapper(T certificate) : base(certificate, nameof(certificate)) { }

		#endregion

		#region Properties

		public virtual string Issuer => this.WrappedInstance.Issuer;
		public virtual string Subject => this.WrappedInstance.Subject;

		#endregion

		#region Methods

		public virtual void Dispose()
		{
			this.WrappedInstance.Dispose();
		}

		#endregion
	}
}