using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiPharmacy.Dtos
{
    public class DataUserDto
    {
        public string Message  { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> JobsTitle { get; set; }
        public string Token { get; set; }

        [JsonIgnore]

        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }

    }
}