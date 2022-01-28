using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace RegionOrebroLan
{
	public class UriInterfaceTypeConverter : TypeConverter
	{
		#region Methods

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if(sourceType == null)
				throw new ArgumentNullException(nameof(sourceType));

			return typeof(IUri).IsAssignableFrom(sourceType) || sourceType == typeof(string) || typeof(Uri).IsAssignableFrom(sourceType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(Uri) || destinationType == typeof(UriWrapper);
		}

		[SuppressMessage("Style", "IDE0010:Convert to conditional expression")]
		[SuppressMessage("Style", "IDE0046:Convert to conditional expression")]
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string uniformResourceIdentifier = null;

			switch(value)
			{
				case string valueAsString:
					uniformResourceIdentifier = valueAsString;
					break;
				case IUri valueAsUri:
					uniformResourceIdentifier = valueAsUri.OriginalString;
					break;
				case Uri valueAsConcreteUri:
					uniformResourceIdentifier = valueAsConcreteUri.OriginalString;
					break;
			}

			// ReSharper disable All
			if(uniformResourceIdentifier != null)
			{
				if(uniformResourceIdentifier.Length == 0)
					return null;

				return new UriWrapper(new Uri(uniformResourceIdentifier, UriKind.RelativeOrAbsolute));
			}
			// ReSharper restore All

			throw this.GetConvertFromException(value);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if(destinationType == null)
				throw new ArgumentNullException(nameof(destinationType));

			// ReSharper disable InvertIf
			if(value is IUri uri)
			{
				if(destinationType == typeof(string))
					return uri.OriginalString;

				if(destinationType == typeof(Uri))
					return new Uri(uri.OriginalString, UriKind.RelativeOrAbsolute);

				if(destinationType == typeof(UriWrapper))
					return new UriWrapper(new Uri(uri.OriginalString, UriKind.RelativeOrAbsolute));
			}
			// ReSharper restore InvertIf

			throw this.GetConvertToException(value, destinationType);
		}

		[SuppressMessage("Style", "IDE0046:Convert to conditional expression")]
		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			if(value is string uniformResourceIdentifier)
				return Uri.TryCreate(uniformResourceIdentifier, UriKind.RelativeOrAbsolute, out _);

			return value is IUri or Uri;
		}

		#endregion
	}
}