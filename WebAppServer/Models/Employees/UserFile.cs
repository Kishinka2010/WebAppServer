﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAppServer.Models.Employees
{
    public class UserFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string SystemName { get; set; }
        public string DisplayName { get; set; }
        //public Employee Employee { get; set; }
    }
}
