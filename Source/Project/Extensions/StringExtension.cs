namespace RegionOrebroLan.Extensions
{
	public static class StringExtension
	{
		#region Methods

		public static string RemoveIfFirst(this string value, char characterToRemove)
		{
			return !string.IsNullOrEmpty(value) && value[0] == characterToRemove ? value.Substring(1) : value;
		}

		#endregion
	}
}