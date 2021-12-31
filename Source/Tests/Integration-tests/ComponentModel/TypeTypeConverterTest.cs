using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.ComponentModel;
using RegionOrebroLan.IntegrationTests.ComponentModel.Mocks;

namespace RegionOrebroLan.IntegrationTests.ComponentModel
{
	[TestClass]
	public class TypeTypeConverterTest
	{
		#region Fields

		private static TypeDescriptionProvider _typeDescriptionProvider;

		#endregion

		#region Methods

		[ClassCleanup]
		public static async Task CleanupAsync()
		{
			await Task.CompletedTask.ConfigureAwait(false);

			TypeDescriptor.RemoveProvider(_typeDescriptionProvider, typeof(Type));
		}

		protected internal virtual async Task<IConfiguration> CreateConfigurationAsync(string jsonFileName)
		{
			var configurationBuilder = new ConfigurationBuilder()
				.SetBasePath(Global.ProjectDirectoryPath)
				.AddJsonFile($"ComponentModel\\Resources\\{jsonFileName}.json");

			return await Task.FromResult(configurationBuilder.Build()).ConfigureAwait(false);
		}

		[ClassInitialize]
		public static async Task InitializeAsync(TestContext _)
		{
			await Task.CompletedTask.ConfigureAwait(false);

			_typeDescriptionProvider = TypeDescriptor.AddAttributes(typeof(Type), new TypeConverterAttribute(typeof(TypeTypeConverter)));
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public async Task Options_Bind_IfTypeIsAnEmptyString_ShouldThrowAnInvalidOperationException()
		{
			var configuration = await this.CreateConfigurationAsync("appsettings.Invalid").ConfigureAwait(false);

			var options = new TypeOptionsMock();

			try
			{
				configuration.GetSection("TypeOptionsMock1").Bind(options);
			}
			catch(InvalidOperationException invalidOperationException)
			{
				if(string.Equals(invalidOperationException.Message, "Failed to convert '' to type 'System.Type'.", StringComparison.Ordinal))
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public async Task Options_Bind_IfTypeIsInvalid_ShouldThrowAnInvalidOperationException()
		{
			var configuration = await this.CreateConfigurationAsync("appsettings.Invalid").ConfigureAwait(false);

			var options = new TypeOptionsMock();

			try
			{
				configuration.GetSection("TypeOptionsMock2").Bind(options);
			}
			catch(InvalidOperationException invalidOperationException)
			{
				if(string.Equals(invalidOperationException.Message, "Failed to convert 'zzzz' to type 'System.Type'.", StringComparison.Ordinal))
					throw;
			}
		}

		[TestMethod]
		public async Task Options_Bind_Test()
		{
			var configuration = await this.CreateConfigurationAsync("appsettings.Valid").ConfigureAwait(false);

			var options = new TypeOptionsMock();
			configuration.GetSection("TypeOptionsMock1").Bind(options);
			Assert.AreEqual(typeof(string), options.Type);

			options = new TypeOptionsMock();
			configuration.GetSection("TypeOptionsMock2").Bind(options);
			Assert.AreEqual(this.GetType(), options.Type);
		}

		[TestMethod]
		public async Task Options_Bind_WithoutTypeTypeConverter_Test()
		{
			TypeDescriptor.RemoveProvider(_typeDescriptionProvider, typeof(Type));
			TypeDescriptor.Refresh(typeof(Type));

			//Thread.Sleep(2000);

			var typeConverter = TypeDescriptor.GetConverter(typeof(Type));
			Assert.AreEqual(typeof(TypeConverter), typeConverter.GetType());

			var configuration = await this.CreateConfigurationAsync("appsettings.Invalid").ConfigureAwait(false);

			var options = new TypeOptionsMock();
			configuration.GetSection("TypeOptionsMock1").Bind(options);
			Assert.IsNull(options.Type);

			_typeDescriptionProvider = TypeDescriptor.AddAttributes(typeof(Type), new TypeConverterAttribute(typeof(TypeTypeConverter)));
		}

		#endregion
	}
}