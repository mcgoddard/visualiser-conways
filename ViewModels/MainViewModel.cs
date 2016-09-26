using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using visualiser_conways.Helpers;
using Timer = System.Timers.Timer;

namespace visualiser_conways.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        #region Public members
        public string OutputDir
        {
            get
            {
                return outputDir;
            }
            set
            {
                if (outputDir != value)
                {
                    outputDir = value;
                    OnPropertyChanged("OutputDir");
                }
            }
        }

        public string DelayTime
        {
            get
            {
                return delayTime.ToString();
            }
            set
            {
                uint tmpDelay;
                if (uint.TryParse(value, out tmpDelay) && tmpDelay != delayTime)
                {
                    delayTime = tmpDelay;
                    OnPropertyChanged("DelayTime");
                }
            }
        }

        public ICommand PlayCommand
        {
            get;
            private set;
        }

        public ICommand StopCommand
        {
            get;
            private set;
        }

        public ICommand SelectFolderCommand
        {
            get;
            private set;
        }

        public bool CanChangeDelay
        {
            get
            {
                return !playing;
            }
        }

        public List<List<bool>> States
        {
            get
            {
                if (states == null || states.Any(s => s == null))
                {
                    return null;
                }
                var bl = states.Select(r => r.Select(c => c).ToList()).ToList();
                return bl;
            }
        }
        #endregion

        #region Private members
        private string outputDir;
        private uint delayTime = 500;
        private bool playing = false;
        private Timer playTimer;
        private int iterNum = 0;
        private bool[][] states;
        private bool rendering = false;
        #endregion

        #region Constructors
        public MainViewModel()
        {
            PlayCommand = new RelayCommand(CanPlay, Play);
            StopCommand = new RelayCommand(CanStop, Stop);
            SelectFolderCommand = new RelayCommand(CanSelectFolder, SelectFolder);
        }
        #endregion

        #region Private methods
        private bool CanPlay (object parameter)
        {
            return !playing && !String.IsNullOrEmpty(outputDir) && delayTime > 0;
        }

        private void Play(object parameter)
        {
            iterNum = 0;
            playing = true;
            playTimer = new Timer(delayTime);
            playTimer.Elapsed += PlayTimer_Elapsed;
            playTimer.AutoReset = true;
            playTimer.Start();
        }

        private void PlayTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (playing)
            {
                if (!rendering)
                {
                    rendering = true;
                    // Load a frame and display
                    var filePath = Path.Combine(outputDir, String.Format("{0}.csv", iterNum));
                    if (File.Exists(filePath))
                    {
                        var file = File.ReadLines(filePath);
                        var states = new List<bool[]>();
                        foreach (string line in file)
                        {
                            var characters = line.Split(',');
                            states.Add(characters.Select(c => Convert.ToBoolean(int.Parse(c))).ToArray());
                        }
                        this.states = states.ToArray();
                        OnPropertyChanged("States");
                        // Increment the iteration number so we load a new frame next time
                        iterNum++;
                    }
                    else
                    {
                        playing = false;
                        MessageBox.Show("End of simulation", String.Format("The simulation ends at interation {0}", iterNum), MessageBoxButtons.OK);
                    }
                    rendering = false;
                }
                else
                {
                    Console.WriteLine("Rendering time is currently longer than delay");
                }
            }
        }

        private bool CanStop(object parameter)
        {
            return playing;
        }

        private void Stop(object parameter)
        {
            playing = false;
        }

        private bool CanSelectFolder(object parameter)
        {
            return !playing;
        }

        private void SelectFolder(object parameter)
        {
            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                OutputDir = dialog.SelectedPath;
            }
        }
        #endregion
    }
}
