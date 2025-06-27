// Import necessary namespaces
using System;
using System.Collections;


// Define the namespace for the Chatbot class
namespace poe_final
{
    // Define a class to hold question-answer pairs
    public class Reply
    {
        public string Question { get; set; }
        public string Answer { get; set; }

        public Reply(string question, string answer)
        {
            Question = question;
            Answer = answer;
        }
    }

    // Define the Chatbot class
    public class Chatbot
    {
        // Define private fields for the username and replies
        private string username;
        private string userAsk;
        private ArrayList replies;

        // Define the constructor for the Chatbot class
        public Chatbot()
        {


            // Initialize the replies ArrayList
            replies = new ArrayList();

            // Display a message to the user
            Console.WriteLine("Mini AI > ");
            Console.WriteLine("Enter your Name");
            Console.WriteLine("You > ");

            // Get the username from the user
            username = Console.ReadLine();



            // Store replies in the ArrayList
            StoreReplies();

            // Enter a loop to handle user input
            do
            {
                // Display a message to the user
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Mini AI > ");
                Console.WriteLine(" Good day " + username + ", how can I help you today?");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(username + " > ");
                // Get the user's input
                userAsk = Console.ReadLine();

                // Answer the user's question
                Answer(userAsk);

            } while (userAsk.ToLower() != "exit");
        }

        // Define a method to store replies in the ArrayList
        private void StoreReplies()
        {
            // Add replies to the ArrayList
            replies.Add(new Reply("password", "Make sure your password is secure"));
            replies.Add(new Reply("phishing", "Phishing is a cybercrime in which a target is contacted by email by someone posing as a legitimate institution"));
            replies.Add(new Reply("safe browsing", "Safe browsing is a service that protects users from dangerous websites"));
            replies.Add(new Reply("how", "I am good and yourself?"));
            replies.Add(new Reply("name", "My Name is mini AI"));
            replies.Add(new Reply("purpose", "I am here to help you find answers to your questions"));
            replies.Add(new Reply("ask", "About cyber security"));

            replies.Add(new Reply("curious", "i am glad you are curious"));
            replies.Add(new Reply("privacy", "Adjust your social media privacy settings to limit who can see your information."));
            replies.Add(new Reply("frustrated", "I hear you! Cybersecurity issues can be frustrating. Let's work through it together."));
            replies.Add(new Reply("worried", "I understand that cybersecurity can feel overwhelming. Take it one step at a time!"));



        }

        // Define a method to answer the user's question
        private void Answer(string asked)
        {

            // Split the user's input into keywords
            string[] keywords = asked.ToLower().Split(new[] { ' ', ',', ';', '.' }, StringSplitOptions.RemoveEmptyEntries);
            bool foundReply = false;

            // Check each keyword against the replies in the ArrayList
            foreach (string keyword in keywords)
            {
                foreach (Reply reply in replies)
                {
                    if (reply.Question.Equals(keyword, StringComparison.OrdinalIgnoreCase))
                    {
                        // Display the reply to the user
                        Console.WriteLine("Mini AI: " + reply.Answer);

                        foundReply = true;
                    }

                }
            }

            // If no replies were found, display a default message
            if (!foundReply)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Mini AI: Sorry, I don't understand your question.");


            }
            else
            {

            }


        }



    }


}