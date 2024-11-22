﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherAPP.Models
{
    internal class Weather
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }


        [JsonPropertyName("main")]
        public string Main { get; set; }


        [JsonPropertyName("description")]
        public string Description { get; set; }


        [JsonPropertyName("icon")]
        public string Icon { get; set; }
    }
}
