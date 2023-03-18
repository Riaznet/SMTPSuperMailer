using Bogus;
using EmailBOT.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailBOT
{ 
    internal class SentenceMaker
    {
        static Faker faker = new Faker();
        public static string GenerateSentence()
        {
            string noun = GetFirstLastName();
            string verb = GetFirstLastName();
            string adjective = GetFirstLastName();
            string adverb = GetFirstLastName();
            string sentence = $"Our New Client :  {noun}, {verb} ,{adverb} , {adjective}.";

            noun = GetFirstLastName();
            verb = GetFirstLastName();
            adjective = GetFirstLastName();
            adverb = GetFirstLastName();
            sentence += $"Our Old Client : {noun} ,{verb} ,{adverb} , {adjective}";

            noun = GetFirstLastName();
            verb = GetFirstLastName();
            adjective = GetFirstLastName();
            adverb = GetFirstLastName();
            sentence += $"Our Outdoor Client :{noun} ,{verb} ,{adverb} , {adjective}.";

            return sentence;
        }
        internal static string GetFirstLastName()
        {
            try
            {
                var firstName = faker.Name.FirstName().Replace("\'", "");
                var lastName = faker.Name.LastName().Replace("\'", "");
                string name = firstName + " " + lastName;
                return name;
            }
            catch
            {
                return "";
            }
        }
        static Random random = new Random();
        public static string Lorem()
        {
            int randomNumber = random.Next(3, 501);
            string lorem = string.Join(" ", faker.Lorem.Sentences(randomNumber));
            return lorem;
        }
    }

}
