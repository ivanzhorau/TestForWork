using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using System.Threading;
using TestOnWork.Model;

namespace TestOnWork.ViewModel
{
    class WindowViewModel : INotifyPropertyChanged
    {
        private Lot selectedLot;

        private ObservableCollection<Lot> lots;
        public ObservableCollection<Lot> Lots { 
            get { return lots; }
            set {
                lots = value;
                OnPropertyChanged("Lots");
            }
        }


        public Lot SelectedLot {
            get { return selectedLot; }
            set {
                selectedLot = value;
                OnPropertyChanged("SelectedLot");
            }
        }
        private RelayCommand addLot;
        public RelayCommand AddLot
        {
            get
            {
                return addLot ??
                  (addLot = new RelayCommand(obj =>
                  {
                      Lot lot = new Lot();
                      ApiConnector.doRequest(lot,"POST");
                      Thread.Sleep(1000);
                      ApiConnector.getList();
                      SelectedLot = lot;
                  }));
            }
        }
        private RelayCommand delLot;
        public RelayCommand DelLot
        {
            get
            {
                return delLot ??
                  (delLot = new RelayCommand(obj =>
                  {
                      ApiConnector.doRequest(SelectedLot, "DELETE");
                      Thread.Sleep(1000);
                      ApiConnector.getList();
                      SelectedLot = Lots[0];
                  }));
            }
        }
        private RelayCommand changeLot;
        public RelayCommand ChangeLot
        {
            get
            {
                return changeLot ??
                  (changeLot = new RelayCommand(obj =>
                  {
                      ApiConnector.doRequest(SelectedLot, "PUT");
                      Thread.Sleep(1000);
                      ApiConnector.getList();
                  }));
            }
        }
    
        public WindowViewModel()
        {
            Lot[] lotArr = ApiConnector.getList();
            Lots = new ObservableCollection<Lot>(lotArr);

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
