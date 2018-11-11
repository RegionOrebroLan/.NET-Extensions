using System;
using System.Globalization;
using RegionOrebroLan.Abstractions;

namespace RegionOrebroLan
{
	public class UriBuilderWrapper : Wrapper<UriBuilder>, IUriBuilder
	{
		#region Fields

		private string _fragment;
		private string _host;
		private string _password;
		private string _path;
		private int? _port;
		private string _query;
		private string _scheme;
		private Lazy<IUri> _uri;
		private string _userName;

		#endregion

		#region Constructors

		public UriBuilderWrapper() : base(new UriBuilder(), "uriBuilder") { }

		public UriBuilderWrapper(string uniformResourceIdentifier) : base(new UriBuilder(), "uriBuilder")
		{
			if(uniformResourceIdentifier == null)
				throw new ArgumentNullException(nameof(uniformResourceIdentifier));

			try
			{
				this.Initialize((UriWrapper) new Uri(uniformResourceIdentifier, UriKind.RelativeOrAbsolute));
			}
			catch(Exception exception)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Could not create an uri from \"{0}\".", uniformResourceIdentifier), nameof(uniformResourceIdentifier), exception);
			}
		}

		public UriBuilderWrapper(IUri uri) : base(new UriBuilder(), "uriBuilder")
		{
			if(uri == null)
				throw new ArgumentNullException(nameof(uri));

			this.Initialize(uri);
		}

		[Obsolete("This constructor will be removed. The inheritance from Wrapper<UriBuilder> will also be removed. Use the other constructors instead. We need to be able to handle relative uri's and UriBuilder do not support that.")]
		public UriBuilderWrapper(UriBuilder uriBuilder) : base(uriBuilder, nameof(uriBuilder))
		{
			this.Initialize((UriWrapper) uriBuilder.Uri);
		}

		#endregion

		#region Properties

		public virtual string Fragment
		{
			get => this._fragment;
			set
			{
				const string hashSign = "#";

				if(!string.IsNullOrEmpty(value) && !value.StartsWith(hashSign, StringComparison.Ordinal))
					value = hashSign + value;

				if(string.Equals(this._fragment, value, StringComparison.Ordinal))
					return;

				this._fragment = value;
				this.Modified = true;
			}
		}

		public virtual string Host
		{
			get => this._host;
			set
			{
				if(string.Equals(this._host, value, StringComparison.Ordinal))
					return;

				this._host = value;
				this.Modified = true;
			}
		}

		protected internal virtual bool IsAbsolute
		{
			get
			{
				var isRelative = this.Host == null && this.Password == null && this.Port == null && this.Scheme == null && this.UserName == null;

				return !isRelative;
			}
		}

		protected internal virtual bool Modified { get; set; }

		public virtual string Password
		{
			get => this._password;
			set
			{
				if(string.Equals(this._password, value, StringComparison.Ordinal))
					return;

				this._password = value;
				this.Modified = true;
			}
		}

		public virtual string Path
		{
			get => this._path;
			set
			{
				if(string.Equals(this._path, value, StringComparison.Ordinal))
					return;

				this._path = value;
				this.Modified = true;
			}
		}

		public virtual int? Port
		{
			get => this._port;
			set
			{
				if(this._port == value)
					return;

				if(value != null)
				{
					// Trigger exception if the value is invalid.
					new UriBuilder().Port = value.Value;
				}

				this._port = value;
				this.Modified = true;
			}
		}

		public virtual string Query
		{
			get => this._query;
			set
			{
				const string questionMark = "?";

				if(!string.IsNullOrEmpty(value) && !value.StartsWith(questionMark, StringComparison.Ordinal))
					value = questionMark + value;

				if(string.Equals(this._query, value, StringComparison.Ordinal))
					return;

				this._query = value;
				this.Modified = true;
			}
		}

		public virtual string Scheme
		{
			get => this._scheme;
			set
			{
				if(string.Equals(this._scheme, value, StringComparison.Ordinal))
					return;

				if(value != null)
				{
					// Trigger exception if the value is invalid.
					new UriBuilder().Scheme = value;
				}

				this._scheme = value;
				this.Modified = true;
			}
		}

		public virtual IUri Uri
		{
			get
			{
				if(this._uri == null || this.Modified)
					this._uri = new Lazy<IUri>(this.CreateUri);

				return this._uri.Value;
			}
		}

		public virtual string UserName
		{
			get => this._userName;
			set
			{
				if(string.Equals(this._userName, value, StringComparison.Ordinal))
					return;

				this._userName = value;
				this.Modified = true;
			}
		}

		#endregion

		#region Methods

		protected internal virtual IUri CreateUri()
		{
			var uriBuilder = new UriBuilder
			{
				Fragment = this.Fragment,
				Path = this.Path,
				Query = this.Query
			};

			if(this.IsAbsolute)
			{
				uriBuilder.Host = this.Host;
				uriBuilder.Password = this.Password;
				uriBuilder.Scheme = this.Scheme;
				uriBuilder.UserName = this.UserName;

				if(this.Port != null)
					uriBuilder.Port = this.Port.Value;
			}

			try
			{
				if(this.IsAbsolute)
				{
					if(string.IsNullOrEmpty(uriBuilder.Host))
						uriBuilder.Host = new UriBuilder().Host;

					if(string.IsNullOrEmpty(uriBuilder.Scheme))
						uriBuilder.Scheme = new UriBuilder().Scheme;

					return (UriWrapper) uriBuilder.Uri;
				}

				const string host = "host";
				const string scheme = "scheme";

				uriBuilder.Host = host;
				uriBuilder.Scheme = scheme;

				var uniformResourceIdentifier = uriBuilder.Uri.OriginalString.Substring(scheme.Length + System.Uri.SchemeDelimiter.Length + host.Length);

				return (UriWrapper) new Uri(uniformResourceIdentifier, UriKind.Relative);
			}
			catch(Exception exception)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Could not get the uri from the uri-builder with the following string-representation: \"{0}\".", uriBuilder), exception);
			}
		}

		protected internal void Initialize(IUri uri)
		{
			if(uri == null)
				return;

			this._fragment = uri.Fragment;
			this._path = uri.Path;
			this._query = uri.Query;

			if(!uri.IsAbsolute)
				return;

			this._host = uri.Host;
			this._port = uri.Port;
			this._scheme = uri.Scheme;

			if(string.IsNullOrEmpty(uri.UserInformation))
				return;

			var parts = uri.UserInformation.Split(':');

			if(parts.Length <= 0)
				return;

			this._userName = parts[0];

			if(parts.Length > 1)
				this._password = parts[1];
		}

		public override string ToString()
		{
			return this.Uri.ToString();
		}

		#endregion
	}
}