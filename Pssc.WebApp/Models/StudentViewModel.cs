﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pssc.WebApp.Models
{
    public class StudentViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Faculty { get; set; }
        public long Id { get; set; }
        public int Grade { get; set; }
    }
}