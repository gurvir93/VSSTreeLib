using System;
using System.Collections.Generic;
using System.IO;
using Hpdi.VssLogicalLib;
using Hpdi.VssPhysicalLib;

namespace VSSTestStub
{
    class Program
    {
        static void Main(string[] args)
        {
            var df = new VssDatabaseFactory(@"C:\VSS Test Database\CPSC 594 Database 1");
            var db = df.Open();

            var tree = new TreeWriter(Console.Out);
            tree.DumpProject(db.RootProject);
        }
    }

    class TreeWriter
    {
        private readonly TextWriter writer;

        private readonly HashSet<string> physicalNames = new HashSet<string>();
        public HashSet<string> PhysicalNames
        {
            get { return physicalNames; }
        }

        public TreeWriter(TextWriter writer)
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
            writer.WriteLine("{0}{1}/",
                indentStr, project.Name);

            foreach (VssProject subproject in project.Projects)
            {
                DumpProject(subproject, indent + 2);
            }

            foreach (VssFile file in project.Files)
            {
                physicalNames.Add(file.PhysicalName);
                writer.WriteLine("{0}  {1} - {2}",
                    indentStr, file.Name, file.GetPath(project));
            }
        }
    }
}