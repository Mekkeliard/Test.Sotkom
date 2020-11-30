using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Bun
    {
        [Key]
        public long Id { get; set; }
        [Range(0, 100)]
        public double Price { get; set; }
        [Range(0, int.MaxValue)]
        public int CountFreshHours { get; set; }
        [Range(0, int.MaxValue)]
        public int CountBadHours { get; set; }
        public TypeOfBun BunType { get; set; }
    }

    public enum TypeOfBun
    {
        Croissant,
        Pretzel,
        Baguette,
        SourCream,
        Loaf
    }
}