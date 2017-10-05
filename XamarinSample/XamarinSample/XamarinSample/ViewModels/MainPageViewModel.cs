using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinSample.Annotations;

namespace XamarinSample.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string _barcodeText;
        private ICommand _scanCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public string BarcodeText
        {
            get { return _barcodeText; }
            set
            {
                _barcodeText = value;
                OnPropertyChanged();
            }
        }

        public ICommand ScanCommand
        {
            get
            {
                return _scanCommand = _scanCommand ?? new Command(async () =>
                {
                    MessagingCenter.Send("BarcodeInitialize", "BarcodeInitialize");

                    var scanner = new ZXing.Mobile.MobileBarcodeScanner();
                    var result = await scanner.Scan();

                    BarcodeText = result.Text;
                });
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
