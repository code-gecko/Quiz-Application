using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace QuizApplication
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private List<QuestionBankDataModel> questions;
        private int currentQuestionindex;
        private int score;

        private DispatcherTimer timer;

        private int timeleft;


        public MainPage()
        {
            this.InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += Timer_Tick;

            // the load is going to assign fields to our data model properties
            loadQuestion();
            //this set is going to send loaded data to our UI
            setQuestion();
        }
        private void Timer_Tick (Object Sender,object e)
        {
            timeleft--;
            txtTimer.Text = timeleft.ToString();
            if (timeleft <=0)
            {
                timer.Stop();
                currentQuestionindex++;
                setQuestion();

            }
        }

        private void loadQuestion()
        {
            questions = new List<QuestionBankDataModel>()
            {
                new QuestionBankDataModel()
                {
                    OurQuestionTest="What is the capital city of Uganda",
                    OurOption1="Nairobi",
                    OurOption2="Kampala",
                    OurOption3="Kigali",
                    OurOption4="Darel Salaam",
                    OurCorrectAnswer=2
                 },
                new QuestionBankDataModel()
                {
                    OurQuestionTest="What is the capital city of Kenya",
                    OurOption1="Nairobi",
                    OurOption2="Kampala",
                    OurOption3="Kigali",
                    OurOption4="Darel Salaam",
                    OurCorrectAnswer=1

                },
                new QuestionBankDataModel()
                {
                    OurQuestionTest="What is the capital city of Rwanda",
                    OurOption1="Nairobi",
                    OurOption2="Kampala",
                    OurOption3="Kigali",
                    OurOption4="Darel Salaam",
                    OurCorrectAnswer=3

                },
                new QuestionBankDataModel()
                {
                    OurQuestionTest="What is the capital city of Tanzania",
                    OurOption1="Nairobi",
                    OurOption2="Kampala",
                    OurOption3="Kigali",
                    OurOption4="Darel Salaam",
                    OurCorrectAnswer=4

                },
            };
        }

        private void setQuestion()
        {
            // this codition is checking where there is a question from the question bank
           
            if (currentQuestionindex< questions.Count)
            {
                var currentquestion = questions[currentQuestionindex];
                txtQuestiontext.Text = currentquestion.OurQuestionTest;
                radiooptiona.Content = currentquestion.OurOption1;
                radiooptionb.Content = currentquestion.OurOption2;
                radiooptionc.Content = currentquestion.OurOption3;
                radiooptiond.Content = currentquestion.OurOption4;
                //we are going to set the time for each question
                //thi is also the rest
                timeleft = 5;
                timer.Stop();
                txtTimer.Text = timeleft.ToString();
                timer.Start();
            }
            else
            {
                txtQuestiontext.Text = "quiz is done";
                radiooptiona.Visibility = Visibility.Collapsed;
                radiooptionb.Visibility = Visibility.Collapsed;
                radiooptionc.Visibility = Visibility.Collapsed;
                radiooptiond.Visibility = Visibility.Collapsed;
                txtScore.Text = $"Your Score is {score}/{questions.Count}";
                if (score == 2)
                {
                    txtText.Text = "Your are average";
                }else if (score > 2)
                {
                    txtText.Text = "you are Excellent";
                }else if (score < 2)
                {
                    txtText.Text = "You aren't competent yet";
                }
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (radiooptiona.IsChecked == false && radiooptionb.IsChecked == false && radiooptionc.IsChecked == false
                && radiooptiond.IsChecked == false)
            {
                return;
            }
            var currentquestion = questions[currentQuestionindex];
            var mostcorrectAnswer = currentquestion.OurCorrectAnswer;

            if (radiooptiona.IsChecked == true && mostcorrectAnswer == 1|| 
                radiooptionb.IsChecked == true && mostcorrectAnswer == 2||
                radiooptionc.IsChecked == true && mostcorrectAnswer == 3 ||
                radiooptiond.IsChecked == true && mostcorrectAnswer == 4)
                {
                score++;
                txtText.Text = "Your answer is correct";
            }
            else
            {
                txtText.Text = "your answer is wrong";
            }
            currentQuestionindex++;
            setQuestion();
            radiooptiona.IsChecked = false;
            radiooptionb.IsChecked = false;
            radiooptionc.IsChecked = false;
            radiooptiond.IsChecked = false;
        }
    }
}
