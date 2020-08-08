using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDbBackup.Database
{
    public class CoinImages
    {
        [Key]
        public string CoinName { get; set; }
        public string ImageURL { get; set; }
        public string Base64EncodedImage { get; set; }
    }
}
