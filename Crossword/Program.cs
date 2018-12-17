using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossword
{
    class Program
    {
        static void Main(string[] args)
        {

            Riddle r1 = new Riddle("Cat", "Fluffy home animal");
            Riddle r2 = new Riddle("France", "Country in Europe");
            Riddle r3 = new Riddle("Alfa Romeo", "Italian car");
            Riddle r4 = new Riddle("August", "Summer month's name");

            List<Riddle> listOfRiddles = new List<Riddle>();
            listOfRiddles.Add(r1);
            listOfRiddles.Add(r2);
            listOfRiddles.Add(r3);
            listOfRiddles.Add(r4);
            //Console.WriteLine(listOfRiddles.Count);

            //var key = Console.ReadKey();

            do
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Possible actions (Press appropriate key number):");
                Console.WriteLine("1 - Guess the word:\n2 - Add the riddle:\n3 - Show list of questions:\n");
                int n;
               
                if (int.TryParse(Console.ReadLine(), out n)) {

                    //string choice = Console.ReadLine();
                    //int n = int.Parse(choice);

                    switch (n)
                    {
                        case 0:
                            return;
                        case 1:
                            
                            GuessTheWord(listOfRiddles);
                            break;
                        case 2:
                            
                            listOfRiddles.Add(AddTheRiddle());
                            break;
                        case 3:
                            
                            ShowListOfQuestions(listOfRiddles);

                            

                            break;
                        default:
                            Environment.Exit(0);
                            
                            break;
                    }
                }else
                {
                    Console.WriteLine("Invalid input, enter only number");
                }
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
    }
       

        class Riddle
        {
            private string word;
            private string description;
            private char[] answer;

            public string Word
            {
                get
                {
                    return word;
                }

                set
                {
                    word = value;
                }
            }

            public string Description
            {
                get
                {
                    return description;
                }

                set
                {
                    description = value;
                }
            }

            public char[] Answer
            {
                get
                {
                    return answer;
                }

                set
                {
                    answer = value;
                }
            }

            public Riddle(string word, string description)
            {
                this.Word = word;
                this.Description = description;
                this.Answer = new Char [word.Length];
                for (int i = 0; i < this.Answer.Length; i++)
                {
                    this.Answer[i] = '_';
                }
            }
        }

        static void ShowListOfQuestions (List<Riddle> list)
        {
            Console.Clear();
            Console.WriteLine("List of questions:");
            Console.WriteLine("(0 - Return to main menu)");
            Console.WriteLine();
            int i = 0;
            foreach (Riddle r in list)
            {
                i++;
                Console.WriteLine("{0}. Word: {1};", i, r.Word);
                Console.WriteLine("   Description: {0};", r.Description);
                Console.WriteLine();
            }

            
        }

        static Riddle AddTheRiddle()
        {
            Console.Clear();
            Console.WriteLine("Add the riddle:");
            Console.WriteLine();
            
            //Riddle r;
            string w = "", d = "";
            // Проверить строку в нижнем регистре.
            while (w.Length == 0)
            {
                Console.WriteLine("Enter riddle word:");
                w = Console.ReadLine();
            }

            while (d.Length == 0)
            {
                Console.WriteLine("Enter riddle explanation:");
                d = Console.ReadLine();
            }
            Console.WriteLine("The riddle was successfully entered to question base.");
            Console.WriteLine("Press enter to continue.");
            return new Riddle(w, d);



        }


        static void GuessTheWord(List<Riddle> list)
        {



                Console.Clear();

                Console.WriteLine("Guess the word:");
                Console.WriteLine("(0 - Return to main menu)");

                Console.WriteLine();


                //string w = "", d = "";
                //Char[] a;
                Random random = new Random();
                int randomNumber = random.Next(0, list.Count);
                Riddle riddleObj = list.ElementAt(randomNumber);
                //w = riddleObj.Word;
                //d = riddleObj.Description;
                //a = riddleObj.Answer;
                Console.WriteLine("Description: {0}", riddleObj.Description);
                Console.WriteLine("Length of word is {0} letters", riddleObj.Word.Length);

                PrintWord(riddleObj.Answer);

            

                while (!riddleObj.Word.ToUpper().Equals(new string(riddleObj.Answer).ToUpper()))
                {

                    //Console.WriteLine(riddleObj.Word.ToUpper());
                    //Console.WriteLine(riddleObj.Answer.ToString().ToUpper());

                    char letter = ' ';
                    string guess;
                    do
                    {

                    
                        

                        Console.WriteLine("Guess the letter or whole word:");
                        guess = Console.ReadLine();
                        if (guess.Length == 1 && guess == "0")
                        {
                            return;
                        }
                        else if (guess.Length == 1)
                        {
                            letter = Char.Parse(guess);
                        }

                    } while (guess.Length <= 0);

                    if (guess.ToUpper().Equals(riddleObj.Word.ToUpper()))
                    {
                        riddleObj.Answer = IsLetterInString(' ', riddleObj.Word, riddleObj.Answer, guess);
                        PrintWord(riddleObj.Answer);
                        Console.WriteLine("Congrats! You guess the word");
                        for (int i = 0; i < riddleObj.Answer.Length; i++)
                            riddleObj.Answer[i] = '_';
                        return;
                    }

                    riddleObj.Answer = IsLetterInString(letter, riddleObj.Word, riddleObj.Answer, "");

                    PrintWord(riddleObj.Answer);

                }

                Console.WriteLine("Congrats! You guess the word");
                for (int i = 0; i < riddleObj.Answer.Length; i++)
                    riddleObj.Answer[i] = '_';

            
        }

        static Char[] IsLetterInString (Char c, string s, Char[] cArr, string answer)
        {
            int count = 0;

            if (s.ToUpper().Equals(answer.ToUpper()))
            {
                cArr = answer.ToUpper().ToCharArray();
                return cArr;
            }


            for (int i = 0; i < s.Length; i++)
            {
                //if (c.ToString().ToUpper().Equals(s[i].ToString().ToUpper()))
                if (Char.ToUpper(s[i]).Equals(Char.ToUpper(c)))
                {
                    cArr[i] = Char.ToUpper(c);
                    count++;
                }
            }
            if (count > 0)
            {
                Console.WriteLine("Your guess is right!");
            }else
            {
                Console.WriteLine("Try another letter!");
            }
            return cArr;
        }

        static void PrintWord(Char[] cArr)
        {
           
            for (int i = 0; i < cArr.Length; i++)
            {
                Console.Write(cArr[i]);
                Console.Write(" ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }

    }

}
