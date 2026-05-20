using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CleanArchitectureLearnProject.Domain.Dtos;

public sealed class GetAccessTokenResponseDto
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = default!;
}
