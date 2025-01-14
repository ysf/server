﻿using System;
using System.ComponentModel.DataAnnotations;
using Bit.Core.Models;
using Bit.Core.Utilities;

namespace Bit.Core.Entities
{
    public class Group : ITableObject<Guid>, IExternal
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public bool AccessAll { get; set; }
        [MaxLength(300)]
        public string ExternalId { get; set; }
        public DateTime CreationDate { get; internal set; } = DateTime.UtcNow;
        public DateTime RevisionDate { get; internal set; } = DateTime.UtcNow;

        public void SetNewId()
        {
            Id = CoreHelpers.GenerateComb();
        }
    }
}
