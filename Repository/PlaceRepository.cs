using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Climate_Watch.Models;
using Climate_Watch.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;


namespace CW_Website.Repository;

public class PlaceRepository:IPlaceRepository {
    private static string GenerateToken()
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
    public void CreatePlace(DefineHome place)
    {
        try
        {
            string url = "http://64.226.79.78:7000/places";

            // Create an instance of HttpClient
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());
                // Serialize the data to JSON
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(place, options);

                // Create an HttpContent object
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // Make the POST request
                HttpResponseMessage response = client.PostAsync(url, content).Result;

                // Read the response content
                string responseString = response.Content.ReadAsStringAsync().Result;
            }
        }
        catch (Exception e)
        {
            throw new BadHttpRequestException("The create place api is not available");
        }        
    }

    public void UpdatePlace(DefineHome place)
    {
        string url = "http://64.226.79.78:7000/places";

        // Create an instance of HttpClient
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());
            
            
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(place, options);
            
            

            // Create an HttpContent object
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            // Make the POST request
            HttpResponseMessage response = client.PutAsync(url, content).Result;

            // Read the response content
            string responseString = response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode!=HttpStatusCode.OK)
            {
                throw new BadHttpRequestException("The create place api is not available");
            }
            
        }
        
        
    }
    public  IEnumerable<DefinedHome> GetPlaces(string telegramId)
    {
        var url = "http://64.226.79.78:7000/places/by-username?adminUsername=" + telegramId;
       //var url = "http://localhost:7000/places?adminUsername=" + telegramId;

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());
        var response =  client.GetAsync(url).Result;
        if (response.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.BadRequest)
        {
            return new List<DefinedHome>();
        }
        response.EnsureSuccessStatusCode();
        var responseBody =  response.Content.ReadAsStringAsync().Result;
            
        var result=Newtonsoft.Json.JsonConvert.DeserializeObject<List<DefinedHome>>(responseBody);

        return result.ToList();
    }
    
    
    
    public  IEnumerable<string> GetUsersInPlace(long placeId)
    {
        var url = "http://64.226.79.78:7000/user/by-place-id?placeID=" + placeId;
        

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());
        var response =  client.GetAsync(url).Result;
        if (response.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.BadRequest)
        {
            return new List<string>();
        }
        response.EnsureSuccessStatusCode();
        var responseBody =  response.Content.ReadAsStringAsync().Result;
            
        var result=Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(responseBody);

        return result.ToList();
    }
    
    
    public void AddUserToPlace(AddUserToPlace place)
    {
        try
        {
            string url = "http://64.226.79.78:7000/places/admin";

            // Create an instance of HttpClient
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());
                // Serialize the data to JSON
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(place, options);

                // Create an HttpContent object
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // Make the POST request
                HttpResponseMessage response = client.PostAsync(url, content).Result;

                // Read the response content
                string responseString = response.Content.ReadAsStringAsync().Result;
            }
        }
        catch (Exception e)
        {
            throw new BadHttpRequestException("The create place api is not available");
        }        
    }
}