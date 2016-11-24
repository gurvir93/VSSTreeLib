using System.Collections.Generic;
using System.IO;



namespace Hpdi.VSSTest
{
    public class VSSTestOut
    {
        public static void main(String[] args)
        {
            var df = new VssDatabaseFactory(@"C:\VSS Test Database\CPSC 594 Database 1");
            var db = df.Open();

            var tree = new TreeDumper(Console.Out) { IncludeRevisions = false };
            tree.DumpProject(db.RootProject);
        }
    }
    class TreeDumper
    {
        private readonly TextWriter writer;

        private readonly HashSet<string> physicalNames = new HashSet<string>();
        public HashSet<string> PhysicalNames
        {
            get { return physicalNames; }
        }

        public bool IncludeRevisions { get; set; }

        public TreeDumper(TextWriter writer)
        {
            this.writer = writer;
        }

        public void DumpProject(VssProject project)
        {
            DumpProject(project, 0);
        }

        public void DumpProject(VssProject project, int indent)
        {
            var indentStr = new string(' ', indent);

            physicalNames.Add(project.PhysicalName);
            writer.WriteLine("{0}{1}/ ({2})",
                indentStr, project.Name, project.PhysicalName);

            foreach (VssProject subproject in project.Projects)
            {
                DumpProject(subproject, indent + 2);
            }

            foreach (VssFile file in project.Files)
            {
                physicalNames.Add(file.PhysicalName);
                writer.WriteLine("{0}  {1} ({2}) - {3}",
                    indentStr, file.Name, file.PhysicalName, file.GetPath(project));

                if (IncludeRevisions)
                {
                    foreach (VssFileRevision version in file.Revisions)
                    {
                        writer.WriteLine("{0}    #{1} {2} {3}",
                            indentStr, version.Version, version.User, version.DateTime);
                    }
                }
            }
        }
    }
}
