﻿using System;

namespace RegionOrebroLan
{
	public interface IDateTimeContext
	{
		#region Properties

		DateTime Now { get; }
		DateTime UtcNow { get; }

		#endregion
	}
}