using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCGCF.API.Entities
{

    public enum ELanguage { English, German, French, Italian, Spanish, Portuguese, Japanese, Chinese, Russian, Korean }

    public class Language
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public ELanguage LanguageName { get; set; }

        [Required]
        public string CardName { get; set; }

        [ForeignKey("CardId")]
        public Card Card { get; set; }
        public int CardId { get; set; }

        public string LanguageString() { 
            ELanguage enumLanguage = (ELanguage)LanguageName;
            return enumLanguage.ToString();
        }
    }
}
