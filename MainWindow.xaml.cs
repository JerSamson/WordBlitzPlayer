using System.Windows;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System;
using System.Windows.Media.Imaging;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace WordBlitzPlayer 
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        const string BASE_PATH = @"C:\Users\Jeremy\source\repos\WordBlitzPlayer\";
        const string REFS_PATH = BASE_PATH + @"\Images\LetterReferences";
        const string DEBUG_PATH = BASE_PATH + @"\Images\DebugScreenshot";
        const string DICT_PATH = BASE_PATH + @"\Dictionnaire.txt";
        const string TRASH_PATH = BASE_PATH + @"\Images\Trash";

        double fairnessLevel = 0;
        Random randomizer = new Random(DateTime.Now.Millisecond);

        private const int MaxTime = 82 * 1000; //ms
        private const int DelayBetweenLetters = 12; //ms
        private const int DelayBetweenWords = 12; //ms

        private System.Diagnostics.Stopwatch Gametimer = new System.Diagnostics.Stopwatch();


        public Calibration calibration = new Calibration();
        public List<System.Windows.Point> CalibrationPoints { get; set; } = new List<System.Windows.Point>();
        public double CalibrationWidth { get; set; } = 0;
        public double CalibrationHeight { get; set; } = 0;

        public bool IsPlayingWords { get; set; } = false;

        public string GuessesTextBoxContent
        {
            get => _GuessesTextBoxContent;
            set
            {
                if (_GuessesTextBoxContent == value) return;
                _GuessesTextBoxContent = value;
                OnPropertyChanged();
            }
        }
        private string _GuessesTextBoxContent { get; set; }

        public string CaptureDirectory;

        public System.Windows.Controls.Image[] ImageControls;

        public List<string> FoundWords { get; set; } = new List<string>();
        public List<string> exploredPaths { get; set; } = new List<string>();

        public List<List<LetterTile>> WordsToPlay = new List<List<LetterTile>>();

        bool debug = false;
        string debugFilePath = string.Empty;

        //System.Diagnostics.Stopwatch GetTilesStopWatch;
        //System.Diagnostics.Stopwatch ParseTilesStopWatch;

        #region Sources

        public BitmapImage Tile00Source
        {
            get => _Tile00Source;
            set
            {
                if (value == _Tile00Source) return;
                _Tile00Source = value;
                OnPropertyChanged();
            }
        }
        private BitmapImage _Tile00Source { get; set; } = new BitmapImage();

        public BitmapImage Tile01Source
        {
            get => _Tile01Source;
            set
            {
                if (value == _Tile01Source) return;
                _Tile01Source = value;
                OnPropertyChanged();
            }
        }
        private BitmapImage _Tile01Source { get; set; } = new BitmapImage();

        public BitmapImage Tile02Source
        {
            get => _Tile02Source;
            set
            {
                if (value == _Tile02Source) return;
                _Tile02Source = value;
                OnPropertyChanged();
            }
        }
        private BitmapImage _Tile02Source { get; set; } = new BitmapImage();

        public BitmapImage Tile03Source
        {
            get => _Tile03Source;
            set
            {
                if (value == _Tile03Source) return;
                _Tile03Source = value;
                OnPropertyChanged();
            }
        }
        private BitmapImage _Tile03Source { get; set; } = new BitmapImage();

        public BitmapImage Tile10Source
        {
            get => _Tile10Source;
            set
            {
                if (value == _Tile10Source) return;
                _Tile10Source = value;
                OnPropertyChanged();
            }
        }
        private BitmapImage _Tile10Source { get; set; } = new BitmapImage();

        public BitmapImage Tile11Source
        {
            get => _Tile11Source;
            set
            {
                if (value == _Tile11Source) return;
                _Tile11Source = value;
                OnPropertyChanged();
            }
        }
        private BitmapImage _Tile11Source { get; set; } = new BitmapImage();

        public BitmapImage Tile12Source
        {
            get => _Tile12Source;
            set
            {
                if (value == _Tile12Source) return;
                _Tile12Source = value;
                OnPropertyChanged();
            }
        }
        private BitmapImage _Tile12Source { get; set; } = new BitmapImage();

        public BitmapImage Tile13Source
        {
            get => _Tile13Source;
            set
            {
                if (value == _Tile13Source) return;
                _Tile13Source = value;
                OnPropertyChanged();
            }
        }
        private BitmapImage _Tile13Source { get; set; } = new BitmapImage();

        public BitmapImage Tile20Source
        {
            get => _Tile20Source;
            set
            {
                if (value == _Tile20Source) return;
                _Tile20Source = value;
                OnPropertyChanged();
            }
        }
        private BitmapImage _Tile20Source { get; set; } = new BitmapImage();

        public BitmapImage Tile21Source
        {
            get => _Tile21Source;
            set
            {
                if (value == _Tile21Source) return;
                _Tile21Source = value;
                OnPropertyChanged();
            }
        }
        private BitmapImage _Tile21Source { get; set; } = new BitmapImage();

        public BitmapImage Tile22Source
        {
            get => _Tile22Source;
            set
            {
                if (value == _Tile22Source) return;
                _Tile22Source = value;
                OnPropertyChanged();
            }
        }
        private BitmapImage _Tile22Source { get; set; } = new BitmapImage();

        public BitmapImage Tile23Source
        {
            get => _Tile23Source;
            set
            {
                if (value == _Tile23Source) return;
                _Tile23Source = value;
                OnPropertyChanged();
            }
        }
        private BitmapImage _Tile23Source { get; set; } = new BitmapImage();

        public BitmapImage Tile30Source
        {
            get => _Tile30Source;
            set
            {
                if (value == _Tile30Source) return;
                _Tile30Source = value;
                OnPropertyChanged();
            }
        }
        private BitmapImage _Tile30Source { get; set; } = new BitmapImage();

        public BitmapImage Tile31Source
        {
            get => _Tile31Source;
            set
            {
                if (value == _Tile31Source) return;
                _Tile31Source = value;
                OnPropertyChanged();
            }
        }
        private BitmapImage _Tile31Source { get; set; } = new BitmapImage();

        public BitmapImage Tile32Source
        {
            get => _Tile32Source;
            set
            {
                if (value == _Tile32Source) return;
                _Tile32Source = value;
                OnPropertyChanged();
            }
        }
        private BitmapImage _Tile32Source { get; set; } = new BitmapImage();

        public BitmapImage Tile33Source
        {
            get => _Tile33Source;
            set
            {
                if (value == _Tile33Source) return;
                _Tile33Source = value;
                OnPropertyChanged();
            }
        }
        private BitmapImage _Tile33Source { get; set; } = new BitmapImage();


        //public ObservableCollection<BitmapImage> Sources = new ObservableCollection<BitmapImage>();

        #endregion
     
        #region clickHandler

        //This is a replacement for Cursor.Position in WinForms
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        //This simulates a left mouse click
        public static void LeftMouseClick(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
        }

        public static void LeftMouseDown(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
        }

        public static void LeftMouseUp(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
        }



        #endregion



        public MainWindow()
        {

            InitializeComponent();

            //Refactor residue
            //AddWordTextBox.KeyDown += new System.Windows.Input.KeyEventHandler(OnKeyDownHandler);
            //ImageControls = new System.Windows.Controls.Image[]
            //{
            //    Tile00, Tile01, Tile02, Tile03,
            //    Tile10, Tile11, Tile12, Tile13,
            //    Tile20, Tile21, Tile22, Tile23,
            //    Tile30, Tile31, Tile32, Tile33
            //};

            foreach (string screenshot in Directory.GetFiles(DEBUG_PATH))
                DebugFile_ComboBox.Items.Add(screenshot);

            DebugFile_ComboBox.SelectedIndex = 0;

        }

        public static string IndexString(List<LetterTile> wordAsTiles)
        {
            string indexString = string.Empty;

            foreach(LetterTile letterTile in wordAsTiles)
            {
                indexString += $"{letterTile.index}-";
            }
            return indexString;
        }


        public bool IsNotPlaying
        {
            get => _IsNotPlaying;
            set
            {
                if (_IsNotPlaying == value) return;
                _IsNotPlaying = value;
                OnPropertyChanged();
                OnPropertyChanged("IsPlaying");
            }
        }

        private bool _IsNotPlaying { get; set; } = true;

        public bool IsPlaying => !IsNotPlaying;

        private object PathLock = new object();
        private void AddExploredPath(string path)
        {
            lock(PathLock)
                if (!exploredPaths.Contains(path))
                {
                    exploredPaths.Add(path);
                    Logger.log($"New explored path ({path})");
                }
        }
        private bool CheckExploredPath(string path)
        {
            lock (PathLock)
                return exploredPaths.Contains(path);
        }

        private object FoundWordLock = new object();

        private void AddFoundWordIfNew(string word, List<LetterTile> wordAsTiles, string wordPath)
        {
            lock (FoundWordLock)
            {
                lock(WordsToPlay)
                {
                    if (FoundWords.Contains(word) || WordsToPlay.Contains(wordAsTiles))
                    {
                        Logger.log($"'{word}' was already found");
                        return;
                    }

                    Logger.log($"Word Found!: '{word}' ({IndexString(wordAsTiles)}) ");
                    FoundWords.Add(word);
                    WordsToPlay.Add(wordAsTiles);

                    //TODO: Rework explored Paths

                    if (Directory.GetDirectories(wordPath).Length == 0)
                        AddExploredPath(IndexString(wordAsTiles));
                }
            }
        }


        private int _NumberOfWordsPlayed { get; set; } = 0;

        public int NumberOfWordsPlayed
        {
            get => _NumberOfWordsPlayed;
            set
            {
                if (_NumberOfWordsPlayed == value)
                    return;

                _NumberOfWordsPlayed = value;
                OnPropertyChanged();
            }
        }

        private void AddFoundWord(string word)
        {
            lock (FoundWordLock)
                if (FoundWords.Contains(word))
                    return;

            FoundWords.Add(word);
            NumberOfWordsPlayed++;
        }

        private bool CheckFoundWord(string word)
        {
            lock (FoundWordLock)
                return FoundWords.Contains(word);
        }

        private object WordsToPlayLock = new object();

        private void AddWordToPlay(List<LetterTile> word)
        {
            lock (WordsToPlayLock)
                if (!WordsToPlay.Contains(word))
                    WordsToPlay.Add(word);
        }



        public void PlayWordsAsync()
        {

            bool firstWordFound = false;
            int wordPlayed = 0;
            int timeoutSeconds = MaxTime;

            var timer = System.Diagnostics.Stopwatch.StartNew();
            var UnlockedFor = System.Diagnostics.Stopwatch.StartNew();
            //var timeSinceExecution = System.Diagnostics.Stopwatch.StartNew();

            double elapsedSeconds = 0;
            long targetWaitTime = 1600;
            long WaitTime = 200;

            List<List<LetterTile>> localWordsToPlay = new List<List<LetterTile>>();


            while (elapsedSeconds <= MaxTime && !IsNotPlaying)
            {

                elapsedSeconds = timer.Elapsed.TotalSeconds;
                if (timer.Elapsed > TimeSpan.FromSeconds(timeoutSeconds))
                {
                    Logger.log($"Word player timed out ({timer.Elapsed.TotalSeconds} seconds)");
                    break;
                }

                if (!firstWordFound)
                {
                    Thread.Sleep(50);
                }
                else if (UnlockedFor.ElapsedMilliseconds < WaitTime)
                {
                    Thread.Sleep((int)(WaitTime - UnlockedFor.ElapsedMilliseconds));
                }

                lock(WordsToPlayLock)
                {
                    if (WordsToPlay.Count > 0)
                    {
                        localWordsToPlay = WordsToPlay.GetRange(0, WordsToPlay.Count);
                        WordsToPlay.Clear();
                    }
                }

                UnlockedFor.Restart();

                if (localWordsToPlay.Count > 0)
                {
                    if (WaitTime < targetWaitTime)
                        WaitTime += 200;

                    firstWordFound = true;
                    IsPlayingWords = true;

                    foreach (List<LetterTile> word in localWordsToPlay)
                    {
                        if (timer.Elapsed > TimeSpan.FromSeconds(timeoutSeconds))
                        {
                            Logger.log($"Word player timed out ({timer.Elapsed.TotalSeconds} seconds)");
                            break;
                        }
                        PlayWord(word);
                        wordPlayed++;
                    }

                    IsPlayingWords = false;
                    
                    localWordsToPlay.Clear();
                }


            }

            timer.Stop();
            Logger.log($"Word player stopped. (Played {wordPlayed} words in {timer.Elapsed.TotalSeconds} seconds)");
        }


        private void Run_Button_Click(object sender, RoutedEventArgs e)
        {
            if(!calibration.CalibrationDone)
            {
                System.Windows.MessageBox.Show("Calibration pending","No calibration",MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            debug = (bool)Debug_CheckBox.IsChecked;
            debugFilePath = DebugFile_ComboBox.Text;

            fairnessLevel = FairnessSlider.Value;

            AddedWordHistoryTextBox.Text = string.Empty;

            Logger.NewLog();
            WordsToPlay.Clear();
            FoundWords.Clear();
            exploredPaths.Clear();

            
            IsNotPlaying = false;

            new Thread(PlayWordsAsync).Start();
            new Thread(Main).Start();

            Gametimer.Restart();

        }

        private void Main()
        {
            WinIt();


            var lastExecutionEllapsed = Gametimer.ElapsedMilliseconds;
            var lastExecutionWordsFound = FoundWords.Count;

            if (!debug)
                while (/*(MaxTime - Gametimer.ElapsedMilliseconds) > lastExecutionEllapsed*0.80 && */lastExecutionWordsFound > 0 && false)
                {
                    if (IsPlayingWords) continue;

                    if (Gametimer.ElapsedMilliseconds > MaxTime) break;

                    var Ellapsed = Gametimer.ElapsedMilliseconds;
                    var WordCount = FoundWords.Count;

                    LeftMouseClick(950, 968); //Rotate board
                    Thread.Sleep(1000);
                    WinIt();

                    lastExecutionWordsFound = FoundWords.Count - WordCount;
                    lastExecutionEllapsed = Gametimer.ElapsedMilliseconds - Ellapsed;
                }


            Gametimer.Stop();
            IsNotPlaying = true;
        }

        private void WinIt()
        {
            Logger.log("Starting winit");

            GuessesTextBoxContent = "";


            CaptureDirectory = TRASH_PATH + @"\" + Path.GetRandomFileName() + "\\";
            Directory.CreateDirectory(CaptureDirectory);


            //Get ScreenShot or debug file
            Bitmap fullImage;

            if (debug)
                fullImage = (Bitmap)Image.FromFile(debugFilePath);
            else
                fullImage = ScreenCapture(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);


            //Split screenshot into tile segments
            LetterTile[][] AllTiles = getTiles(fullImage, 40, 4, -3, -3, 0, 0);

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    AllTiles[i][j].SurroundingTiles = GetSurroundingTiles(AllTiles, (i, j));

            List<List<LetterTile>> words = new List<List<LetterTile>>();


            FindWord(AllTiles);

         
        }

        public Bitmap ScreenCapture(int x, int y, int width, int height)
        {
            Rectangle rect = new Rectangle(x, y, width, height);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
            String filename = getUniqueName( CaptureDirectory + "MainImage", ".jpeg");

            bmp.Save(filename, ImageFormat.Jpeg);

            return (Bitmap)Image.FromFile(CaptureDirectory + "MainImage.jpeg");
            
        }

        public LetterTile[][] getTiles(Bitmap FullImage, int tileWidth, int tilePerRow, int tileSpacingX, int tileSpacingY, int paddingX, int paddingY)
        {
            LetterTile[][] tiles =
            {
                new LetterTile[4],
                new LetterTile[4],
                new LetterTile[4],
                new LetterTile[4]
            };

            Logger.log("Parsing letters...");
            for(int y = 0; y < 4; y++)
            {
                string row = string.Empty;
                string rowIndexes = string.Empty;

                for (int x = 0; x < 4; x++)
                {

                    int realWidth = tileWidth; 
                    int realHeight = tileWidth; 

                    Rectangle segRect = new Rectangle((int)calibration.LetterBoxesCoord[(y*4)+x].X, (int)calibration.LetterBoxesCoord[(y * 4) + x].Y, (int)(calibration.BoxWidth*1.25), (int)(calibration.BoxWidth * 1.25));

                    string segFileName = getUniqueName(CaptureDirectory + $"segment{x}-{y}", ".jpeg");
                    string rawSegFileName = getUniqueName(CaptureDirectory + $"RAWsegment{x}-{y}", ".jpeg");


                    Bitmap rawSegment = FullImage.Clone(segRect, FullImage.PixelFormat);

                    Bitmap segment = boxLetter(rawSegment, 10);

                    char letter;

                    try
                    {
                        letter = parseLetter(segment)[0];
                    }
                    catch
                    {
                        letter = '_';
                    }

                    row += letter + "  ";
                    int IndexNbrChar = (4 * y + x).ToString().ToCharArray().Length;
                    rowIndexes += (4 * y + x).ToString() + (IndexNbrChar == 1? "  " : " ");

                    tiles[y][x] = new LetterTile(letter, ((int)calibration.LetterBoxesCoord[(y * 4) + x].X + (int)(calibration.BoxWidth * 0.625), (int)calibration.LetterBoxesCoord[(y * 4) + x].Y + (int)(calibration.BoxWidth * 0.625)), (y, x) );
                    segment.Save(segFileName);
                    rawSegment.Save(rawSegFileName);

                    GuessesTextBoxContent += tiles[y][x].Letter + " ";
                    if (x == 3 && y != 3) GuessesTextBoxContent += "\n";

                    UpdateImageSource(x, y, segFileName);
                    //Sources[y * 4 + x] =  new BitmapImage(new Uri(segFileName));
                }
                Logger.log(row + "  " + rowIndexes);
            }

            return tiles;
        }

        public void UpdateImageSource(int x, int y, string _source)
        {
            if (y == 0 && x == 0)
            {
                Tile00Source = new BitmapImage(new Uri(_source));
                return;
            }
            if (y == 0 && x == 1)
            {
                Tile01Source = new BitmapImage(new Uri(_source));
                return;
            }
            if (y == 0 && x == 2)
            {
                Tile02Source = new BitmapImage(new Uri(_source));
                return;
            }
            if (y == 0 && x == 3)
            {
                Tile03Source = new BitmapImage(new Uri(_source));
                return;
            }
            //
            if (y == 1 && x == 0)
            {
                Tile10Source = new BitmapImage(new Uri(_source));
                return;
            }
            if (y == 1 && x == 1)
            {
                Tile11Source = new BitmapImage(new Uri(_source));
                return;
            }
            if (y == 1 && x == 2)
            {
                Tile12Source = new BitmapImage(new Uri(_source));
                return;
            }
            if (y == 1 && x == 3)
            {
                Tile13Source = new BitmapImage(new Uri(_source));
                return;
            }
            //
            if (y == 2 && x == 0)
            {
                Tile20Source = new BitmapImage(new Uri(_source));
                return;
            }
            if (y == 2 && x == 1)
            {
                Tile21Source = new BitmapImage(new Uri(_source));
                return;
            }
            if (y == 2 && x == 2)
            {
                Tile22Source = new BitmapImage(new Uri(_source));
                return;
            }
            if (y == 2 && x == 3)
            {
                Tile23Source = new BitmapImage(new Uri(_source));
                return;
            }
            //
            if (y == 3 && x == 0)
            {
                Tile30Source = new BitmapImage(new Uri(_source));
                return;
            }
            if (y == 3 && x == 1)
            {
                Tile31Source = new BitmapImage(new Uri(_source));
                return;
            }
            if (y == 3 && x == 2)
            {
                Tile32Source = new BitmapImage(new Uri(_source));
                return;
            }
            if (y == 3 && x == 3)
            {
                Tile33Source = new BitmapImage(new Uri(_source));
                return;
            }

        }

        public Bitmap boxLetter(Bitmap tile, int TargetWidth)
        {

            int HighestPix = tile.Height;
            int LowestPix = 0;

            int LeftestPix = tile.Width;
            int RighestPix = 0;


            for (int xPix = 0; xPix < tile.Width; xPix++)
            {
                for (int yPix = 0; yPix < tile.Height; yPix++)
                {
                    if (tile.GetPixel(xPix, yPix).GetBrightness() < 0.3)
                    {
                        tile.SetPixel(xPix, yPix, Color.Black);
                        if (xPix < LeftestPix) LeftestPix = xPix;
                        if (xPix > RighestPix) RighestPix = xPix;
                        if (yPix > LowestPix) LowestPix = yPix;
                        if (yPix < HighestPix) HighestPix = yPix;
                    }
                    else
                    {
                        tile.SetPixel(xPix, yPix, Color.White); 
                    }
                }
            }



            Rectangle segRect = new Rectangle(LeftestPix, HighestPix, RighestPix - LeftestPix, LowestPix - HighestPix);
            Bitmap croppedBitmap = tile.Clone(segRect, tile.PixelFormat);


            return PadToDesiredFormat(croppedBitmap, 50, 50);
        }

        public Bitmap PadToDesiredFormat(Bitmap bitmap, int desiredWidth, int desiredHeight)
        {
            if (bitmap.Width > desiredWidth || bitmap.Height > desiredWidth)
                throw new Exception("Cannot pad because already too big");

            if (bitmap.Width == desiredWidth && bitmap.Height == desiredHeight) return bitmap;

            int horizontalPadding = (desiredWidth - bitmap.Width)/2;
            int verticalPadding = (desiredHeight - bitmap.Height)/2;

            System.Drawing.Image hPadding = new Bitmap(horizontalPadding, bitmap.Height, PixelFormat.Format24bppRgb);
            System.Drawing.Image vPadding = new Bitmap(desiredWidth, verticalPadding, PixelFormat.Format24bppRgb);

            using (Graphics grp = Graphics.FromImage(hPadding))            
                grp.FillRectangle(Brushes.White, 0, 0,hPadding.Width, hPadding.Height);

            using (Graphics grp = Graphics.FromImage(vPadding))
                grp.FillRectangle(Brushes.White, 0, 0, vPadding.Width, vPadding.Height);

            Bitmap paddedBitMap = new Bitmap(bitmap.Width + hPadding.Width*2, bitmap.Height + vPadding.Height*2);

            using (Graphics g = Graphics.FromImage(paddedBitMap))
            {
                g.DrawImage(vPadding, 0, 0);
                g.DrawImage(hPadding, 0, vPadding.Height);
                g.DrawImage(bitmap, hPadding.Width, vPadding.Height);
                g.DrawImage(hPadding, hPadding.Width + bitmap.Width, vPadding.Height);
                g.DrawImage(vPadding, 0, bitmap.Height + vPadding.Height);
            }


            return paddedBitMap;
        }

        public string parseLetter(Bitmap tile)
        {
            
            int nbrOfPxl = tile.Height * tile.Width;
            string bestTile = null;
            (string letter, float score) BestMatch = (null, 0);

            string[] Letters = Directory.GetDirectories(REFS_PATH);

            foreach (string Letter in Letters)
            {
                foreach (string referenceTile in Directory.GetFiles(Letter))
                {
                    float similarity = 0;

                    Bitmap refBitMap = boxLetter((Bitmap)System.Drawing.Image.FromFile(referenceTile), 50);

                    int minWidth = Math.Min(refBitMap.Width, tile.Width);
                    int minHeight = Math.Min(refBitMap.Height, tile.Height);

                    for (int xPix = 0; xPix < minWidth; xPix++)
                    {
                        for (int yPix = 0; yPix < minHeight; yPix++)
                        {
                            if (tile.GetPixel(xPix, yPix).GetBrightness() == refBitMap.GetPixel(xPix, yPix).GetBrightness())
                                similarity += 1;
                            else
                                similarity -= 1;
                        }
                    }

                    if (similarity / nbrOfPxl <  (new List<string>() { "R", "O", "Q" }.Contains(Letter) ? 0.95 : 0.80)) break; //Switch Letter

                    if (similarity > BestMatch.score)
                    {
                        BestMatch = (new DirectoryInfo(Letter).Name, similarity);
                        bestTile = referenceTile;
                    }
                }

                if (BestMatch.score / nbrOfPxl > 0.98) //Stop Looking
                {
                    break;
                }
            }


            string similarityStr = (BestMatch.score*100/nbrOfPxl).ToString("n2");

            if (BestMatch.letter is null)
            {
                if(debug)
                    System.Windows.MessageBox.Show($"Couldn't find a letter");

                return null;
            }

            return BestMatch.letter;
        }

        public string getUniqueName(string filename, string extension)
        {
            if (Path.HasExtension(filename))
                filename.Replace(extension, "");

            string tempFileName = filename;

            int index = 1;
            while (File.Exists(tempFileName))
                tempFileName = filename + $"({index})"; index++;

           return filename += extension;
        }


        public void PlayWord(List<LetterTile> word)
        {
            if (word.Count == 0) return;


            string wordAsString = string.Empty;
            wordAsString += word[0].Letter;

            LeftMouseDown(word[0].MiddlePoint.x, word[0].MiddlePoint.y);
            Thread.Sleep(DelayBetweenLetters);

            foreach (LetterTile letter in word)
            {
                if (letter.index == word[0].index)
                    continue;

                wordAsString += letter.Letter; 
                SetCursorPos(letter.MiddlePoint.x, letter.MiddlePoint.y);
                Thread.Sleep(DelayBetweenLetters);
            }

            LeftMouseUp(word[word.Count - 1].MiddlePoint.x, word[word.Count - 1].MiddlePoint.y);
            Logger.log($"Played '{wordAsString}'");


            Thread.Sleep(DelayBetweenWords);

        }

        public List<LetterTile> GetSurroundingTiles(LetterTile[][] board, (int x, int y) index)
        {
            List<LetterTile> surroundingTiles = new List<LetterTile>();


            /*          (X,Y) => (1,1)
             *          
             *          (0,0) (0,1) (0,2) (0,3)
             *          (1,0) (1,1) (1,2) (1,3)  
             *          (2,0) (2,1) (2,2) (2,3)
             *          (3,0) (3,1) (3,2) (3,3)
             * */

            int boardWidth = board[0].Length - 1;

            if (index.x > 0)
                surroundingTiles.Add(board[index.x - 1][index.y]); //(0,1) Haut

            if (index.y > 0)
                surroundingTiles.Add(board[index.x][index.y - 1]); //(1,0) Gauche

            if (index.x < boardWidth)
                surroundingTiles.Add(board[index.x + 1][index.y]); //(2,1) Bas

            if (index.y < boardWidth)
                surroundingTiles.Add(board[index.x][index.y + 1]); //(1,2) Droite

            if (index.x > 0 && index.y > 0)
                surroundingTiles.Add(board[index.x - 1][index.y - 1]); //(0,0) Haut-Gauche

            if (index.x < boardWidth && index.y < boardWidth)
                surroundingTiles.Add(board[index.x + 1][index.y + 1]); //(2,2) Bas-Droite

            if (index.x < boardWidth && index.y > 0)
                surroundingTiles.Add(board[index.x + 1][index.y - 1]); //(2,0)  Bas-Gauche
             
            if (index.x > 0 && index.y < boardWidth)
                surroundingTiles.Add(board[index.x - 1][index.y + 1]); //(0,2) Haut-Droite

            return surroundingTiles;
        }

        public List<LetterTile> findNextLetter(string wordPath, List<LetterTile> surroundingLetters, List<LetterTile> wordAsTiles)
        {

            //Returns a letter that is both in the dictionnary and in the surrounding tiles

            string[] lettersInDir = Directory.GetDirectories(wordPath);

            if (lettersInDir.Length == 0)
            {
                AddExploredPath(IndexString(wordAsTiles));
                return null;
            }

            List<LetterTile> nextLetters = new List<LetterTile>();
             
            foreach (string letterDir in lettersInDir)
            {
                char letter = Path.GetFileName(letterDir)[0];
                //if (exploredPaths.Contains(wordPath + $"\\{letter.ToString().ToUpper()}")) continue;

                try
                {
                    var surr = surroundingLetters.FindAll(x => x.Letter == letter && !wordAsTiles.Contains(x));
                    if (surr != null && surr.Count > 0)
                    {
                        foreach (LetterTile ltr in surr)
                            if (!CheckExploredPath(IndexString(wordAsTiles) + ltr.index + "-"))
                                nextLetters.Add(ltr);
                            else
                                Logger.log($"Skipped {wordAsTiles} because path was explored");

                            
                    }
                }
                catch
                {
                    Logger.log($"ERROR while findind next letter: {wordPath}");
                    //ignore
                }
            }

            if (nextLetters.Count == 0) return null;
            return nextLetters;
        }

        public List<LetterTile> FindWordRecurse(LetterTile[][] board, LetterTile currentTile ,List<LetterTile> wordAsTiles = null, string wordPath= null)
        {

            Logger.log($"Exploring path: {wordPath.Replace(WordDictionnary.organizedDictBaseFolder, "").Replace("\\", "")} ({IndexString(wordAsTiles)})");

            if(Gametimer.ElapsedMilliseconds > MaxTime)
            {
                Logger.log($"Stopped FindWordRecurse because max game time reached ({MaxTime/1000}s)");
                return null;
            }

            //If first iteration
            if (wordAsTiles is null)
            {
                wordAsTiles = new List<LetterTile>() { currentTile };
                wordPath = WordDictionnary.Dictionnary + $"\\{currentTile.Letter}";
            }


            //Check if valid word
            string potentialWord = null;
            if(wordAsTiles.Count > 1)
            try
            {
               potentialWord = Path.GetFileNameWithoutExtension(Directory.GetFiles(wordPath, "*.word", SearchOption.TopDirectoryOnly)[0]);
                    if (randomizer.NextDouble() * 100 >= fairnessLevel) //Fairness adjust
                        AddFoundWordIfNew(potentialWord, wordAsTiles, wordPath);
            }
            catch(Exception e)
            {
                Logger.log($"Not a word: {wordPath.Replace(WordDictionnary.organizedDictBaseFolder, "").Replace("\\","")} ({IndexString(wordAsTiles)})");
            }

            if (currentTile.SurroundingTiles == null)
                currentTile.SurroundingTiles = GetSurroundingTiles(board, findBoardIndex(board, currentTile));

            List<LetterTile> nextLetters = findNextLetter(wordPath, currentTile.SurroundingTiles, wordAsTiles);

            //TODO: Rework that
            if (nextLetters is null || nextLetters.Count == 0)
            {
                AddExploredPath(IndexString(wordAsTiles));
                return null;
            }

            List<LetterTile> OGWordAsTiles = wordAsTiles;
            foreach (LetterTile nextltr in nextLetters)
            {
                List<LetterTile> temp = new List<LetterTile>();
                temp.AddRange(wordAsTiles);

                //foreach(LetterTile ltr in wordAsTiles)
                //    temp.Add(ltr);

                temp.Add(nextltr);

                if (!CheckExploredPath(IndexString(temp)))
                    FindWordRecurse(board, nextltr, temp, wordPath + $"\\{nextltr.Letter}");
            }

            return wordAsTiles;

        }

        public void FindWordRecurseFromStartingLetters(LetterTile[][] board, LetterTile[] startingTiles)
        {
            string basePath = WordDictionnary.Dictionnary;
            string wordPath = basePath;

            foreach(LetterTile letter in startingTiles)
            {
                if (Gametimer.ElapsedMilliseconds < MaxTime)
                    try
                    {
                        wordPath = basePath + $"\\{letter.Letter}";
                        FindWordRecurse(board, letter, new List<LetterTile>() { letter }, wordPath);
                    }
                    catch (Exception e)
                    {
                        Logger.log($"Error on finding new word: {wordPath}");
                    }
                else
                    Logger.log($"Stopped word search beacause max game time was reached ({MaxTime / 1000}s)");
            }
            
        }

        public void FindWord(LetterTile[][] board)
        {

            WordsToPlay.Clear();
            exploredPaths.Clear();

            if(!debug)
            {
                var thread1 = new Thread(() => FindWordRecurseFromStartingLetters(board, board[0]));
                var thread2 = new Thread(() => FindWordRecurseFromStartingLetters(board, board[1]));
                var thread3 = new Thread(() => FindWordRecurseFromStartingLetters(board, board[2]));
                var thread4 = new Thread(() => FindWordRecurseFromStartingLetters(board, board[3]));

                thread1.Start();
                thread2.Start();
                thread3.Start();
                thread4.Start();

                thread1.Join();
                thread2.Join();
                thread3.Join();
                thread4.Join();
            }


            if(debug)
                System.Windows.MessageBox.Show($"{FoundWords.Count} words found\n{WordsToPlay.Count} words to play");

            Logger.log($"{WordsToPlay.Count} words to play found");

            Logger.log($"Played {WordsToPlay.Count}/{FoundWords.Count}");
        }
        

        public (int, int) findBoardIndex(LetterTile[][] board, LetterTile tile)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (board[i][j].MiddlePoint.x == tile.MiddlePoint.x && board[i][j].MiddlePoint.y == tile.MiddlePoint.y)
                        return (i, j);

            return (-1, -1);
        }

        private void AddWordTextBox_Click(Object _sender, RoutedEventArgs _e)
        {
            if (String.IsNullOrEmpty(AddWordTextBox.Text)) return;

            if(WordDictionnary.AddWord(AddWordTextBox.Text))
            {
                AddedWordHistoryTextBox.Text = $"+ {AddWordTextBox.Text.ToUpper()}\n" + AddedWordHistoryTextBox.Text;
            }
            AddWordTextBox.Text = "";
            AddWordTextBox.Focus();
        }

        private void RemoveWordButton_Click(object sender, RoutedEventArgs e)
        {
            if (WordDictionnary.RemoveWord(AddWordTextBox.Text))
            {
                AddedWordHistoryTextBox.Text = $"- {AddWordTextBox.Text.ToUpper()}\n" + AddedWordHistoryTextBox.Text;
            }
            AddWordTextBox.Text = "";
            AddWordTextBox.Focus();
        }

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            if(WordDictionnary.Undo())            
                AddedWordHistoryTextBox.Text = $"{(WordDictionnary.isLastActionAdd ? "+" : "-")} {WordDictionnary.LastWord}\n" + AddedWordHistoryTextBox.Text;

            AddWordTextBox.Text = "";
            AddWordTextBox.Focus();
        }

        private void OnKeyDownHandler(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                AddWordTextBox_Click(this, new RoutedEventArgs());
            }
        }


        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] String _propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));


        #endregion


        private void CalibrationButton_Click(object sender, RoutedEventArgs e)
        {
            Calibrate();   
        }

        private void Calibrate()
        {
            calibration.LauchWIndow();
        }

        private void Debug_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            DebugFile_ComboBox.Visibility = Visibility.Visible;
        }

        private void Debug_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            DebugFile_ComboBox.Visibility = Visibility.Hidden;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }





    public struct LetterTile
    {
        public char Letter;
        public (int x, int y) MiddlePoint;
        public bool isUsed;
        public List<LetterTile> SurroundingTiles { get; set; }
        public (int x, int y) BoardIndex { get; set; }
        public int index { get; }

        public LetterTile(char letter, (int x, int y) middlePoint, (int x, int y) boardIndex, bool _isUsed = false)
        {
            BoardIndex = boardIndex;
            isUsed = _isUsed;
            Letter = letter;
            MiddlePoint = middlePoint;
            SurroundingTiles = null;
            index = 4 * boardIndex.x + boardIndex.y;
        }

        public override bool Equals(object obj)
        {

            if(obj is LetterTile)
            {
               LetterTile letter = (LetterTile)obj;
               return Letter == letter.Letter && BoardIndex.x == letter.BoardIndex.x && BoardIndex.y == letter.BoardIndex.y;
            }
            else
            {
               return false;
            }
        }

        public override int GetHashCode()
        {
            var hashCode = 1457246626;
            hashCode = hashCode * -1521134295 + Letter.GetHashCode();
            hashCode = hashCode * -1521134295 + MiddlePoint.GetHashCode();
            hashCode = hashCode * -1521134295 + isUsed.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<LetterTile>>.Default.GetHashCode(SurroundingTiles);
            hashCode = hashCode * -1521134295 + BoardIndex.GetHashCode();
            return hashCode;
        }
    }
}

