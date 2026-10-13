using Microsoft.Build.Framework;

namespace Microsoft.Build.AppxPackage
{
    /// <summary>
    /// ExpandPayloadDirectories 占位任务：把目录形式的 payload 项展开为文件项。
    /// 占位直接透传输入——类库场景的 payload 项已是无目录的解析路径。
    /// </summary>
    public sealed class ExpandPayloadDirectories : ITask
    {
        [Required]
        public ITaskItem[] Inputs { get; set; }

        public string VsTelemetrySession { get; set; }

        [Output]
        public ITaskItem[] Expanded { get; set; }

        public IBuildEngine BuildEngine { get; set; }

        public ITaskHost HostObject { get; set; }

        public bool Execute()
        {
            Expanded = Inputs ?? System.Array.Empty<ITaskItem>();
            return true;
        }
    }
}
