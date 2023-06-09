﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using BackupSystem.Models;

namespace BackupSystem.Dtos;

public partial class ConfigurationDto
{
    public int ConfigId { get; set; }
    public string? ConfigName { get; set; }
    public string? BackupType { get; set; }
    public int Retention { get; set; }
    public int PackageSize { get; set; }
    public bool Zip { get; set; }
    public bool Periodic { get; set; }
    public bool Finished { get; set; }
    public string? PeriodCron { get; set; }
    public virtual ICollection<BackupDestinationDto> Destinations { get; set; } = new List<BackupDestinationDto>();
    public virtual ICollection<BackupSourceDto> Sources { get; set; } = new List<BackupSourceDto>();
    public virtual ICollection<int> Stations { get; set; } = new List<int>();
    public virtual ICollection<int> Groups { get; set; } = new List<int>();
}