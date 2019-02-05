using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleRPG
{
    class Puzzle
    {
        public string question { get; set; }
        List<string> answers = new List<string>();
        public string answer { get; set; } = "";
        public int correctAnswer { get; set; } = 0;

        public int nrOfAns { get; set; }

        public Puzzle(string fileName)
        {
            using (StreamReader readtext = new StreamReader(fileName))
            {
                question = readtext.ReadLine();
                nrOfAns = int.Parse(readtext.ReadLine());

                for (int i = 0; i < nrOfAns; i++)
                {
                    answers.Add(readtext.ReadLine());
                }

                correctAnswer = int.Parse(readtext.ReadLine());
            }
        }

        public string getAsString()
        {
            string answer = "";

            for (int i = 0; i < answers.Count; i++)
            {
                answer += i.ToString() + ": " + answers[i] + "\n";
            }

            //return question + "\n" + "\n"
            //    + answer + "\n"
            //    + correctAnswer.ToString() + "\n";

            return question + "\n" + "\n"
               + answer + "\n";
        }
    }
}
