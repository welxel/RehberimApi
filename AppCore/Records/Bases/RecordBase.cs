using System;
using System.ComponentModel.DataAnnotations;

namespace AppCore.Records.Bases
{
    public abstract class RecordBase
    {
        [Key]
        public int Id { get; set; }
    }
}
