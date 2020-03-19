using System;
using System.Collections.Generic;

namespace RegionOrebroLan.Validation
{
	public interface IValidationResult
	{
		#region Properties

		IList<Exception> Exceptions { get; }
		bool Valid { get; }

		#endregion
	}

	public interface IValidationResult<out T> : IValidationResult
	{
		#region Properties

		T ValidatedInstance { get; }

		#endregion
	}
}