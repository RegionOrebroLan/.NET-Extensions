using System;
using System.Globalization;
using RegionOrebroLan.Extensions;

namespace RegionOrebroLan
{
	public class UriBuilderWrapper : IUriBuilder
	{
		#region Fields

		private string _fragment;
		private const char _hashSign = '#';
		private string _host;
		private static bool? _isDotNetFrameworkContext;
		private string _password;
		private string _path;
		private int? _port;
		private string _query;
		private const char _questionMark = '?';
		private string _scheme;
		private Lazy<IUri> _uri;
		private string _userName;

		#endregion

		#region Constructors

		public UriBuilderWrapper() { }

		public UriBuilderWrapper(string uniformResourceIdentifier)
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

		public UriBuilderWrapper(IUri uri)
		{
			this.Initialize(uri);
		}

		#endregion

		#region Properties

		public virtual string Fragment
		{
			get => this._fragment;
			set
			{
				if(string.Equals(this._fragment, value, StringComparison.Ordinal))
					return;

				this._fragment = value;
				this.Modified = true;
			}
		}

		protected internal virtual char HashSign => _hashSign;

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

		protected internal virtual bool IsDotNetFrameworkContext
		{
			get
			{
				if(_isDotNetFrameworkContext == null)
					_isDotNetFrameworkContext = typeof(UriBuilder).Assembly.GetName().Name.Equals("System", StringComparison.Ordinal);

				return _isDotNetFrameworkContext.Value;
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
				if(string.Equals(this._query, value, StringComparison.Ordinal))
					return;

				this._query = value;
				this.Modified = true;
			}
		}

		protected internal virtual char QuestionMark => _questionMark;

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
				Fragment = this.IsDotNetFrameworkContext ? this.RemoveIfFirst(this.Fragment, this.HashSign) : this.Fragment,
				Path = this.Path,
				Query = this.IsDotNetFrameworkContext ? this.RemoveIfFirst(this.Query, this.QuestionMark) : this.Query
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
				throw new ArgumentNullException(nameof(uri));

			this._fragment = this.RemoveIfFirst(uri.Fragment, this.HashSign);
			this._path = uri.Path;
			this._query = this.RemoveIfFirst(uri.Query, this.QuestionMark);

			if(uri.IsAbsolute)
			{
				this._host = uri.Host;
				this._port = uri.Port;
				this._scheme = uri.Scheme;

				if(!string.IsNullOrEmpty(uri.UserInformation))
				{
					var parts = uri.UserInformation.Split(':');

					if(parts.Length > 0)
					{
						this._userName = parts[0];

						if(parts.Length > 1)
							this._password = parts[1];
					}
				}
			}
		}

		protected internal virtual string RemoveIfFirst(string value, char characterToRemove)
		{
			return value.RemoveIfFirst(characterToRemove);
		}

		public override string ToString()
		{
			return this.Uri.ToString();
		}

		#endregion
	}
}