﻿using System;

namespace Events.Business.Models
{
    public class Event
    {
        public virtual string Id { get; set; }

        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual string LongDescription { get; set; }

        public virtual DateTime? FromDate { get; set; }

        public virtual DateTime? ToDate { get; set; }

        public virtual string Location { get; set; }
    }
}