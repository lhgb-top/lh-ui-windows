using Microsoft.Build.Framework;

namespace Microsoft.Build.AppxPackage
{
    /// <summary>
    /// GetDefaultResourceLanguage 占位任务：原样回传入参的默认语言（类库无 AppxManifest）。
    /// </summary>
    public sealed class GetDefaultResourceLanguage : ITask
    {
        public string DefaultLanguage { get; set; }

        public ITaskItem[] SourceAppxManifest { get; set; }

        public string VsTelemetrySession { get; set; }

        [Output]
        public string DefaultResourceLanguage { get; set; }

        public IBuildEngine BuildEngine { get; set; }

        public ITaskHost HostObject { get; set; }

        public bool Execute()
        {
            DefaultResourceLanguage = string.IsNullOrEmpty(DefaultLanguage) ? "en-US" : DefaultLanguage;
            return true;
        }
    }
}
