namespace Api.Utilities
{
    public static class Errors
    {
        public static string unknown_error { get; } = "Error Desconocido";
        public static string wrong_attributes { get; } = "Parametros Incorrectos";
        public static string user_not_authorized { get; } = "Usuario No Autorizado";

        public static string element_not_found { get; } = "El o los elementos buscados no se encuentran disponibles";

        public static string date_incorrect { get; } = "Fecha Incorrecta";

        public static string emailExist(string email)
        {
            return "El E-Mail " + email + " ya existe";
        }
    }
}