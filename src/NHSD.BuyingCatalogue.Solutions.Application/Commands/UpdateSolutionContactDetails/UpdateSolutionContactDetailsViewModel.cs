using System;
using Newtonsoft.Json;

namespace NHSD.BuyingCatalogue.Solutions.Application.Commands.UpdateSolutionContactDetails
{
    public sealed class UpdateSolutionContactDetailsViewModel
    {
        [JsonProperty("contact-1")]
        public UpdateSolutionContactViewModel Contact1 { get; set; }

        [JsonProperty("contact-2")]
        public UpdateSolutionContactViewModel Contact2 { get; set; }
    }

    public sealed class UpdateSolutionContactViewModel
    {
        [JsonProperty("department-name")]
        public string Department { get; set; }

        [JsonProperty("first-name")]
        public string FirstName { get; set; }

        [JsonProperty("last-name")]
        public string LastName { get; set; }

        [JsonProperty("phone-number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("email-address")]
        public string Email { get; set; }

        public bool HasData()
        {
            return !(String.IsNullOrWhiteSpace(FirstName) &&
                     String.IsNullOrWhiteSpace(LastName) &&
                     String.IsNullOrWhiteSpace(PhoneNumber) &&
                     String.IsNullOrWhiteSpace(Email) &&
                     String.IsNullOrWhiteSpace(Department));
        }

        public void Trim()
        {
            Department = Department?.Trim();
            FirstName = FirstName?.Trim();
            LastName = LastName?.Trim();
            PhoneNumber = PhoneNumber?.Trim();
            Email = Email?.Trim();
        }
    }
}
