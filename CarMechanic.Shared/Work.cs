using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace CarMechanic.Shared;

public enum WorkStatus
{   
    ListedWork,
    Working,
    Completed
}

public enum WorkCategory
{   
    BodyWork,
    Engine,
    Suspension,
    Breaks
}
    
    public class Work
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string WorkId { get; set; }
        
        [Required]
        [ForeignKey("CustomerId")]
        public string CustomerId { get; set; }
        
        [Required]
        [RegularExpression(@"^[A-Z]{3}-\d{3}$")]
        public string LicensePlate { get; set; }
        
        [Required]
        public DateOnly ManufacturingDate { get; set; }
        
        [Required]
        public WorkCategory Category { get; set; }
        
        [Required]
        public string WorkDescription { get; set; }
        
        [Required]
        [Range(typeof(int), "0", "10")]
        public int Fault { get; set; }
        
        [Required]
        public WorkStatus Status { get; set; }
    }


