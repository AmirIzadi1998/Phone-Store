﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class PhoneProduct
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string PhoneName { get; set; }
        public string PhoneColor { get; set; }
        [Required]
        [MaxLength(15)]
        public long PhoneIMEI { get; set; }
        public bool PhoneIsGlobal { get; set; }
        public string PhoneBattery { get; set; }
        public string PhoneChip { get; set; }
        public bool IsExisting { get; set; }



    }
}
