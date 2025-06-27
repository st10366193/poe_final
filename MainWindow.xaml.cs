using System.Net.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.ML;
using Microsoft.ML.Data;

namespace poe_final
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // declaration for instances and variables

        // list 

        private List< QuizQuestions > quizData;
        //variables 
        private int questionIndex = 0;
        int currentScore = 0;

        // buttons 
        private Button selectedChoice = null;
        private Button correctChoice = null;

        // GLOBAL INSTANCES AND VARIABLES 
        private class sentimentData
        {
            public string Text{ get; set; }
            public bool Label {  get; set; }
        }

        //detect or predictions class 
        private class sentimentPrediction

        {

            [ColumnName("PredictedLabel")]
            public bool Prediction{ get ; set;}

            public float Probability {  get; set;}

            public float Score {  get; set;}

        }
        // then variables 
        private readonly MLContext mlContext;
        private PredictionEngine<sentimentData, sentimentPrediction> engine;


        public MainWindow()
        {
            InitializeComponent();

            // call the load quiz method 
            LoadQuizData();
            //call show quiz
            ShowQuiz();

            //initialize NPL 
            mlContext = new MLContext();


            new voice() { };
            new logo() { };
             new Chatbot() { };

        }

        // method to train AI Prediction

        private void trainModel() {
            // train data model 

            var trainingData = new[]
            {
                new sentimentData{
                 Text="I am Happy",
                 Label=true
                },

                new sentimentData{
                 Text="I hate this",
                 Label=false
                },

                new sentimentData{
                 Text="I am sad",
                 Label=false
                },


            };
            // send the data to dataview

            var trainDataView = mlContext.Data.LoadFromEnumerable(trainingData);

            var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(sentimentData.Text)).Append(mlContext.BinaryClassification
                .Trainers.SdcaLogisticRegression(labelColumnName: "label", featureColumnName: "Feature"));

            // then send model to search 

            var model = pipeline .Fit(trainDataView);

            // the AI predicts as it trains 
            engine = mlContext.Model.CreatePredictionEngine<sentimentData,sentimentPrediction>(model);

                

            }// end of train model
        

        // method to showquiz on buttons  
        private void ShowQuiz()
        {
            // check if user is done playing 

            if (questionIndex >= quizData.Count) 
            
            {

                // show complete message 
                MessageBox.Show("You have completed the game " + currentScore + " score");

                // reset game 
                currentScore = 0;
                currentScore = 0;
                questionIndex = 0;
                ShowQuiz();
                return;
            
            }
            

            // get the current index quiz
            correctChoice = null;
            selectedChoice = null;

            // then gett all the questions 
            var currentQuiz = quizData[questionIndex];

            // display question

            DisplayQuestion.Text=currentQuiz.Question;

            // add the choices tothe buttons 
            var shuffled = currentQuiz.Choices.OrderBy( _=> Guid.NewGuid() ).ToList();

            // then add by index
            FirstchoiceButton.Content = shuffled[0];
            SecondchoiceButton.Content = shuffled[1];
            ThirdchoiceButton.Content = shuffled[2];
            //correct one 
            FourthchoiceButton.Content = currentQuiz.CorrectChoice;
        }

        //methodload the quiz data 

        private void LoadQuizData()
        {

            //store info 
            quizData = new List<QuizQuestions>  
            {
            
                new QuizQuestions { 
                 Question = "What is phishing",
                 CorrectChoice ="Phishing is a method attackers use to trick you into revealing personal information.",
                 Choices = new List< string >{"it is when you go out on a boat to catch fish ", 
                                              " It is when a Fish jumps on you ",
                                              " it is not a thing(its made up)" 
                 }
                },

                new QuizQuestions {
                 Question = "What is an example of a strong password ",
                 CorrectChoice ="A strong password should be at least 12 characters long with mixed symbols.",
                 Choices = new List< string >{"using a code you will not remeber ",
                                              "Stephen Hawkings birthday  ",
                                              " All of the above"
                 }
                },

                new QuizQuestions {
                 Question = "How do you ensure privacy on the internet ",
                 CorrectChoice ="Never share personal details publicly online—it can be exploited.",
                 Choices = new List< string >{"Tell everyone your password ",
                                              "Tell you parents your password ",
                                              " None of the above"
                 }
                },

                new QuizQuestions {
                 Question = "What is safe browsing ",
                 CorrectChoice ="Safe browsing is a service that protects users from dangerous websites",
                 Choices = new List< string >{"Safe browsing is when you go incagnito ",
                                              "is when you dont use the internet",
                                              " 1 and 3"
                 }
                },



            };

        }// end of load quiz data 

        private void show_chats_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
             // get selected item fro list 
             string selected_task = show_chats.SelectedItem.ToString();
            MessageBox.Show(selected_task);

            // check if task is not marked 

            if (!selected_task.Contains(" Done "))

            {
                // get index
                int getIndex = show_chats.Items.IndexOf(selected_task);


                //edit the selected item 
                show_chats.Items[getIndex] = selected_task + " Done ";



            }
            else 
            {
                // remove task if marked done 
                show_chats.Items.Remove(selected_task);
            }//end if
        }

        private void ask_question(object sender, RoutedEventArgs e)
        {
            // collect what the user enters 
            string collect_question = user_question.Text.ToString();

            // validate user entry 

            if (!collect_question.Equals(""))
            {

                // check if user wants to add a task

                if (collect_question.ToLower().Contains("add task")) 
                {
                // add  a task 

                DateTime date = DateTime.Now.Date;
                DateTime time = DateTime.Now.ToLocalTime();

                    // set format for date 
                    string format_date = date.ToString("yyyy/MM/dd");

                    // add to list
                    show_chats.Items.Add("User ; " + collect_question + "\n"+ format_date + "Time" + time );

                    // auto scroll 
                    show_chats.ScrollIntoView(show_chats.Items[show_chats.Items.Count - 1 ] ) ;
                
                }//End of if

            }
            else 
            {
                // error
                MessageBox.Show("Field is Required ");

            }//end of if

        }// end of question

        private void HandlerAnswerselection(object sender, RoutedEventArgs e)
        {

            // using sender to getselected button
            selectedChoice = sender as Button;
            string chosen = selectedChoice.Content.ToString();

            // check correct quiz 
            string correct = quizData[questionIndex].CorrectChoice;

            if (chosen == correct) 
            
            {
            // set background 
            selectedChoice.Background = Brushes.Green;
                correctChoice=selectedChoice;
            }
            else
            {
                // if incorrect
                selectedChoice.Background= Brushes.Red;
            }//end if 

        }//end of hanle answer selection

        private void HandleNextQuestion(object sender, RoutedEventArgs e)
        {
            // check if user selected one ofthe answer 
            if (selectedChoice==null)
                 
            {
                MessageBox.Show("Pick a choice ");
            }
            else 
            {
                string chosen = selectedChoice.Content.ToString();
                string correct = quizData[questionIndex].CorrectChoice;

                // check iff cooreecr
                if (chosen == correct ) 
                {

                    // add points 
                    currentScore++;
                    // show score
                    DisplayScore.Text= "Score ; " + currentScore;

                    // move to next question 
                    questionIndex++;
                    // show next question 
                    ShowQuiz();


                }
                else { questionIndex++; ShowQuiz(); }
            }//end if
        }

        private void emotions(object sender, RoutedEventArgs e)
        {
          // collect what user enters 
          string input =emotion.Text;


            //check if empty 
            if (string.IsNullOrWhiteSpace(input)) 
            
            {
            
                // then show error message 
                var prediction= engine.Predict(new sentimentData { Text = input });
                // show emotions 
                show_emotion_detected.Text = prediction.Prediction ? $"Positive emotion(confidence :{prediction.Prediction:P1}" 
                    : $"Negative emotion( Confidence : {prediction.Prediction:P1} )"; 
            }

        }
    }

    
}