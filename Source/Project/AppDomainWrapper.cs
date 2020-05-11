using System;
using RegionOrebroLan.Abstractions;
using RegionOrebroLan.ServiceLocation;

namespace RegionOrebroLan
{
#pragma warning disable CS0618 // Type or member is obsolete
	[ServiceConfiguration(InstanceMode = InstanceMode.Singleton, ServiceType = typeof(IApplicationDomain))]
#pragma warning restore CS0618 // Type or member is obsolete
	public class AppDomainWrapper : Wrapper<AppDomain>, IApplicationDomain
	{
		#region Constructors

		public AppDomainWrapper(AppDomain appDomain) : base(appDomain, nameof(appDomain)) { }

		#endregion

		#region Properties

		public virtual string BaseDirectory => this.WrappedInstance.BaseDirectory;

		#endregion

		#region Methods

		public virtual object GetData(string name)
		{
			return this.WrappedInstance.GetData(name);
		}

		#region Implicit operator

		public static implicit operator AppDomainWrapper(AppDomain appDomain)
		{
			return appDomain != null ? new AppDomainWrapper(appDomain) : null;
		}

		#endregion

		public virtual void SetData(string name, object data)
		{
			this.WrappedInstance.SetData(name, data);
		}

		public static AppDomainWrapper ToAppDomainWrapper(AppDomain appDomain)
		{
			return appDomain;
		}

		#endregion
	}
}