using System.Collections.Generic;
using System.Text.Json.Serialization;

public class GoogleGeocodeResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("results")]
    public List<GoogleGeocodeResult> Results { get; set; }
}

public class GoogleGeocodeResult
{
    [JsonPropertyName("formatted_address")]
    public string FormattedAddress { get; set; }

    [JsonPropertyName("address_components")]
    public List<GoogleAddressComponent> AddressComponents { get; set; }

    [JsonPropertyName("geometry")]
    public GoogleGeometry Geometry { get; set; }
}

public class GoogleAddressComponent
{
    [JsonPropertyName("long_name")]
    public string LongName { get; set; }

    [JsonPropertyName("short_name")]
    public string ShortName { get; set; }

    [JsonPropertyName("types")]
    public List<string> Types { get; set; }
}

public class GoogleGeometry
{
    [JsonPropertyName("location")]
    public GoogleLocation Location { get; set; }
}

public class GoogleLocation
{
    [JsonPropertyName("lat")]
    public double Lat { get; set; }

    [JsonPropertyName("lng")]
    public double Lng { get; set; }
}

// 🔹 Resposta de Distância
public class GoogleDistanceResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("rows")]
    public List<GoogleDistanceRow> Rows { get; set; }
}

public class GoogleDistanceRow
{
    [JsonPropertyName("elements")]
    public List<GoogleDistanceElement> Elements { get; set; }
}

public class GoogleDistanceElement
{
    [JsonPropertyName("distance")]
    public GoogleDistanceValue Distance { get; set; }

    [JsonPropertyName("duration")]
    public GoogleDurationValue Duration { get; set; }
}

public class GoogleDistanceValue
{
    [JsonPropertyName("value")]
    public int Value { get; set; } // Em metros

    [JsonPropertyName("text")]
    public string Text { get; set; } // Exemplo: "5.2 km"
}

public class GoogleDurationValue
{
    [JsonPropertyName("value")]
    public int Value { get; set; } // Em segundos

    [JsonPropertyName("text")]
    public string Text { get; set; } // Exemplo: "15 min"
}

// 🔹 Sugestões de endereço (autocomplete)
public class GoogleAutocompleteResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("predictions")]
    public List<GooglePrediction> Predictions { get; set; }
}

public class GooglePrediction
{
    [JsonPropertyName("description")]
    public string Description { get; set; }
}
