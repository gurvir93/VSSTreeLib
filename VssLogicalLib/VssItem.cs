/* Copyright 2009 HPDI, LLC
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using Hpdi.VssPhysicalLib;

namespace Hpdi.VssLogicalLib
{
    /// <summary>
    /// Represents an abstract VSS item, which is a project or file.
    /// </summary>
    /// <author>Trevor Robinson</author>
    public abstract class VssItem
    {
        protected readonly VssDatabase database;
        protected readonly VssItemName itemName;
        protected readonly string physicalPath;
        private ItemFile itemFile;

        public VssDatabase Database
        {
            get { return database; }
        }

        public VssItemName ItemName
        {
            get { return itemName; }
        }

        public bool IsProject
        {
            get { return itemName.IsProject; }
        }

        public string Name
        {
            get { return itemName.LogicalName; }
        }

        public string PhysicalName
        {
            get { return itemName.PhysicalName; }
        }

        public string PhysicalPath
        {
            get { return physicalPath; }
        }

        public string DataPath
        {
            get { return physicalPath + ItemFile.Header.DataExt; }
        }

        public int RevisionCount
        {
            get { return ItemFile.Header.Revisions; }
        }


        internal ItemFile ItemFile
        {
            get
            {
                if (itemFile == null)
                {
                    itemFile = new ItemFile(physicalPath, database.Encoding);
                }
                return itemFile;
            }
            set
            {
                itemFile = value;
            }
        }

        internal VssItem(VssDatabase database, VssItemName itemName, string physicalPath)
        {
            this.database = database;
            this.itemName = itemName;
            this.physicalPath = physicalPath;
        }
    }
}
