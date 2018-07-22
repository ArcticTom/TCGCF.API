using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCGCF.API.Entities
{
    public enum EColor { White, Red, Black, Blue, Green, Colorless }

    public class Color
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public EColor Name { get; set; }

        [ForeignKey("CardId")]
        public Card Card { get; set; }
        public int CardId { get; set; }

        public string ColorName() { 
            EColor enumColor = (EColor)Name;
            return enumColor.ToString();
        }
    }
}
