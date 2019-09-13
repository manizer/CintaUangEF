using System;
using System.ComponentModel.DataAnnotations;

namespace Model.DTO.DB
{
    public class ExecuteResultDTO
    {
        [Key]
        public int InstanceId { get; set; }
    }
}
