using System;
using Microsoft.Extensions.Hosting;

namespace RegionOrebroLan
{
	public class ApplicationHost : IApplicationDomain
	{
		#region Constructors

		[CLSCompliant(false)]
		public ApplicationHost(AppDomain appDomain, IHostEnvironment hostEnvironment)
		{
			this.AppDomain = appDomain ?? throw new ArgumentNullException(nameof(appDomain));
			this.HostEnvironment = hostEnvironment ?? throw new ArgumentNullException(nameof(hostEnvironment));
		}

		#endregion

		#region Properties

		protected internal virtual AppDomain AppDomain { get; }
		public virtual string BaseDirectory => this.HostEnvironment.ContentRootPath;

		[CLSCompliant(false)]
		protected internal virtual IHostEnvironment HostEnvironment { get; }

		#endregion

		#region Methods

		public virtual object GetData(string name)
		{
			return this.AppDomain.GetData(name);
		}

		public virtual void SetData(string name, object data)
		{
			this.AppDomain.SetData(name, data);
		}

		#endregion
	}
}