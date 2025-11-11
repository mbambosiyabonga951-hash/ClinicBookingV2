using System.ComponentModel.DataAnnotations;

namespace ClinicBooking.Client.Models {

    // Auth
    public sealed class RegisterDto { public string? Email { get; set; } public string? Password { get; set; } }
    public sealed class LoginDto { public string? Email { get; set; } public string? Password { get; set; } }
    public sealed class AuthResponse { public string? token { get; set; } }

    // Clinics & Patients
    public class UpsertProviderRequest
    {

        public string FullName { get; set; }
        public string Specialty { get; set; }
        public long ClinicId { get; set; }
    }
    public sealed class ClinicDto { public long Id { get; set; } public string? Name { get; set; } public string? Address { get; set; } }
    public record ProviderDto(long Id, string Name, string Specialty, long ClinicId);

    public record CreateAppointmentRequest
    {
        public long ClinicId { get; set; }
        public long ProviderId { get; set; } = 0;

        public string Date { get; set; } = "";      // yyyy-MM-dd 

        public long PatientId { get; set; }
        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }
        public string? Notes { get; set; }
    }
    public record TimeslotDto(long Id, long ProviderId, DateTime StartUtc, DateTime EndUtc, bool IsBooked);
    public record CreateTimeslotRequest(long ProviderId, DateTime StartUtc, DateTime EndUtc);
    public sealed class PatientDto { public long Id { get; set; } public string? FirstName { get; set; } public string? LastName { get; set; } public string? Email { get; set; } }
    public sealed class CreatePatientRequest { public string? FirstName { get; set; } public string? LastName { get; set; } public string? Email { get; set; } }

    // Appointments (strings per OpenAPI)
    public sealed class AppointmentDto
    {
        public long Id { get; set; }
        public long ClinicId { get; set; }
        public long PatientId { get; set; }
        public string Date { get; set; } = "";      // yyyy-MM-dd
        public string StartTime { get; set; } = ""; // HH:mm:ss
        public string EndTime { get; set; } = "";   // HH:mm:ss
        public string? Email { get; set; }

   
   
  

//public sealed class CreateAppointmentRequest
//{
//    public long ClinicId { get; set; }
//    public long PatientId { get; set; }
//    public DateTime? Date { get; set; }
//    public DateTime? StartTime { get; set; } 
//    public DateTime? EndTime { get; set; }
//    public string? Email { get; set; }    
//    public string? FirstName { get; set; }
//    public string? LastName { get; set; }

//    public string SelectedClinicIdStr { get; set; } = "";
//    public string SelectedPatientIdStr { get; set; }   = "";

//    private DateTime? createDate;

//    private DateTime? createStartDateTime;
//    private DateTime? createEndDateTime;

    public string PatientIdText
        {
            get => PatientId.ToString();
            set
            {
                if (int.TryParse(value, out var id))
                {
                    PatientId = id;
                }
            }
        }

        public string ClinicIdText
        {
            get => ClinicId.ToString();
            set
            {
                if (int.TryParse(value, out var id))
                {
                    ClinicId = id;
                }
            }
        }

    }
}
