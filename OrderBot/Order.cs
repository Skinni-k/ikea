using System;
using Microsoft.Data.Sqlite;

namespace OrderBot
{
    public class Order
    {
        private Status status;
        private static SqliteConnection connection;
        private MealSearch mealsearch = new MealSearch();
        private static void Connect()
        {
            connection = Sqlite.GetConnection();
            connection.Open();
        }
        public Order()
        {
            Connect();
        }

        private Status getStatus(Boolean success)
        {
            this.status.nextState(connection, success);
            return this.status;
        }

        public String OnMessage(String sInMessage)
        {
            String sMessage, append = "\n";

            if (this.status == null)
            {
                // Create Status with first status
                this.status = new Status(connection);
                sMessage = this.status.message;
                System.Diagnostics.Debug.WriteLine(sMessage);
                return sMessage;
            }
            Boolean valid = this.status.validValue(sInMessage);
            if (!valid) return this.status.message;

            dynamic value;
            if (this.status.type == "Number") value = Int32.Parse(sInMessage);
            else value = sInMessage;

            switch (this.status.name)
            {
                case "PROTEINS":
                    this.mealsearch.proteins = value;
                    valid = mealsearch.searchMeal(connection);
                    for (int i = 0; i < mealsearch.meals.Count; i++)
                    {
                        int j = i + 1;
                        append += "\n" + j + ". " + mealsearch.meals[i];
                    }
                    break;
                case "FATS":
                    this.mealsearch.fats = value;
                    break;
                case "CALORIES":
                    this.mealsearch.calories = value;
                    break;
                case "MEAL_FOUND":
                    if (value > 3 || value < 1)
                    {

                        for (int i = 0; i < mealsearch.meals.Count; i++)
                        {
                            int j = i + 1;
                            append += "\n" + j + ". " + mealsearch.meals[i];
                        }
                        append += "\nPlease type a number between 1 and 3";
                        valid = false;
                    }
                    else
                    {
                        mealsearch.selectedMeal = mealsearch.meals[value - 1];
                        append += "\n" + mealsearch.selectedMeal;
                    }
                    break;
                case "CONFIRM_MEAL":
                    if (sInMessage.ToLower() == "yes")
                    {
                        append += "http://localhost:5000/payment?meal=" + mealsearch.selectedMeal;
                        valid = true;
                    }
                    else valid = false;
                    break;
            }

            status = this.getStatus(valid);
            sMessage = this.status.message + append;
            System.Diagnostics.Debug.WriteLine(sMessage);
            return sMessage;
        }
    }
}
