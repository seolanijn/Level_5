/*
 * Program:         CatchMind.exe
 * Module:          MainWindow.xaml.cs
 * Author:          Soohwan Kim, Seolan Jin, Junyeong Jo
 * Date:            30/03/2023
 * Description:     The application is a game where the host draws paintings and other users have to guess 
 *                  what the paintings are supposed to be. 
 */

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.ServiceModel;
using CatchMindLibrary;

namespace CatchMindGUIClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, UseSynchronizationContext = false)]
    public partial class MainWindow : Window, ICallback
    {
        private IGameInfo gameInfo = null;
        //id for each client
        private int id = 0;

        //Constructor
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                DuplexChannelFactory<IGameInfo> channel = new DuplexChannelFactory<IGameInfo>(this, "GameInfoPoint");
                gameInfo = channel.CreateChannel();

                // registers for callbacks and gets the instance id
                id = gameInfo.RegisterForCallbacks();

                //Prevents letting users in more than 4 people.
                if (id > 3)
                {
                    this.Close();
                }

                // the first player (user) is a drawer
                bool isDrawer = (id == 0) ? true : false;
                gameInfo.AddUser(id, isDrawer);

                gameInfo.SharedMessage = $"Player {id + 1} has joined.";

            }
            catch (ObjectDisposedException)
            {
                MessageBox.Show("This session is full now.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void drawingCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // if the user of the current instance is the drawer and the game is still running
                if (gameInfo.Users[id].IsDrawer && !gameInfo.IsGameOver)
                {
                    gameInfo.IsDrawing = true;
                    gameInfo.LastPoint = e.GetPosition(drawingCanvas);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void drawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                // if the user of the current instance is the drawer and the game is still running
                if (gameInfo.Users[id].IsDrawer && !gameInfo.IsGameOver)
                {
                    if (gameInfo.IsDrawing)
                    {

                        gameInfo.X1 = gameInfo.LastPoint.X;
                        gameInfo.Y1 = gameInfo.LastPoint.Y;
                        gameInfo.X2 = e.GetPosition(drawingCanvas).X;
                        gameInfo.Y2 = e.GetPosition(drawingCanvas).Y;

                        gameInfo.LastPoint = e.GetPosition(drawingCanvas);

                        gameInfo.IsClear = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private SolidColorBrush convertColor(string color)
        {
            switch (color)
            {
                case "Black":
                    return Brushes.Black;
                case "Red":
                    return Brushes.Red;
                case "Blue":
                    return Brushes.Blue;
                case "White":
                    return Brushes.White;
                default:
                    return Brushes.Black;
            }
        }

        private void drawingCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // if the user of the current instance is the drawer and the game is still running
                if (gameInfo.Users[id].IsDrawer && !gameInfo.IsGameOver)
                {
                    gameInfo.IsDrawing = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDraw_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // only for the drawer
                if (gameInfo.Users[id].IsDrawer)
                {
                    gameInfo.BrushColour = "Black";
                    gameInfo.BrushThickness = 3;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // only for the drawer
                if (gameInfo.Users[id].IsDrawer)
                {
                    gameInfo.BrushColour = "White";
                    gameInfo.BrushThickness = 30;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // only for the drawer
                if (gameInfo.Users[id].IsDrawer)
                {
                    gameInfo.IsClear = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void redRadioBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // only for the drawer
                if (gameInfo.Users[id].IsDrawer)
                {
                    gameInfo.BrushColour = "Red";
                    gameInfo.BrushThickness = 3;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void blueRadioBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // only for the drawer
                if (gameInfo.Users[id].IsDrawer)
                {
                    gameInfo.BrushColour = "Blue";
                    gameInfo.BrushThickness = 3;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void blackRadioBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // only for the drawer
                if (gameInfo.Users[id].IsDrawer)
                {
                    gameInfo.BrushColour = "Black";
                    gameInfo.BrushThickness = 3;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (gameInfo.Users[id].IsDrawer) // only for the drawer
            {
                gameInfo.Answer = wordTextBox.Text; // set an answer
                gameInfo.SharedMessage = $"The game is now ready. The length of the answer is [{gameInfo.Answer.Length}].";
                gameInfo.IsAnswerSet = true;
            }
            else
            {
                if (gameInfo.Answer.ToLower() == wordTextBox.Text.ToLower()) // Correct Answer
                {
                    gameInfo.AddScore(id);
                    if (gameInfo.Users[id].IsWinner()) // Game Over (The score of the current user hit the goal score)
                    {
                        gameInfo.SharedMessage = $"Game Over. The Winner is Player {id + 1}";
                        gameInfo.IsGameOver = true;
                    }
                    else
                    {
                        gameInfo.SharedMessage = $"Player {id + 1} won this round! The answer was [{gameInfo.Answer}].";
                    }
                    gameInfo.IsAnswerSet = false;
                }
                else // Wrong Answer
                {
                    gameInfo.SharedMessage = $"Player {id + 1} has entered a wrong answer. Try again!";
                }
            }
        }

        public void UpdateClient(CallbackInfo info, int key)
        {
            try
            {
                if (System.Threading.Thread.CurrentThread == this.Dispatcher.Thread)
                {
                    id = key; // update the current instance id

                    sharedGameState.Content = gameInfo.SharedMessage; // set the shared game state for all players

                    // Draw
                    Line line = new Line();
                    line.Stroke = convertColor(gameInfo.BrushColour);
                    line.StrokeThickness = gameInfo.BrushThickness;
                    line.X1 = gameInfo.X1;
                    line.Y1 = gameInfo.Y1;
                    line.X2 = gameInfo.X2;
                    line.Y2 = gameInfo.Y2;
                    drawingCanvas.Children.Add(line);

                    if (gameInfo.IsClear) // when the drawer clicks the Clear button
                    {
                        drawingCanvas.Children.Clear();
                    }
                    if (gameInfo.IsAnswerSet) // when the drawer sets the answer
                    {
                        btnSubmit.IsEnabled = true;
                    }
                    if (!gameInfo.IsAnswerSet)
                    {
                        if (gameInfo.Users[id].IsDrawer)
                        {
                            btnSubmit.IsEnabled = true;
                        }
                        else
                        {
                            btnSubmit.IsEnabled = false;
                        }
                    }

                    if (gameInfo.IsGameOver) // when the game is over
                    {
                        wordTextBox.IsEnabled = false;
                        btnSubmit.IsEnabled = false;
                    }

                    // updates the players list
                    User1Name.Content = gameInfo.Users.Count > 0 ? "Player 1" : "";
                    User2Name.Content = gameInfo.Users.Count > 1 ? "Player 2" : "";
                    User3Name.Content = gameInfo.Users.Count > 2 ? "Player 3" : "";
                    User4Name.Content = gameInfo.Users.Count > 3 ? "Player 4" : "";

                    // updates the score
                    User1Score.Content = gameInfo.Users.Count > 0 ? "N/A" : "";
                    User2Score.Content = gameInfo.Users.Count > 1 ? gameInfo.Users[1].Score.ToString() : "";
                    User3Score.Content = gameInfo.Users.Count > 2 ? gameInfo.Users[2].Score.ToString() : "";
                    User4Score.Content = gameInfo.Users.Count > 3 ? gameInfo.Users[3].Score.ToString() : "";

                    // reset the color
                    User1Name.Foreground = Brushes.White;
                    User1Score.Foreground = Brushes.White;
                    User1Drawer.Foreground = Brushes.White;
                    User2Name.Foreground = Brushes.White;
                    User2Score.Foreground = Brushes.White;
                    User3Name.Foreground = Brushes.White;
                    User3Score.Foreground = Brushes.White;
                    User4Name.Foreground = Brushes.White;
                    User4Score.Foreground = Brushes.White;

                    // sets the color of the user on the list
                    switch (id)
                    {
                        case 0:
                            User1Name.Foreground = Brushes.CornflowerBlue;
                            User1Score.Foreground = Brushes.CornflowerBlue;
                            User1Drawer.Foreground = Brushes.CornflowerBlue;
                            break;
                        case 1:
                            User2Name.Foreground = Brushes.CornflowerBlue;
                            User2Score.Foreground = Brushes.CornflowerBlue;
                            break;
                        case 2:
                            User3Name.Foreground = Brushes.CornflowerBlue;
                            User3Score.Foreground = Brushes.CornflowerBlue;
                            break;
                        case 3:
                            User4Name.Foreground = Brushes.CornflowerBlue;
                            User4Score.Foreground = Brushes.CornflowerBlue;
                            break;
                        default: break;
                    }
                }
                else
                {
                    Action<CallbackInfo, int> updateDelegate = UpdateClient;
                    this.Dispatcher.BeginInvoke(updateDelegate, info, key);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }       
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (id == 0) // when the drawer has left
            {
                gameInfo.SharedMessage = $"The drawer has left. The next player is now a drawer.";
            }
            else if (id > 3) // when an user tried to get in the full session
            {
                gameInfo.SharedMessage = $"An user tried to get in the full session.";
            }
            else // when a player has left
            {
                gameInfo.SharedMessage = $"Player {id + 1} has left.";
            }

            // Unsubscribe this client from the callbacks feature
            gameInfo.UnregisterFromCallbacks();

            // Formally close the WCF channel
            (gameInfo as IClientChannel)?.Close();
        }
    } // end class
}
