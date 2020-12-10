using System;

namespace ComponentsDemo
{
    public class Animal
    {
        private static readonly Random s_random = new Random();
        private static readonly string[] s_types = { "Dog", "Cat", "Donkey", "Duck", "Snake", "Ferret", "Spider", "Kangaroo", "Mouse", "Rabbit", "Horse", "Parrot" };

        public Animal(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public static string GetName()
        {
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };

            var name = string.Empty;
            var nameLength = s_random.Next(3, 12);

            name += consonants[s_random.Next(consonants.Length)].ToUpper();
            name += vowels[s_random.Next(vowels.Length)];

            int curNameLength = 2;
            while (curNameLength < nameLength)
            {
                name += consonants[s_random.Next(consonants.Length)];
                name += vowels[s_random.Next(vowels.Length)];

                curNameLength += 2;
            }

            return name;
        }

        public static string GetTypeName()
            => s_types[s_random.Next(s_types.Length)];

        public static Animal Create()
            => new Animal(GetName(), GetTypeName());

        public override string ToString() => Name;

        public string Name { get; set; }

        public string Type { get; set; }
    }
}