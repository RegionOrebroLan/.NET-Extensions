using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using RegionOrebroLan.Abstractions;
using RegionOrebroLan.Extensions;

namespace RegionOrebroLan
{
	public class UriBuilderWrapper : Wrapper<UriBuilder>, IUriBuilder
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

		public UriBuilderWrapper() : base(new UriBuilder(), "uriBuilder") { }

		public UriBuilderWrapper(string uniformResourceIdentifier) : base(new UriBuilder(), "uriBuilder")
		{
			if(uniformResourceIdentifier == null)
				throw new ArgumentNullException(nameof(uniformResourceIdentifier));

			try
			{
				this.InitialUri = (UriWrapper) new Uri(uniformResourceIdentifier, UriKind.RelativeOrAbsolute);
			}
			catch(Exception exception)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Could not create an uri from \"{0}\".", uniformResourceIdentifier), nameof(uniformResourceIdentifier), exception);
			}
		}

		public UriBuilderWrapper(IUri uri) : base(new UriBuilder(), "uriBuilder")
		{
			this.InitialUri = uri ?? throw new ArgumentNullException(nameof(uri));
		}

		[Obsolete("This constructor will be removed. The inheritance from Wrapper<UriBuilder> will also be removed. Use the other constructors instead. We need to be able to handle relative uri's and UriBuilder do not support that.")]
		public UriBuilderWrapper(UriBuilder uriBuilder) : base(uriBuilder, nameof(uriBuilder))
		{
			this.InitialUri = (UriWrapper) uriBuilder.Uri;
		}

		#endregion

		#region Properties

		public virtual string Fragment
		{
			get
			{
				this.InitializeIfNecessary();
				return this._fragment;
			}
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
			get
			{
				this.InitializeIfNecessary();
				return this._host;
			}
			set
			{
				if(string.Equals(this._host, value, StringComparison.Ordinal))
					return;

				this._host = value;
				this.Modified = true;
			}
		}

		protected internal virtual bool Initialized { get; set; }
		protected internal virtual IUri InitialUri { get; }

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
			get
			{
				this.InitializeIfNecessary();
				return this._password;
			}
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
			get
			{
				this.InitializeIfNecessary();
				return this._path;
			}
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
			get
			{
				this.InitializeIfNecessary();
				return this._port;
			}
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
			get
			{
				this.InitializeIfNecessary();
				return this._query;
			}
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
			get
			{
				this.InitializeIfNecessary();
				return this._scheme;
			}
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
				this.InitializeIfNecessary();

				if(this._uri == null || this.Modified)
					this._uri = new Lazy<IUri>(this.CreateUri);

				return this._uri.Value;
			}
		}

		public virtual string UserName
		{
			get
			{
				this.InitializeIfNecessary();
				return this._userName;
			}
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

		protected internal virtual void Initialize()
		{
			if(this.Initialized)
				throw new InvalidOperationException("The uri-builder-wrapper is already initialized.");

			if(this.InitialUri == null)
				return;

			this.Fragment = this.RemoveIfFirst(this.InitialUri.Fragment, this.HashSign);
			this.Path = this.InitialUri.Path;
			this.Query = this.RemoveIfFirst(this.InitialUri.Query, this.QuestionMark);

			if(this.InitialUri.IsAbsolute)
			{
				this.Host = this.InitialUri.Host;
				this.Port = this.InitialUri.Port;
				this.Scheme = this.InitialUri.Scheme;

				if(!string.IsNullOrEmpty(this.InitialUri.UserInformation))
				{
					var parts = this.InitialUri.UserInformation.Split(':');

					if(parts.Length > 0)
					{
						this.UserName = parts[0];

						if(parts.Length > 1)
							this.Password = parts[1];
					}
				}
			}

			this.Modified = false;

			this.Initialized = true;
		}

		[Obsolete("This method will be removed. Initialization handled differently. Initialization is no longer called from the constructor. The initialization is called from the \"getters\" only if necessary.")]
		[SuppressMessage("Usage", "CA1801:Review unused parameters")]
		[SuppressMessage("Performance", "CA1822:Mark members as static")]
		protected internal void Initialize(IUri uri) { }

		protected internal virtual void InitializeIfNecessary()
		{
			if(this.Initialized)
				return;

			this.Initialize();
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