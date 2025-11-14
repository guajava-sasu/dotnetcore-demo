namespace ModustaAPI.Tools
{
    public class ColorGenerator
    {
        private static Random _random = new Random();

        public static string GenerateRandomColor()
        {
            int r = _random.Next(0, 100);  // Générer une valeur aléatoire pour rouge (0-255)
            int g = _random.Next(100, 256);  // Générer une valeur aléatoire pour vert (0-255)
            int b = _random.Next(0, 256);  // Générer une valeur aléatoire pour bleu (0-255)

            return $"rgb({r}, {g}, {b})";  // Retourner la couleur au format "rgb(r, g, b)"
        }
    }

}
