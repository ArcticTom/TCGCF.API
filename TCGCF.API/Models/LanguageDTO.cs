namespace TCGCF.API.Models
{
    public enum LanguageEnum { English, German, French, Italian, Spanish, Portuguese, Japanese, Chinese, Russian, Korean }
    public class LanguageDTO
    {
        public string LanguageName { get; set; }

        public string CardName { get; set; }

    }
}