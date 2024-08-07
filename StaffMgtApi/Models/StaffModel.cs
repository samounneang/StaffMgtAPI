using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using StaffMgtApi.Data;

namespace StaffMgtApi.Models
{
    public class StaffModel
    {
      
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        [StringLength(8)]
        public string StaffId { get; set; } = "";

        [StringLength(100)]
        public string FullName { get; set; } = "";

        [DataType(DataType.Date)]
        [Required]
        public DateTime Birthday { get; set; }

        public StaffGender Gender { get; set; }

        public StaffModel() { }
        public StaffModel(string StaffId, string FullName, DateTime Birthdate, StaffGender staffGender) {
            this.StaffId = StaffId;
            this.FullName = FullName;
            this.Birthday = Birthdate;
            this.Gender = staffGender;
        }
    }
    public enum StaffGender
    {
        Male = 1, Female = 2
    }

}