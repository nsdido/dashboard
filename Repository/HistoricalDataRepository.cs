using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Climate_Watch.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Climate_Watch.Repository;

public class HistoricalDataRepository:IHistoricalDataRepository {
    
    public string GenerateToken()
    {
        // Create JWT token
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, "Dashboard"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, "Climate_Watch_User"),
            new Claim("IsAdmin", "Climate_Watch_is_Admin"),
        };

        // var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKeyThatIsAtLeast32CharactersLong"));
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("fdjkfghdkjfghdkfjghdfjghfjghfjghfgjhfjghfgjfhgjfgh"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "YourIssuer",
            audience: "YourAudience",
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: creds);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenString;
    }
    public  IEnumerable<HistoricalData> GetHistoricalData(string sensorName,long placeId)
    {
        string url = "http://167.71.6.50:6060/historical/data?sensor_name=" 
                     + sensorName 
                     + "&place_id=" 
                     + placeId;

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());
            HttpResponseMessage response =  client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            string responseBody =  response.Content.ReadAsStringAsync().Result;
            
            var result=JsonConvert.DeserializeObject<List<HistoricalData>>(responseBody);
            result = result.Select(s => { s.SensorName = sensorName; return s; }).ToList();

            return result.ToList();
        }
    }
}