using HashGenerator.Extensions;

namespace HashGenerator
{
    public class Program
    {
        /// <summary>Main function</summary>
        /// <param name="args">The params</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Inform the source value:");
            string? source = Console.ReadLine();

            Console.WriteLine("Inform the salt value:");
            string? salt = Console.ReadLine();

            string? hash = source?.CustomHash(salt);
            Console.WriteLine($"Hash generated: {hash}");
        }
    }
}