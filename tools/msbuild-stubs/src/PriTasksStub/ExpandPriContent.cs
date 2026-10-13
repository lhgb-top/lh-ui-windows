using System.Linq;
using Microsoft.Build.Framework;

namespace Microsoft.Build.Packaging.Pri.Tasks
{
    /// <summary>
    /// ExpandPriContent 占位任务：PRI 展开交给官方 MakePri 工具，占位直接透传输入项。
    /// </summary>
    public sealed class ExpandPriContent : ITask
    {
        [Required]
        public ITaskItem[] Inputs { get; set; }

        public string MakePriExeFullPath { get; set; }

        public string MakePriExtensionPath { get; set; }

        public string IntermediateDirectory { get; set; }

        public string AdditionalMakepriExeParameters { get; set; }

        public string ExcludeXamlFromLibraryLayoutsWhenXbfIsPresent { get; set; }

        public string VsTelemetrySession { get; set; }

        [Output]
        public ITaskItem[] Expanded { get; set; }

        [Output]
        public ITaskItem[] IntermediateFileWrites { get; set; }

        public IBuildEngine BuildEngine { get; set; }

        public ITaskHost HostObject { get; set; }

        public bool Execute()
        {
            Expanded = Inputs ?? System.Array.Empty<ITaskItem>();
            IntermediateFileWrites = System.Array.Empty<ITaskItem>();
            return true;
        }
    }
}
