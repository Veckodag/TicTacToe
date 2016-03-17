using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tenta20151218
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Fredrik Sandströms Awesome Tic Tac Toe Game
        
        //Global list of the gamebuttons
        List<Button> myButtons = new List<Button>();

        //A three dimensional int array that holds the game winning combinations.
        static private int[,] Winners = new int[,]
        {
            {0,1,2},
            {3,4,5},
            {6,7,8},
            {0,3,6},
            {1,4,7},
            {2,5,8},
            {0,4,8},
            {2,4,6}
        };

        //Int that checks whoms turn it is. When it's 0 it's O turn and when it's 1 it's X turn
        private int PlayerTurn = 0;

        public MainWindow()
        {
            InitializeComponent();
            AddingButtonsToList();
        }

        //Fills the list with the nine game buttons for easy access in other methods
        public void AddingButtonsToList()
        {
            myButtons.Add(FirstButton);
            myButtons.Add(SecondButton);
            myButtons.Add(ThirdButton);
            myButtons.Add(FourthButton);
            myButtons.Add(FifthButton);
            myButtons.Add(SixthButton);
            myButtons.Add(SeventhButton);
            myButtons.Add(EigthButton);
            myButtons.Add(NinthButton);
        }
        // Extra functionality when winner is declared. Disables the gamebuttons for further clicks
        public void YesYouAreAWinner()
        {
            foreach (var myButton in myButtons)
                myButton.IsEnabled = false;
            ExtraInfo.Content = "Start a new game to play more!";
        }

        // Main Game Logic. Checks if you are a winner by comparing the buttons index against the Winners int array.
        public void AreYouAWinner(List<Button> selectedButtons)
        {
            for (int i = 0; i < 8; i++)
            {
                //Goes through all the values in the Winners intarray, and compares them to the selectedButtons index
                int a = Winners[i, 0], b = Winners[i, 1], c = Winners[i, 2];
                Button b1 = selectedButtons[a], b2 = selectedButtons[b], b3 = selectedButtons[c];

                //If there is no match between the 3 values needed to win, it continues.
                if (string.IsNullOrEmpty((string)b1.Content) || string.IsNullOrEmpty((string)b2.Content) || string.IsNullOrEmpty((string)b3.Content))
                    continue;

                //If the 3 values are the same, a winner is declared with a nice messagebox
                if (b1.Content == b2.Content && b2.Content == b3.Content)
                {
                    PlayersTurnLabel.Content = $"The winner is {b1.Content}!";
                    PlayersTurnLabel.Foreground = Brushes.Green;
                    MessageBox.Show($"The winner is {b1.Content}!", "Congratulations");
                    YesYouAreAWinner();
                    break;
                }
            }
        }
        #region GameButtonClickEvents
        //The click events for all the game buttons
        private void GameButton_OnClick(object sender, RoutedEventArgs e)
        {
            CheckTheButton((Button)sender);
        }

        #endregion

        //Game Logic. Checks whos turn it is, sets the right mark (X or O) and then disables the button.
        //Also calls methods for checking if you won or if it's a draw
        public void CheckTheButton(Button selectedButton)
        {
            if (PlayerTurn == 0)
            {
                selectedButton.Content = "O";
                PlayerTurn = 1;
                PlayersTurnLabel.Content = "X Turn";
            }
            else
            {
                selectedButton.Content = "X";
                PlayerTurn = 0;
                PlayersTurnLabel.Content = "O Turn";
            }
            IsItADraw(myButtons);
            AreYouAWinner(myButtons);
            selectedButton.IsEnabled = false;
        }

        //Tells the player if the game is a draw. Executed when you click the game buttons and there is no more game buttons to press
        private void IsItADraw(List<Button> selectedButtons)
        {
            int count = selectedButtons.Count(selectedButton => selectedButton.IsEnabled == false);
            if (count == 8)
            {
                PlayersTurnLabel.Content = "It's a draw! Start a new game!";
                PlayersTurnLabel.Foreground = Brushes.Red;
            }
        }

        // Starts a new game. Enables all the game button, clears the content and sets the playerTurn to O
        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button myButton in myButtons)
            {
                myButton.IsEnabled = true;
                myButton.Content = "";
            }
            PlayersTurnLabel.Content = "A new game begins! It's player O who goes first!";
            PlayersTurnLabel.Foreground = Brushes.Black;
            ExtraInfo.Content = "";
            PlayerTurn = 0;
        }

        //Application exit message
        private void EndGameButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thanks for playing!", "Great Tic Tac Toe Adventures");
            Application.Current.Shutdown();
        }
    }
}
