using System.Diagnostics.CodeAnalysis;

namespace RegionOrebroLan
{
	public abstract class BasicParser<T> : IParser<T>
	{
		#region Methods

		public virtual bool CanParse(string value)
		{
			return this.TryParse(value, out _);
		}

		public abstract T Parse(string value);

		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public virtual bool TryParse(string value, out T result)
		{
			try
			{
				result = this.Parse(value);
				return true;
			}
			catch
			{
				result = default(T);
				return false;
			}
		}

		#endregion
	}
}