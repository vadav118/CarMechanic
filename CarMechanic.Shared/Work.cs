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
        public int Id { get; private set; }
        
        [Required]
        public int CustomerId { get; set; }
        
        [Required]
        [RegularExpression(@"^[A-Z]{3}-\d{3}$")]
        public string LicensePlate { get; set; }
        
        [Required]
        [TimeValidation]
        public int ManufacturingYear { get; set; }
        
        [Required]
        [Range(0,3)]
        public WorkCategory Category { get; set; }
        
        [Required]
        public string WorkDescription { get; set; }
        
        [Required]
        [Range(0,10)]
        public int Fault { get; set; }
        
        [Required]
        [Range(0,2)]
        public WorkStatus Status { get; set; }
        
       
        public double Estimate { get; private set; }

        public void CalculateEstimate()
        {
            Estimate = WorkEstimate.CalculateWorkEstimation(this);
        }
    }


