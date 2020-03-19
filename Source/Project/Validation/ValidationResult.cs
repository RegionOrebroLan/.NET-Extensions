using System;
using System.Collections.Generic;
using System.Linq;

namespace RegionOrebroLan.Validation
{
	public class ValidationResult : IValidationResult
	{
		#region Properties

		public virtual IList<Exception> Exceptions { get; } = new List<Exception>();
		public virtual bool Valid => !this.Exceptions.Any();

		#endregion
	}

	public class ValidationResult<T> : ValidationResult, IValidationResult<T>
	{
		#region Properties

		public virtual T ValidatedInstance { get; set; }

		#endregion
	}
}