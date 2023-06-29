using System.ComponentModel.DataAnnotations;

namespace RoomBooking.Domain.BaseModels
{
    public abstract class RoomBookingBase : IValidatableObject
    {
        [Required]
        [StringLength(50)]
        public string? FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Date <= DateTime.Now.Date)
                yield return new ValidationResult("Date must be in future", new[] { nameof(Date) });
        }
    }
}