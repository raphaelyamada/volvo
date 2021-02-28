using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Volvo.Cadastro.Models
{
    public class Caminhao
    {
        public int IdCaminhao { get; set; }

        [Required]
        [Display(Name = "Modelo")]
        public int ModeloIdModelo { get; set; }

        [Required]
        [Display(Name = "Ano de Fabricação")]
        public int AnoFabricacao { get; set; }
        
        [Required]
        [Remote(action: "ValidaAno", controller: "Caminhoes", AdditionalFields = nameof(AnoFabricacao))]
        [Display(Name = "Ano Modelo")]
        public int AnoModelo { get; set; }

        [ForeignKey("ModeloIdModelo")]
        public Modelo Modelo { get; set; }
    }
}
