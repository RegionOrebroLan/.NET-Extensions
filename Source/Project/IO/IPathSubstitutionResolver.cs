using System.Threading.Tasks;

namespace RegionOrebroLan.IO
{
	public interface IPathSubstitutionResolver
	{
		#region Methods

		Task<string> ResolveDataDirectoryAsync(string path);

		#endregion
	}
}