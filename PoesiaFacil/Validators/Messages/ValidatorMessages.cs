using System.Text;

namespace PoesiaFacil.Validators.Messages
{
    public abstract class ValidatorMessages
    {
        public static string FieldIsEmptyMessage(string field)
        {
            return $"O Conteúdo do Campo {field} não pode ser vazio";
        }

        public static string FieldIsInAnInvalidFormat(string field)
        {
            return $"O Conteúdo do Campo {field} está em um formato inválido";
        }
    }
}
