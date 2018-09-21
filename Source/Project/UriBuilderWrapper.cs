using System;
using System.Globalization;
using RegionOrebroLan.Abstractions;

namespace RegionOrebroLan
{
	public class UriBuilderWrapper : Wrapper<UriBuilder>, IUriBuilder
	{
		#region Fields

		private const int _portNullValue = -1;

		#endregion

		#region Constructors

		public UriBuilderWrapper(UriBuilder uriBuilder) : base(uriBuilder, nameof(uriBuilder)) { }

		#endregion

		#region Properties

		public virtual string Fragment
		{
			get => this.WrappedInstance.Fragment;
			set => this.WrappedInstance.Fragment = this.RemoveFirstCharacterIfNecessary(value, '#');
		}

		public virtual string Host
		{
			get => this.WrappedInstance.Host;
			set => this.WrappedInstance.Host = value;
		}

		public virtual string Password
		{
			get => this.WrappedInstance.Password;
			set => this.WrappedInstance.Password = value;
		}

		public virtual string Path
		{
			get => this.WrappedInstance.Path;
			set => this.WrappedInstance.Path = value;
		}

		public virtual int? Port
		{
			get => this.WrappedInstance.Port != this.PortNullValue ? (int?) this.WrappedInstance.Port : null;
			set
			{
				if(value == null)
					value = this.PortNullValue;

				this.WrappedInstance.Port = value.Value;
			}
		}

		protected internal virtual int PortNullValue => _portNullValue;

		public virtual string Query
		{
			get => this.WrappedInstance.Query;
			set => this.WrappedInstance.Query = this.RemoveFirstCharacterIfNecessary(value, '?');
		}

		public virtual string Scheme
		{
			get => this.WrappedInstance.Scheme;
			set => this.WrappedInstance.Scheme = value;
		}

		public virtual IUri Uri => (UriWrapper) this.WrappedInstance.Uri;

		public virtual string UserName
		{
			get => this.WrappedInstance.UserName;
			set => this.WrappedInstance.UserName = value;
		}

		#endregion

		#region Methods

		protected internal virtual string RemoveFirstCharacterIfNecessary(string value, char characterToRemove)
		{
			// ReSharper disable InvertIf
			if(value != null)
			{
				var stringToRemove = characterToRemove.ToString(CultureInfo.InvariantCulture);

				if(value.StartsWith(stringToRemove, StringComparison.OrdinalIgnoreCase))
					return value.Substring(stringToRemove.Length);
			}
			// ReSharper restore InvertIf

			return value;
		}

		public override string ToString()
		{
			return this.WrappedInstance.ToString();
		}

		#endregion
	}
}