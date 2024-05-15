using Daryl___Hangman.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Daryl___Hangman
{
    public partial class Form1 : Form
    {
        public enum Achievement
        {
            FirstWin,
            PerfectGame,
            HighScore,
            // Add more achievements as needed
        }
        public enum Difficulty
        {
            Easy,
            Medium,
            Hard
        }
        public enum WordCategory
        {
            General,
            Technology,
            Animals,
            Countries,
            Movies
        }
        private List<string> letters = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        private Dictionary<Achievement, bool> achievements = new Dictionary<Achievement, bool>();

        private string selectedWord;
        private int remainingLives = 8; // Initialize remaining lives
        private bool gameOver = false; // Flag to track if the game is over
        private int correctGuesses = 0; // Count of correctly guessed letters        

        private int currentHangmanAni = 0;
        Label lblHint = new Label();
        private int score = 0; // Variable to track the player's score
        private Dictionary<Difficulty, int> difficultyParameters = new Dictionary<Difficulty, int>()
        {
            { Difficulty.Easy, 3 },
            { Difficulty.Medium, 6 },
            { Difficulty.Hard, 8 }
        };

        private Dictionary<WordCategory, List<string>> wordCategories = new Dictionary<WordCategory, List<string>>()
{
    { WordCategory.General, new List<string> { "APPLE", "BANANA", "COMPUTER", "HOUSE", "UMBRELLA", "CAR", "TABLE", "CHAIR", "PHONE", "BOOK",
                                                "BALL",  } },

    { WordCategory.Technology, new List<string> { "PROGRAMMING", "KEYBOARD", "MOUSE", "SOFTWARE", "INTERNET", "DATABASE", "ALGORITHM", "HARDWARE", "INTERFACE", "WEBSITE",
                                                    } },

    { WordCategory.Animals, new List<string> { "DOG", "CAT", "ELEPHANT", "LION", "TIGER", "RABBIT", "GOAT", "COW", "HORSE", } },

    { WordCategory.Countries, new List<string> { "USA", "CANADA", "INDIA", "JAPAN", "FRANCE", "GERMANY", "AUSTRALIA", "BRAZIL", "MEXICO", } },

    { WordCategory.Movies, new List<string> { "INCEPTION", "TITANIC", "AVATAR", "INTERSTELLAR", "JAWS", "MATRIX", "GLADIATOR", "FORREST GUMP", "PULP FICTION", "THE SHAWSHANK REDEMPTION",
                                               } }
};


        private Dictionary<string, string> wordHints = new Dictionary<string, string>()
{
                     { "APPLE", "A fruit often associated with teachers" },
                    { "BANANA", "A yellow fruit often found in tropical regions" },
                    { "COMPUTER", "An electronic device used for processing data" },
                    { "HOUSE", "A building used for human habitation" },
                    { "UMBRELLA", "An object used to protect against rain or sunlight" },
                    { "CAR", "A four-wheeled vehicle used for transportation" },
                    { "TABLE", "A piece of furniture with a flat top and one or more legs" },
                    { "CHAIR", "A piece of furniture designed for sitting on" },
                    { "PHONE", "A device used for communication over long distances" },
                    { "BOOK", "A written or printed work consisting of pages glued or sewn together" },
                    { "BALL", "A spherical object used in games and sports" },

                    { "PROGRAMMING", "The process of writing instructions for computers to execute" },
                    { "KEYBOARD", "An input device used to type characters into a computer" },
                    { "MOUSE", "A pointing device used to interact with graphical user interfaces" },
                    { "SOFTWARE", "Programs and data that provide instructions for a computer" },
                    { "INTERNET", "A global network that connects millions of computers worldwide" },
                    { "DATABASE", "An organized collection of data stored electronically" },
                    { "ALGORITHM", "A sequence of steps to solve a problem or perform a task" },
                    { "HARDWARE", "Physical components of a computer system" },
                    { "INTERFACE", "A point where two systems, subjects, organizations, etc., meet and interact" },
                    { "WEBSITE", "A collection of web pages accessible through the internet" },

                    { "DOG", "A domesticated mammal commonly kept as a pet" },
                    { "CAT", "A small domesticated carnivorous mammal with soft fur" },
                    { "ELEPHANT", "A large mammal with a trunk and tusks, native to Africa and Asia" },
                    { "LION", "A large carnivorous feline mammal found in Africa and Asia" },
                    { "TIGER", "A large predatory cat with stripes found in Asia" },
                    { "RABBIT", "A small mammal with long ears and a fluffy tail" },
                    { "GOAT", "A domesticated mammal commonly raised for its milk, meat, and wool" },
                    { "COW", "A large domesticated mammal used for its milk and meat" },
                    { "HORSE", "A large domesticated mammal used for riding, racing, and carrying loads" },

                    { "USA", "United States of America, a country in North America" },
                    { "CANADA", "A country in North America known for its vast landscapes and friendly people" },
                    { "INDIA", "A country in South Asia known for its rich history and diverse culture" },
                    { "JAPAN", "An island country in East Asia known for its technology and tradition" },
                    { "FRANCE", "A European country known for its art, cuisine, and fashion" },
                    { "GERMANY", "A European country known for its engineering and beer" },
                    { "AUSTRALIA", "A country and continent surrounded by the Indian and Pacific oceans" },
                    { "BRAZIL", "The largest country in South America, known for its Amazon rainforest and carnival" },
                    { "MEXICO", "A country in North America known for its tacos, tequila, and mariachi music" },

                    { "INCEPTION", "A science fiction film directed by Christopher Nolan" },
                    { "TITANIC", "A romantic disaster film directed by James Cameron" },
                    { "AVATAR", "A science fiction film directed by James Cameron" },
                    { "INTERSTELLAR", "A science fiction film directed by Christopher Nolan" },
                    { "JAWS", "A thriller film directed by Steven Spielberg" },
                    { "MATRIX", "A science fiction action film directed by the Wachowskis" },
                    { "GLADIATOR", "A historical drama film directed by Ridley Scott" },
                    { "FORREST GUMP", "A comedy-drama film directed by Robert Zemeckis" },
                    { "PULP FICTION", "A crime film directed by Quentin Tarantino" },
                    { "THE SHAWSHANK REDEMPTION", "A drama film directed by Frank Darabont" }





};


        private WordCategory currentCategory = WordCategory.General;
        private Difficulty currentDifficulty = Difficulty.Medium;
        private string hint;
        private Label categoryLabel;
        public Panel pnlLetters = new Panel();
        public PictureBox heart = new PictureBox();
        
        private Button letterButton = new Button();
        private MenuStrip menuStrip = new MenuStrip();
        public Form1()
        {
            InitializeComponent();
            Gamestart();
        }


        private void Gamestart()
        {

            InitializeButtons();
            InitializeGame();
            InitializeCategoryLabel();
            InitializeMenu();            
            
        }
        private void InitializeGame()
        {
            //Form form = new Form();
            //foreach (Control control in form.Controls)
            //{


            //    foreach (Control c in form.Controls)
            //    {
            //        c.Enabled = false;
            //    }

            //}
            
            Random random = new Random();

            // Filter words based on the selected difficulty level's range of letters
            List<string> categoryWords = wordCategories[currentCategory]
                .Where(word => word.Length >= difficultyParameters[currentDifficulty])
                .ToList();

            // If the difficulty is not Hard, further filter the words
            if (currentDifficulty != Difficulty.Hard)
            {
                categoryWords = categoryWords
                    .Where(word => word.Length <= difficultyParameters[currentDifficulty + 1])
                    .ToList();
            }

            // Select a random word from the filtered list
            selectedWord = categoryWords[random.Next(categoryWords.Count)];

            // Add buttons for each letter of the word
            int buttonWidth = 50;
            int buttonHeight = 50;
            int startX = 30; // Center the buttons horizontally

            foreach (char letter in selectedWord)
            {
                letterButton = new Button();
                if (letter == ' ') // Check if the letter is a space
                {
                    // If it's a space, set the text of the button to an empty string and change its back color to black
                    letterButton.BackColor = Color.Black;
                    letterButton.Text = "";
                }
                else
                {
                    // Otherwise, set the text of the button to "_"
                    letterButton.Text = "_";
                }

                letterButton.Width = buttonWidth;
                letterButton.Height = buttonHeight;
                letterButton.Top = 100;
                letterButton.Location = new Point(10, 650);
                letterButton.Left = startX;
                letterButton.Tag = letter.ToString();
                letterButton.ForeColor = Color.White;
                letterButton.BackColor = Color.Orange;
                letterButton.BringToFront();
                startX += buttonWidth + 5;


                Controls.Add(letterButton);


                letterButton.Click += Button_Click;
            }
            // Retrieve the hint for the selected word
            hint = wordHints[selectedWord];



        }
       

        private void InitializeMenu()
        {
            menuStrip = new MenuStrip();

            ToolStripMenuItem gameMenu = new ToolStripMenuItem("Game");
            gameMenu.DropDownItems.Add("New Game", null, NewGame_Click);
            gameMenu.DropDownItems.Add("Exit", null, Exit_Click);
            menuStrip.Items.Add(gameMenu);

            ToolStripMenuItem difficultyMenu = new ToolStripMenuItem("Difficulty");
            difficultyMenu.DropDownItems.Add("Easy", null, Easy_Click);
            difficultyMenu.DropDownItems.Add("Medium", null, Medium_Click);
            difficultyMenu.DropDownItems.Add("Hard", null, Hard_Click);
            menuStrip.Items.Add(difficultyMenu);

            ToolStripMenuItem categoryMenu = new ToolStripMenuItem("Category");
            foreach (WordCategory category in Enum.GetValues(typeof(WordCategory)))
            {
                categoryMenu.DropDownItems.Add(category.ToString(), null, Category_Click);
            }
            menuStrip.Items.Add(categoryMenu);


            this.Controls.Add(menuStrip);
            this.MainMenuStrip = menuStrip;
        }
        private void InitializeButtons()
        {
            Color buttonBackColor = Color.Orange; // Example vibrant color
            Color buttonForeColor = Color.White; // Example text color


            pnlLetters.Location = new Point(950, 130); // Example panel location
            pnlLetters.Size = new Size(320, 570); // Example panel size
            pnlLetters.BackColor = Color.DarkOrange; // Example panel background color

            Controls.Add(pnlLetters); // Add panel to the form

            int buttonX = 10; // Starting X-coordinate for the first button
            int buttonY = 10; // Starting Y-coordinate for the first button
            int buttonWidth = 70; // Width of each button
            int buttonHeight = 70; // Height of each button

            for (int i = 0; i < letters.Count; i++)
            {
                Button button = new Button();
                button.Text = letters[i];
                button.Name = letters[i].ToString();
                button.Size = new Size(buttonWidth, buttonHeight); // Set button size
                button.FlatStyle = FlatStyle.Flat;
                button.Font = new Font("Comic Sans MS", 20);

                // Set button colors
                button.BackColor = buttonBackColor;
                button.ForeColor = buttonForeColor;

                // Position the button based on its index in the list
                button.Location = new Point(buttonX, buttonY);

                // Update position for the next button
                buttonX += buttonWidth + 6; // Move right for the next button
                if (buttonX + buttonWidth > pnlLetters.Width)
                {

                    buttonX = 10; // Reset X-coordinate for the next row
                    buttonY += buttonHeight + 10; // Move down for the next row                    
                }
                if (button.Text == "D")
                {
                    buttonY = 90;
                    buttonX = 10;
                }
                if (button.Text == "H")
                {
                    buttonY = 170;
                    buttonX = 10;
                }
                if (button.Text == "X")
                {

                    buttonX = 85;
                }





                // Add event handler (optional):
                button.Click += Button_Click;

                pnlLetters.Controls.Add(button);
            }
        }
        private void InitializeCategoryLabel()
        {
            // Display the hint in the hint label
            lblHint = new Label();
            lblHint.ForeColor = Color.White;
            lblHint.Location = new Point(300, 50);
            lblHint.Font = new Font("Century Gothic", 10);
            lblHint.AutoSize = true;
            lblHint.Text = $"Hint: {hint}";
            Controls.Add(lblHint);

            lblHint = new Label();
            lblHint.ForeColor = Color.White;
            lblHint.Location = new Point(950, 80);
            lblHint.Font = new Font("Arial", 11, FontStyle.Bold);
            lblHint.AutoSize = true;
            lblHint.Text = $"Difficulty: {currentDifficulty}";
            Controls.Add(lblHint);

            lblHint = new Label();
            lblHint.ForeColor = Color.White;
            lblHint.Location = new Point(950, 110);
            lblHint.Font = new Font("Arial", 11, FontStyle.Bold);
            lblHint.AutoSize = true;
            lblHint.Text = $"Lives: {remainingLives}";
            Controls.Add(lblHint);


            categoryLabel = new Label();
            categoryLabel.Text = "Category: " + currentCategory.ToString();
            categoryLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            categoryLabel.AutoSize = true;
            categoryLabel.Location = new Point(950, 50); // Adjust position as needed
            categoryLabel.ForeColor = Color.White;
            this.Controls.Add(categoryLabel);


            
        }
        private void Button_Click(object sender, EventArgs e)
        {
            bool letterFound = false;
            lblHint.Text = $"Lives: {remainingLives}";
            hangmanPictureBox.BackColor = Color.Transparent;
            hangmanPictureBox.Location = new Point(80, 80);
            hangmanPictureBox.Size = new Size(617, 565);
            if (gameOver)
                return;

            Button clickedButton = (Button)sender;
            string guessedLetter = clickedButton.Text;

            // Ensure that guessed letter is either all uppercase or all lowercase
            if (!string.IsNullOrEmpty(guessedLetter) && guessedLetter.All(char.IsLetter))
            {
                guessedLetter = guessedLetter.ToUpper(); // Convert guessed letter to uppercase


                for (int i = 0; i < selectedWord.Length; i++)
                {
                    if (char.ToUpper(selectedWord[i]) == guessedLetter[0])
                    {
                        foreach (Control control in Controls)
                        {
                            if (control is Button letterButton && letterButton.Tag != null && letterButton.Tag.ToString() == guessedLetter && letterButton.Text == "_")
                            {
                                letterButton.Text = guessedLetter;
                                letterFound = true;
                                if (selectedWord[i] != ' ')
                                {
                                    correctGuesses++;
                                    score += 10;
                                }
                                clickedButton.BackColor = Color.FromArgb(155, 162, 215);
                                clickedButton.Enabled = false;
                            }
                        }
                    }
                }

                if (!letterFound)
                {

                    remainingLives--;
                    score -= 5;
                    currentHangmanAni++;
                    UpdateHangmanPictureBox();
                    if (remainingLives == 0)
                    {
                        
                        MessageBox.Show("Game Over! You ran out of lives.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        gameOver = true;
                        currentHangmanAni = 0;
                    }

                    

                }

                int actualWordLength = selectedWord.Replace(" ", "").Length;
                if (correctGuesses == actualWordLength)
                {
                    MessageBox.Show("Congratulations! You've guessed the word.", "Victory!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gameOver = true;
                    ResetGame(); // Restart the game after the user clicks OK

                }
            }


            foreach (Control button in pnlLetters.Controls)
            {
                if (button == clickedButton)
                {
                    button.Visible = false;
                }

            } 

        }
        private void UpdateHangmanPictureBox()
        {
            if (currentHangmanAni >= 0 && currentHangmanAni <= 7)
            {
                hangmanPictureBox.BackgroundImage = (Bitmap)Resources.ResourceManager.GetObject($"h{currentHangmanAni + 1}");
            }
        }

        private void ChangeDifficulty(Difficulty newDifficulty)
        {
            currentDifficulty = newDifficulty;


            // Reset the game
            ResetGame();
        }
        private void ChangeCategory(WordCategory newCategory)
        {
            currentCategory = newCategory;
            ResetGame();
        }
        private void ResetGame()
        {
            correctGuesses = 0;
            gameOver = false;
            remainingLives = 8; currentHangmanAni = 0;
            foreach (Control control in Controls.OfType<Control>().ToArray())
            {
                if (control != hangmanPictureBox)
                {
                    Controls.Remove(control);
                }
            }
            
            Gamestart();
            
        }
        private void NewGame_Click(object sender, EventArgs e)
        {
            // Implement new game functionality
            ResetGame();


        }
        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Easy_Click(object sender, EventArgs e)
        {
            ChangeDifficulty(Difficulty.Easy);
        }
        private void Medium_Click(object sender, EventArgs e)
        {
            ChangeDifficulty(Difficulty.Medium);
        }
        private void Hard_Click(object sender, EventArgs e)
        {
            ChangeDifficulty(Difficulty.Hard);
        }
        private void Category_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            WordCategory selectedCategory;
            if (Enum.TryParse(menuItem.Text, out selectedCategory))
            {
                ChangeCategory(selectedCategory);
            }
        }
        private void InitializeAchievements()
        {
            foreach (Achievement achievement in Enum.GetValues(typeof(Achievement)))
            {
                achievements.Add(achievement, false); // Initialize all achievements as locked
            }
        }

    }
}
