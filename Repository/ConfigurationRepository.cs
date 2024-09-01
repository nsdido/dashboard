using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Climate_Watch.Models.Configurations;
using CW_Website.Repository;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace Climate_Watch.Repository;

public class ConfigurationRepository : IConfigurationRepository {
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

    public MonitoringModel GetMonitoringSettings()
    {
        const string url = "http://64.226.79.78:7000/sysconfig/monitoring";

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());

        var response = client.GetAsync(url).Result;
        response.EnsureSuccessStatusCode();
        var responseBody = response.Content.ReadAsStringAsync().Result;

        var result = JsonConvert.DeserializeObject<MonitoringModel>(responseBody);

        return result;
    }

    public MonitoringModel UpdateMonitoringSettings(MonitoringModel model)
    {
        const string url = "http://64.226.79.78:7000/sysconfig/monitoring";


        // Create an instance of HttpClient
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());
        // Serialize the data to JSON


        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        var json = JsonSerializer.Serialize(model, options);

        // Create an HttpContent object
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Make the POST request
        var response = client.PutAsync(url, content).Result;

        // Read the response content
        var responseString = response.Content.ReadAsStringAsync().Result;

        return model;
    }

    public DbConnectorModel GetDbConnectorSettings()
    {
        const string url = "http://64.226.79.78:7000/sysconfig/dbConnector";

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());
        var response = client.GetAsync(url).Result;
        response.EnsureSuccessStatusCode();
        var responseBody = response.Content.ReadAsStringAsync().Result;

        var result = JsonConvert.DeserializeObject<DbConnectorModel>(responseBody);

        return result;
    }

    public DbConnectorModel UpdateDbConnectorSettings(DbConnectorModel model)
    {
        const string url = "http://64.226.79.78:7000/sysconfig/monitoring";

        // Create an instance of HttpClient
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());
        // Serialize the data to JSON


        //===============

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        var json = JsonSerializer.Serialize(model, options);

        // Create an HttpContent object
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");


        // Make the POST request
        var response = client.PutAsync(url, content).Result;

        // Read the response content
        var responseString = response.Content.ReadAsStringAsync().Result;

        return model;
    }

    public InfluxDbModel GetInfluxDbSettings()
    {
        const string url = "http://64.226.79.78:7000/sysconfig/influxdb";

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());
        var response = client.GetAsync(url).Result;
        response.EnsureSuccessStatusCode();
        var responseBody = response.Content.ReadAsStringAsync().Result;

        var result = JsonConvert.DeserializeObject<InfluxDbModel>(responseBody);

        return result;
    }

    public InfluxDbModel UpdateInfluxDbSettings(InfluxDbModel model)
    {
        const string url = "http://64.226.79.78:7000/sysconfig/influxdb";

        // Create an instance of HttpClient
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        var json = JsonSerializer.Serialize(model, options);

        // Create an HttpContent object
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Make the POST request
        var response = client.PutAsync(url, content).Result;

        // Read the response content
        var responseString = response.Content.ReadAsStringAsync().Result;

        return model;
    }

    public HistoricalDataModel GetHistoricalDataSettings()
    {
        const string url = "http://64.226.79.78:7000/sysconfig/historical";

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());
        var response = client.GetAsync(url).Result;
        response.EnsureSuccessStatusCode();
        var responseBody = response.Content.ReadAsStringAsync().Result;

        var result = JsonConvert.DeserializeObject<HistoricalDataModel>(responseBody);

        return result;
    }

    public HistoricalDataModel UpdateHistoricalDataSettings(HistoricalDataModel model)
    {
        const string url = "http://64.226.79.78:7000/sysconfig/historical";

        // Create an instance of HttpClient
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());
        // Serialize the data to JSON


        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        var json = JsonSerializer.Serialize(model, options);

        // Create an HttpContent object
        var content = new StringContent(json, Encoding.UTF8, "application/json");


        // Make the POST request
        var response = client.PutAsync(url, content).Result;

        // Read the response content
        var responseString = response.Content.ReadAsStringAsync().Result;

        return model;
    }

    public WebsiteDataModel GetWebsiteSettings()
    {
        const string url = "http://64.226.79.78:7000/sysconfig/website";

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());
        var response = client.GetAsync(url).Result;
        response.EnsureSuccessStatusCode();
        var responseBody = response.Content.ReadAsStringAsync().Result;

        var result = JsonConvert.DeserializeObject<WebsiteDataModel>(responseBody);

        return result;
    }

    public WebsiteDataModel RegisterWebsite()
    {
        // const string url = "http://64.226.79.78:7000/services/register?service_name=website";
//
//        using var client = new HttpClient();
//        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());
//        var response = client.GetAsync(url).Result;
//        response.EnsureSuccessStatusCode();
//        var responseBody = response.Content.ReadAsStringAsync().Result;
//
//        var result = JsonConvert.DeserializeObject<WebsiteDataModel>(responseBody);
//
//        return result;

        return new WebsiteDataModel();
    }

    public WebsiteDataModel UpdateWebsiteSettings(WebsiteDataModel model)
    {
        const string url = "http://64.226.79.78:7000/sysconfig/website";

        // Create an instance of HttpClient
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());


        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        var json = JsonSerializer.Serialize(model, options);

        // Create an HttpContent object
        var content = new StringContent(json, Encoding.UTF8, "application/json");


        // Make the POST request
        var response = client.PutAsync(url, content).Result;

        // Read the response content
        var responseString = response.Content.ReadAsStringAsync().Result;

        return model;
    }

    public UACDataModel GetUACSettings()
    {
        const string url = "http://64.226.79.78:7000/sysconfig/uac";

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());
        var response = client.GetAsync(url).Result;
        response.EnsureSuccessStatusCode();
        var responseBody = response.Content.ReadAsStringAsync().Result;

        var result = JsonConvert.DeserializeObject<UACDataModel>(responseBody);

        return result;
    }

    public UACDataModel UpdateUACSettings(UACDataModel model)
    {
        const string url = "http://64.226.79.78:7000/sysconfig/uac";

        // Create an instance of HttpClient
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());


        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        var json = JsonSerializer.Serialize(model, options);

        // Create an HttpContent object
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Make the POST request
        var response = client.PutAsync(url, content).Result;

        // Read the response content
        var responseString = response.Content.ReadAsStringAsync().Result;

        return model;
    }

    public TelegramDataModel GetTelegramSettings()
    {
        const string url = "http://64.226.79.78:7000/sysconfig/telegram";

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());
        var response = client.GetAsync(url).Result;
        response.EnsureSuccessStatusCode();
        var responseBody = response.Content.ReadAsStringAsync().Result;

        var result = JsonConvert.DeserializeObject<TelegramDataModel>(responseBody);

        return result;
    }

    public TelegramDataModel UpdateTelegramSettings(TelegramDataModel model)
    {
        const string url = "http://64.226.79.78:7000/sysconfig/telegram";

        // Create an instance of HttpClient
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());
        // Serialize the data to JSON

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        var json = JsonSerializer.Serialize(model, options);

        // Create an HttpContent object
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Make the POST request
        var response = client.PutAsync(url, content).Result;

        // Read the response content
        var responseString = response.Content.ReadAsStringAsync().Result;

        return model;
    }

    public GeneralDataModel GetGeneralSettings()
    {
        const string url = "http://64.226.79.78:7000/sysconfig/general";

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());
        var response = client.GetAsync(url).Result;
        response.EnsureSuccessStatusCode();
        var responseBody = response.Content.ReadAsStringAsync().Result;

        var result = JsonConvert.DeserializeObject<GeneralDataModel>(responseBody);

        return result;
    }

    public GeneralDataModel UpdateGeneralSettings(GeneralDataModel model)
    {
        const string url = "http://64.226.79.78:7000/sysconfig/general";

        // Create an instance of HttpClient
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());


        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        var json = JsonSerializer.Serialize(model, options);

        // Create an HttpContent object
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        // Make the POST request
        var response = client.PutAsync(url, content).Result;

        // Read the response content
        var responseString = response.Content.ReadAsStringAsync().Result;

        return model;
    }
}