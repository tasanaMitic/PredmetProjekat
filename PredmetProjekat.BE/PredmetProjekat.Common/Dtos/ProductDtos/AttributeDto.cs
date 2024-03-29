﻿using System.ComponentModel.DataAnnotations;

namespace PredmetProjekat.Common.Dtos.ProductDtos
{
    public class AttributeDto
    {
        [Required(AllowEmptyStrings = false)]
        public Guid AttributeId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string AttributeName { get; set; }
    }
}
