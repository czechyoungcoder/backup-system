﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Daemon
{
    public class BackupConfiguration    
    {
        public int configId { get; set; }
        public List<source> sources { get; set; }
        public List<destination> destinations { get; set; }
        public BackupType BackupType { get; set; }
        public string LastBackupPath { get; set; }
        public string periodCron { get; set; }  
    }
    public class destination
    {
        public string path { get; set; } 
    }
    public class source
    {
        public string path { get; set; }  
    }

}
