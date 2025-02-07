using Elastic.Apm.Config;
using Elastic.Apm.Logging;
using Elastic.Apm.Report;

namespace Elastic.Apm.Tests.Mocks
{
	internal class TestAgentComponents : AgentComponents
	{
		public TestAgentComponents()
			: this(new TestAgentConfigurationReader(new TestLogger(ParseWithoutLogging("Debug")))) { }

		public TestAgentComponents(
			string logLevel = "Debug",
			string serverUrls = null,
			string secretToken = null,
			string captureHeaders = null,
			string transactionSampleRate = null,
			string stackTraceLimit = null,
			string spanFramesMinDurationInMilliseconds = null,

			IPayloadSender payloadSender = null
		)
			: this(new TestAgentConfigurationReader(
				new TestLogger(ParseWithoutLogging(logLevel)),
				serverUrls: serverUrls,
				secretToken: secretToken,
				captureHeaders: captureHeaders,
				logLevel: logLevel,
				transactionSampleRate: transactionSampleRate,
				stackTraceLimit: stackTraceLimit,
				spanFramesMinDurationInMilliseconds: spanFramesMinDurationInMilliseconds
			), payloadSender)
		{
		}

		public TestAgentComponents(TestLogger logger, string serverUrls = null, IPayloadSender payloadSender = null)
			: this(new TestAgentConfigurationReader(logger, serverUrls: serverUrls), payloadSender) { }

		public TestAgentComponents(
			TestAgentConfigurationReader reader,
			IPayloadSender payloadSender = null
		) : base(new FakeMetricsCollector(), reader.Logger, reader, payloadSender ?? new MockPayloadSender()) { }

		protected internal static LogLevel ParseWithoutLogging(string value)
		{
			if (AbstractConfigurationReader.TryParseLogLevel(value, out var level)) return level;
			return ConsoleLogger.DefaultLogLevel;
		}

	}
}
