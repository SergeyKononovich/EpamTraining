using System.Linq;

namespace Task2_TextEditor
{
    public static class CharExtentions
    {
        public static bool IsVowel(this char character)
        {
            return new[]
            {
                'a', 'e', 'i', 'o', 'u',
                'а', 'о', 'е', 'ы', 'э', 'ю', 'и', 'я'
            }.Contains(char.ToLower(character));
        }

        public static bool IsСonsonant(this char character)
        {
            return new[]
            {
                'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z',
                'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ'
            }.Contains(char.ToLower(character));
        }
    }
}
