
using System.Text.Json.Serialization;

namespace ConsumerProducts.Models
{
    //public class FattureInCloudDto
    //{
    //    [JsonPropertyName("api_uid")]
    //    public string Api_uid { get; set; }

    //    [JsonPropertyName("api_key")]
    //    public string Api_key { get; set; }

    //    [JsonPropertyName("cod")]
    //    public string Cod { get; set; }

    //    [JsonPropertyName("nome")]
    //    public string Nome { get; set; }

    //    [JsonPropertyName("desc")]
    //    public string Desc { get; set; }

    //    [JsonPropertyName("prezzo_ivato")]
    //    public bool Prezzo_ivato { get; set; }

    //    [JsonPropertyName("prezzo_netto")]
    //    public string Prezzo_netto { get; set; }

    //    [JsonPropertyName("prezzo_lordo")]
    //    public string Prezzo_lordo { get; set; }

    //    [JsonPropertyName("costo")]
    //    public string Costo { get; set; }

    //    [JsonPropertyName("cod_iva")]
    //    public int cod_iva { get; set; }

    //    [JsonPropertyName("um")]
    //    public string Um { get; set; }

    //    [JsonPropertyName("categoria")]
    //    public string Categoria { get; set; }

    //    [JsonPropertyName("note")]
    //    public string Note { get; set; }

    //    [JsonPropertyName("magazzino")]
    //    public bool Magazzino { get; set; }

    //    [JsonPropertyName("giacenza_iniziale")]
    //    public string Giacenza_iniziale { get; set; }




    //}


    public class FattureInCloudDto
    {
        public string id { get; set; }
        public string api_uid { get; set; }
        public string api_key { get; set; }
        public string cod { get; set; }
        public string nome { get; set; }
        public string desc { get; set; }
        public bool prezzo_ivato { get; set; }
        public string prezzo_netto { get; set; }
        public string prezzo_lordo { get; set; }
        public string costo { get; set; }
        public int cod_iva { get; set; }
        public string um { get; set; }
        public string categoria { get; set; }
        public string note { get; set; }
        public bool magazzino { get; set; }
        public string giacenza_iniziale { get; set; }
    }


    public class Lista_Prodotti
    {
        public FattureInCloudDto[] lista_prodotti { get; set; }
        public int pagina_corrente { get; set; }
        public int numero_pagine { get; set; }
        public bool success { get; set; }
    }   


}
