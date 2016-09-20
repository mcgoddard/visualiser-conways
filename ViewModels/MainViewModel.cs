using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using visualiser_conways.Helpers;

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
        #endregion

        #region Private members
        private string outputDir;
        private uint delayTime;
        private bool playing = false;
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
            return false;
        }

        private void Play(object parameter)
        {
            throw new NotImplementedException();
        }

        private bool CanStop(object parameter)
        {
            return false;
        }

        private void Stop(object parameter)
        {
            throw new NotImplementedException();
        }

        private bool CanSelectFolder(object parameter)
        {
            return true;
        }

        private void SelectFolder(object parameter)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
