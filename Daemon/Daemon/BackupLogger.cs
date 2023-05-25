﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daemon
{
    public class BackupLogger
    {
        public List<LogEntry> LogEntries { get; } = new List<LogEntry>();
        private FileManager manager = new FileManager();
        private BackupReport report = new BackupReport();
        private ApiHandler api = new ApiHandler();

        public async Task LogBackup(BackupConfiguration config, bool backupSuccess, string exceptionMessage = null)
        {
            LogEntry logEntry = new LogEntry
            {
                ConfigId = config.configId,
                StationId = manager.GetStationID(),
                Success = backupSuccess,
            };

            if (logEntry.Success == false && exceptionMessage != null)
            {
                logEntry.errorMessage = exceptionMessage;
            }
            else if (logEntry.Success == false && exceptionMessage == null)
            {
                logEntry.errorMessage = "No error message provided";
            }

            report = report.GenerateBackupReport(logEntry);
            await api.PostReport(report);
        }
    }
}
