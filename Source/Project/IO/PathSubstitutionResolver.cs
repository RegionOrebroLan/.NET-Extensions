using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using RegionOrebroLan.DependencyInjection;
using RegionOrebroLan.Extensions;

namespace RegionOrebroLan.IO
{
	[ServiceConfiguration(ServiceType = typeof(IPathSubstitutionResolver))]
	public class PathSubstitutionResolver : IPathSubstitutionResolver
	{
		#region Constructors

		public PathSubstitutionResolver(IApplicationDomain applicationDomain, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
		{
			this.ApplicationDomain = applicationDomain ?? throw new ArgumentNullException(nameof(applicationDomain));
			this.Comparison = comparison;
		}

		#endregion

		#region Properties

		protected internal virtual IApplicationDomain ApplicationDomain { get; }
		protected internal virtual StringComparison Comparison { get; }

		#endregion

		#region Methods

		[SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters")]
		public virtual async Task<string> ResolveDataDirectoryAsync(string path)
		{
			if(string.IsNullOrWhiteSpace(path))
				return await Task.FromResult(path);

			if(!path.StartsWith(PathSubstitutions.DataDirectory, this.Comparison))
				return path;

			var dataDirectory = this.ApplicationDomain.GetDataDirectory();

			if(dataDirectory == null)
				throw new InvalidOperationException("The data-directory is not set for the application-domain.");

			var slashCharacters = new[] {'/', '\\'};

			return dataDirectory.TrimEnd(slashCharacters) + '\\' + path.Substring(PathSubstitutions.DataDirectory.Length).TrimStart(slashCharacters);
		}

		#endregion
	}
}