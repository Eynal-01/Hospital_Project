﻿using HospitalProject.Entities.Data;
using HospitalProject.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Entities.DbEntities
{
    public class Post
    {
        public int Id { get; set; }
        public string? AdminId { get; set; }
        public string? ImageUrl { get; set; }
        public bool? IsImage { get; set; }
        public string? Content { get; set; }
        public string? Title { get; set; }
        public string? PublishTime { get; set; }
        public int ViewCount { get; set; }

        public Admin? Admin { get; set; }
    }
}
