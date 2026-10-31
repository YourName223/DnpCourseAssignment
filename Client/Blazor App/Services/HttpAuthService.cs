using System.Net.Http.Headers;
using System.Text.Json;
using System.Security.Claims;
using System.Buffers.Text;
using System.Security.Cryptography;
using DTOs;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

using Microsoft.IdentityModel.JsonWebTokens;



public class HttpAuthService(HttpClient client, ProtectedLocalStorage storage) : IAuthService
{
    private const string StorageKey = "jwt";
    public string? Jwt { get; private set; }

    public event Action<ClaimsPrincipal>? OnAuthStateChanged;
    public async Task LoginAsync(UserLoginDto request)
    {
        
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("Auth/login",request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        string token = response;

        await storage.SetAsync(StorageKey, token);

        ClaimsPrincipal principal = UseToken(token);
        OnAuthStateChanged?.Invoke(principal);
    }
    //Should have a logout method
    public async Task<ClaimsPrincipal> GetAuthAsync()
    {
        // After a page refresh we have a new circuit, so the token must be loaded from the browser again
        Jwt ??= await LoadTokenFromStorageAsync();
        return UseToken(Jwt);
    }
    public async Task<int?> GetUserIdAsync()
    {
        ClaimsPrincipal principal = await GetAuthAsync();

        String? sub = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        if(!int.TryParse(sub, out int userId))
        {
            return null;
        }

        return userId;
    }

    private ClaimsPrincipal UseToken(string? token)
    {
        List<Claim> claims = token == null ? [] : ParseClaimsFromJwt(token);

        if (claims.Count == 0 || IsExpired(claims))
        {
            Jwt = null;
            client.DefaultRequestHeaders.Authorization = null;
            return new ClaimsPrincipal(); // not logged in
        }

        Jwt = token;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // nameType and roleType tell .NET which claims to use for User.Identity.Name and User.IsInRole(...)
        ClaimsIdentity identity = new(claims, authenticationType: "jwt", nameType: JwtRegisteredClaimNames.Sub, roleType: ClaimTypes.Role);
        return new ClaimsPrincipal(identity);
    }

    private static bool IsExpired(List<Claim> claims)
    {
        long exp = long.Parse(claims.First(c => c.Type == "exp").Value);
        return DateTimeOffset.FromUnixTimeSeconds(exp) < DateTimeOffset.UtcNow;
    }

    private static List<Claim> ParseClaimsFromJwt(string jwt)
    {
        string payload = jwt.Split('.')[1];
        byte[] json = Base64Url.DecodeFromChars(payload);
        Dictionary<string, JsonElement> values = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json) ?? [];
        return values.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())).ToList();
    }

    private async Task<string?> LoadTokenFromStorageAsync()
    {
        try
        {
            ProtectedBrowserStorageResult<string> result = await storage.GetAsync<string>(StorageKey);
            return result.Success ? result.Value : null;
        }
        catch (CryptographicException)
        {
            // The stored value was tampered with, or encrypted with an old key: treat it as logged out
            await storage.DeleteAsync(StorageKey);
            return null;
        }
    }
}