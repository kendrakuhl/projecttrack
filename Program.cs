using System;

namespace DiceRolls
{
    class Program
    {
        // This is the winning score. It is a CONST which means that it can never be
        // changed. Can you think why I would want to do it this way?
        private const int WINNING_SCORE = 20;

        static void Main(string[] args)
        {
            bool isThereAWinner = false; // This is what my loop will be checking
            int turnCounter = PickFirstPlayer(); // This will tell me who's turn it is.
            int computerScore = 0; // This is the computer's score
            int kendraScore = 0; // This is the player's score

            Console.WriteLine("Let's Play A Game!!");
            Console.WriteLine("Who Ever Gets To 20 First, WINS!!!");
            Console.Write("Please enter a name and hit enter: ");
            var playerName = Console.ReadLine();

            do
            {
                // If the turn counter is odd, it's my turn. If it's even, it's computer's
                // This is called a modulus operator. What it does is gives you the remainder of a division
                // so, if you divide by two and it's even, your remainder is 0
                if ((turnCounter%2) > 0) // Me
                {
                    isThereAWinner = TakeTurn(kendraScore, playerName, out kendraScore);

                } else // Computer
                {
                    isThereAWinner = TakeTurn(computerScore, "Computer", out computerScore);
                }

                turnCounter++;
            } while (!isThereAWinner);

            Console.ReadLine();
        }

        static private int RollDice()
        {
            // I do this because of the way that random works.
            // If the loop is fast enough, the clock time that random
            // keys off of will be the same, this means the number
            // that random will give me will be the same. 
            byte[] gb = Guid.NewGuid().ToByteArray();
            int i = BitConverter.ToInt32(gb, 0);

            Random rnd = new Random(i); // I'm seeding this with something that is pretty unique
            return rnd.Next(2, 12);
        }

        static private bool TakeTurn(int currentScore, string playerName, out int newScore)
        {
            if (playerName != "Computer")
            {
                Console.Write("It is your turn. Hit enter to roll.");
                Console.ReadLine();
            } else
            {
                Console.WriteLine("It is {0}'s turn.", playerName);
            }

            var roll = RollDice();
            newScore = currentScore + roll;
            Console.WriteLine("{0} rolled {1}. That gives them a score of {2}{3}", playerName, roll, newScore, Environment.NewLine);
            
            if (newScore >= WINNING_SCORE)
            {
                Console.WriteLine(playerName + " Is The Winner");
                return true;
            }

            return false;
        }

        // This picks a first player at random. This is because statistically, the first player will usually win
        static private int PickFirstPlayer()
        {
            Random rnd = new Random();
            return rnd.Next(1, 4);
        }

    }
}
