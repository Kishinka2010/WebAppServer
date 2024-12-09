using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAppServer.Models.Employees
{
    public class WorkExperience
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int WorkedYears { get; set; } = 0;
        public string? Description { get; set; } = null;
        public Employee Employee { get; set; }
    }
}
