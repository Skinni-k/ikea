using System;

namespace OrderBot
{
    public class Order
    {
        private enum State
        {
            WELCOMING, GUESSING
        }

        private State nCur = State.WELCOMING;
        private int nNumber;

        private string welcomeMessage = @"Welcome to BENN's curbside pickup ordering platform
        
        Please enter the amount of carbs, proteins and fats you require in your food in this order in comma separated format, 
        for example: 8,9,2 would mean you want 8 grams of carbs, 9 grams of protein and 2 grams of fats";
        public Order(int nTest = -1)
        {
            if(nTest == -1){
                Random rnd = new Random();
                this.nNumber = rnd.Next(1, 99);

            }else{
                this.nNumber = nTest;
            }

        }

        public String OnMessage(String sInMessage)
        {
            String sMessage = this.welcomeMessage;
            switch (this.nCur)
            {
                case State.WELCOMING:
                    this.nCur = State.GUESSING;
                    break;
                case State.GUESSING:
                    try
                    {
                        int nGuess = Int32.Parse(sInMessage);
                        if (nGuess > this.nNumber)
                        {
                            sMessage = "Too high";
                        }
                        else if (nGuess < this.nNumber)
                        {
                            sMessage = "Too low";
                        }
                        else
                        {
                            sMessage = "Just Right ... Guess my new number";
                            Random rnd = new Random();
                            this.nNumber = rnd.Next(1, 99);
                        }
                    }
                    catch (Exception)
                    {
                        sMessage = "Enter a number between 1 and 100";
                    }
                    break;

            }
            System.Diagnostics.Debug.WriteLine(sMessage);
            return sMessage;
        }

    }
}
