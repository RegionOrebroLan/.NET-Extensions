using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RegionOrebroLan
{
	[TypeConverter(typeof(UriInterfaceTypeConverter))]
	public interface IUri
	{
		#region Properties

		string Authority { get; }
		string DnsSafeHost { get; }
		string Fragment { get; }
		string Host { get; }
		UriHostNameType? HostNameType { get; }
		string IdnHost { get; }
		bool IsAbsolute { get; }
		bool IsDefaultPort { get; }
		bool IsFile { get; }
		bool IsLoopback { get; }
		bool IsUnc { get; }
		string LocalPath { get; }
		string OriginalString { get; }
		string Path { get; }
		string PathAndQuery { get; }
		int? Port { get; }
		string Query { get; }
		string Scheme { get; }
		IEnumerable<string> Segments { get; }
		bool UserEscaped { get; }
		string UserInformation { get; }

		#endregion

		#region Methods

		string GetComponents(UriComponents components, UriFormat format);
		string GetLeftPart(UriPartial part);
		bool IsBaseOf(IUri uri);
		bool IsWellFormedOriginalString();

		#endregion
	}
}