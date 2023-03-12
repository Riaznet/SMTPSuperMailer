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

        static Random random = new Random(); 
        public static string GenerateSentence()
        {
            string noun = BaseClass.GetFirstLastName();
            string verb = BaseClass.GetFirstLastName();
            string adjective = BaseClass.GetFirstLastName();
            string adverb = BaseClass.GetFirstLastName();  
            string sentence = $"Our Customer :  {noun} {verb} {adverb}  {adjective}.";

            noun = BaseClass.GetFirstLastName();
            verb = BaseClass.GetFirstLastName();
            adjective = BaseClass.GetFirstLastName();
            adverb = BaseClass.GetFirstLastName();
            sentence += $"{noun} {verb} {adverb}  {adjective}";

            noun = BaseClass.GetFirstLastName();
            verb = BaseClass.GetFirstLastName();
            adjective = BaseClass.GetFirstLastName();
            adverb = BaseClass.GetFirstLastName();
            sentence += $"{noun} {verb} {adverb}  {adjective}."; 

            return sentence;
        }
    }

}
