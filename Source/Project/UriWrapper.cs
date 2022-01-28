using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using RegionOrebroLan.Abstractions;

namespace RegionOrebroLan
{
	[Serializable]
	public class UriWrapper : Wrapper<Uri>, ISerializable, IUri
	{
		#region Fields

		[NonSerialized] private const string _internalAbsoluteHost = "883b7dc2-9327-40fb-9021-1ae8f0de5f8c-32ba188f-85ba-4313-ab57-4b4ceb6ae0ac-7fc88cbd-7a25-406d-bb20-005afa831992";
		[NonSerialized] private const string _internalAbsoluteScheme = "c5246af8-dd31-47ed-9e40-1faa137e34a6-54e61c8f-4090-4204-88e4-fbe6c4744bd5-8927128d-6f41-41a7-ba60-6238ed9e9e44";
		[NonSerialized] private Uri _internalAbsoluteUri;
		[NonSerialized] private const int _portNullValue = -1;
		[NonSerialized] private const string _wrappedInstanceSerializationParameterName = "WrappedInstance";

		#endregion

		#region Constructors

		public UriWrapper(Uri uri) : base(uri, nameof(uri)) { }

		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters")]
		protected UriWrapper(SerializationInfo info, StreamingContext context) : this((Uri)ValidateSerializationInfo(info).GetValue(_wrappedInstanceSerializationParameterName, typeof(Uri))) { }

		#endregion

		#region Properties

		public virtual string Authority => this.IsAbsolute ? this.WrappedInstance.Authority : null;
		public virtual string DnsSafeHost => this.IsAbsolute ? this.WrappedInstance.DnsSafeHost : null;
		public virtual string Fragment => this.InternalAbsoluteUri.Fragment;
		public virtual string Host => this.IsAbsolute ? this.WrappedInstance.Host : null;
		public virtual UriHostNameType? HostNameType => this.IsAbsolute ? this.WrappedInstance.HostNameType : null;
		public virtual string IdnHost => this.IsAbsolute ? this.WrappedInstance.IdnHost : null;
		protected internal virtual string InternalAbsoluteHost => _internalAbsoluteHost;
		protected internal virtual string InternalAbsoluteScheme => _internalAbsoluteScheme;

		protected internal virtual Uri InternalAbsoluteUri
		{
			get
			{
				if(this._internalAbsoluteUri == null)
					this._internalAbsoluteUri = this.IsAbsolute ? this.WrappedInstance : new Uri(this.InternalAbsoluteScheme + Uri.SchemeDelimiter + this.InternalAbsoluteHost + (this.WrappedInstance.OriginalString.StartsWith("/", StringComparison.OrdinalIgnoreCase) ? string.Empty : "/") + this.WrappedInstance.OriginalString, UriKind.Absolute);

				return this._internalAbsoluteUri;
			}
		}

		public virtual bool IsAbsolute => this.WrappedInstance.IsAbsoluteUri;
		public virtual bool IsDefaultPort => this.IsAbsolute && this.WrappedInstance.IsDefaultPort && this.WrappedInstance.Port != this.PortNullValue;
		public virtual bool IsFile => this.InternalAbsoluteUri.IsFile;
		public virtual bool IsLoopback => !this.IsAbsolute || this.WrappedInstance.IsLoopback;
		public virtual bool IsUnc => this.IsAbsolute && this.WrappedInstance.IsUnc;
		public virtual string LocalPath => this.InternalAbsoluteUri.LocalPath;
		public virtual string OriginalString => this.WrappedInstance.OriginalString;
		public virtual string Path => this.InternalAbsoluteUri.AbsolutePath;
		public virtual string PathAndQuery => this.InternalAbsoluteUri.PathAndQuery;
		public virtual int? Port => !this.IsAbsolute || this.WrappedInstance.Port == this.PortNullValue ? null : this.WrappedInstance.Port;
		protected internal virtual int PortNullValue => _portNullValue;
		public virtual string Query => this.InternalAbsoluteUri.Query;
		public virtual string Scheme => this.IsAbsolute ? this.WrappedInstance.Scheme : null;
		public virtual IEnumerable<string> Segments => this.InternalAbsoluteUri.Segments;
		public virtual bool UserEscaped => this.WrappedInstance.UserEscaped;
		public virtual string UserInformation => this.IsAbsolute ? this.WrappedInstance.UserInfo : null;

		#endregion

		#region Methods

		protected internal virtual Uri AsConcreteUri(IUri uri)
		{
			Uri concreteUri = null;

			// ReSharper disable All
			if(uri != null)
			{
				concreteUri = (uri as IWrapper)?.WrappedInstance as Uri;

				if(concreteUri == null)
					concreteUri = new Uri(uri.OriginalString, UriKind.RelativeOrAbsolute);
			}
			// ReSharper restore All

			return concreteUri;
		}

		public virtual string GetComponents(UriComponents components, UriFormat format)
		{
			return this.IsAbsolute ? this.WrappedInstance.GetComponents(components, format) : this.GetRelativeComponents(components, format);
		}

		public virtual string GetLeftPart(UriPartial part)
		{
			return this.IsAbsolute ? this.WrappedInstance.GetLeftPart(part) : this.GetRelativeLeftPart(part);
		}

		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if(info == null)
				throw new ArgumentNullException(nameof(info));

			info.AddValue(_wrappedInstanceSerializationParameterName, this.WrappedInstance);
		}

		protected internal virtual string GetRelativeComponents(UriComponents components, UriFormat format)
		{
			var relativeComponents = this.GetRelativeResult(this.InternalAbsoluteUri.GetComponents(components, format));

			return !string.IsNullOrEmpty(relativeComponents) ? relativeComponents : null;
		}

		[SuppressMessage("Style", "IDE0046:Convert to conditional expression")]
		protected internal virtual string GetRelativeLeftPart(UriPartial part)
		{
			if(part is UriPartial.Authority or UriPartial.Scheme)
				return null;

			return this.GetRelativeResult(this.InternalAbsoluteUri.GetLeftPart(part));
		}

		protected internal virtual string GetRelativeResult(string uniformResourceIdentifier)
		{
			// ReSharper disable InvertIf
			if(!string.IsNullOrWhiteSpace(uniformResourceIdentifier))
			{
				foreach(var value in new[] { this.InternalAbsoluteScheme, Uri.SchemeDelimiter, this.InternalAbsoluteHost })
				{
					if(uniformResourceIdentifier.StartsWith(value, StringComparison.Ordinal))
						uniformResourceIdentifier = uniformResourceIdentifier.Substring(value.Length);
				}
			}
			// ReSharper restore InvertIf

			return uniformResourceIdentifier;
		}

		public virtual bool IsBaseOf(IUri uri)
		{
			return this.WrappedInstance.IsBaseOf(this.AsConcreteUri(uri));
		}

		public virtual bool IsWellFormedOriginalString()
		{
			return this.InternalAbsoluteUri.IsWellFormedOriginalString();
		}

		#region Implicit operators

		public static implicit operator UriWrapper(Uri uri)
		{
			return uri != null ? new UriWrapper(uri) : null;
		}

		#endregion

		public override string ToString()
		{
			return this.WrappedInstance.ToString();
		}

		public static UriWrapper ToUriWrapper(Uri uri)
		{
			return uri;
		}

		private static SerializationInfo ValidateSerializationInfo(SerializationInfo info)
		{
			if(info == null)
				throw new ArgumentNullException(nameof(info));

			return info;
		}

		#endregion
	}
}