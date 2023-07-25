using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Film
    {
       // [Key]
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[Required(ErrorMessage ="Film Adı boş geçilemez.")]
        //[StringLength(50,ErrorMessage ="Film adı en fazla 50 karakter olabilir.")]
        //[Display(Name ="Film Adı")]
        public string FilmAdi { get; set; }

        //[DisplayName("Yönetmen Adı")]
        public string YonetmenAdi { get; set; }

        public string Basrol { get; set; }

        public int CikisYili { get; set; }

        public string Kategori { get; set; }
    }
}
