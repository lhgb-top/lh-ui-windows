using System.Linq;
using Microsoft.Build.Framework;

namespace Microsoft.Build.AppxPackage
{
    /// <summary>
    /// RemovePayloadDuplicates 占位任务：按 ItemSpec 去重后透传。
    /// 官方任务还会规范化 ..\..\ 风格的路径；占位实现假设输入已规范化（NuGet 解析的路径通常如此）。
    /// </summary>
    public sealed class RemovePayloadDuplicates : ITask
    {
        [Required]
        public ITaskItem[] Inputs { get; set; }

        public string ProjectName { get; set; }

        public string Platform { get; set; }

        public string VsTelemetrySession { get; set; }

        [Output]
        public ITaskItem[] Filtered { get; set; }

        public IBuildEngine BuildEngine { get; set; }

        public ITaskHost HostObject { get; set; }

        public bool Execute()
        {
            Filtered = (Inputs ?? System.Array.Empty<ITaskItem>())
                .GroupBy(i => i.ItemSpec)
                .Select(g => g.First())
                .ToArray();
            return true;
        }
    }
}
