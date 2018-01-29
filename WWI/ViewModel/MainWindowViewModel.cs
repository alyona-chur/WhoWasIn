using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WWI.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WWI.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged, IDisposable
    {
        public VisitorsDBContext context = new VisitorsDBContext();

        #region Private Variables

        private ObservableCollection<EntryElement> _onDisplay;
        private ObservableCollection<User> _usersOndisplay;
        private DateTime _selectedDateTime;
        private User _selectedUser;
        private DateTime _curDateTime;
        private String _curDateTimeStr;

        //Commands
        private readonly ICommand _findByDateCommand;
        private readonly ICommand _findByUserCommand;
        private readonly ICommand _cancelFindCommand;

        #endregion

        #region Properties

        public ObservableCollection<EntryElement> OnDisplay
        {
            get
            {
                return _onDisplay;
            }
            set
            {
                _onDisplay = value;
                RaisePropertyChanged("OnDisplay");
            }
        }

        public ObservableCollection<User> UsersOnDisplay
        {
            get
            {
                return _usersOndisplay;
            }
            set
            {
                _usersOndisplay = value;
                RaisePropertyChanged("UsersOnDisplay");
            }
        }

        public DateTime SelectedDateTime
        {
            get
            {
                return _selectedDateTime;
            }
            set
            {
                _selectedDateTime = value;
                RaisePropertyChanged("SelectedDateTime");
            }

        }

        public User SelectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {
                _selectedUser = value;
                RaisePropertyChanged("SelectedUser");
            }
        }

        public DateTime CurDateTime
        {
            get
            {
                return _curDateTime;
            }
            set
            {
                _curDateTime = value;
            }
        }

        public String CurDateTimeStr
        {
            get
            {
                return _curDateTimeStr;
            }
            set
            {
                _curDateTimeStr = value;
                RaisePropertyChanged("CurDateTimeStr");
            }
        }

        //Commands
        public ICommand FindByDateCommand
        {
            get
            {
                return _findByDateCommand;
            }
        }

        public ICommand FindBuUserCommand
        {
            get
            {
                return _findByUserCommand;
            }
        }

        public ICommand CancelFindCommand
        {
            get
            {
                return _cancelFindCommand;
            }
        }

        #endregion

        #region Constructors

        public MainWindowViewModel()
        {
            //Data
            CurDateTime = DateTime.Now;
            CurDateTimeStr = CurDateTime.ToString("dd-MM-yyyy HH:mm tt");

            OnDisplay = new ObservableCollection<EntryElement>();
            ObservableCollection<Entry> Data = new ObservableCollection<Entry>(context.Entrys);
            for (int i = 0; i < Data.Count(); ++i)
            {
                EntryElement x = new EntryElement();
                x.EntryDate = Data[i].Entry_date;
                x.EntryDateStr = x.EntryDate.ToString("dd.MM.yyyy HH:mm tt");
                x.EntryID = Data[i].EntryID;
                x.UserID = Data[i].UserID;
                User y = (from z in context.Users where z.UserID == x.UserID select z).FirstOrDefault();
                x.UserName = y.UserName;
                OnDisplay.Add(x);
            }

            UsersOnDisplay = new ObservableCollection<User>(context.Users);

            //Commands
            _findByDateCommand = new RelayCommand(FindByDate, CanFindByDate);
            _findByUserCommand = new RelayCommand(FindByUser, CanFindByUser);
            _cancelFindCommand = new RelayCommand(CancelFind, CanCancelFind);
        }

        #endregion

        #region Commands

        #region FindByDateCommand

        public bool CanFindByDate(object obj)
        {
            if (SelectedDateTime == null)
                return false;
            return true;
        }

        public void FindByDate(object obj)
        {
            OnDisplay.Clear();
            ObservableCollection<Entry> Data = new ObservableCollection<Entry>(context.Entrys);
            for (int i = 0; i < Data.Count(); ++i)
            {
                if (Data[i].Entry_date.Date == SelectedDateTime.Date)
                {
                    EntryElement x = new EntryElement();
                    x.EntryDate = Data[i].Entry_date;
                    x.EntryDateStr = x.EntryDate.ToString("dd.MM.yyyy HH:mm tt");
                    x.EntryID = Data[i].EntryID;
                    x.UserID = Data[i].UserID;
                    User y = (from z in context.Users where z.UserID == x.UserID select z).FirstOrDefault();
                    x.UserName = y.UserName;
                    OnDisplay.Add(x);
                }
            }
        }

        #endregion //find by date

        #region FindByUserCommand

        public bool CanFindByUser(object obj)
        {
            if (SelectedUser == null)
                return false;
            return true;
        }

        public void FindByUser(object obj)
        {
            OnDisplay.Clear();
            ObservableCollection<Entry> Data = new ObservableCollection<Entry>(context.Entrys);
            for (int i = 0; i < Data.Count(); ++i)
            {
                if (Data[i].UserID == SelectedUser.UserID)
                {
                    EntryElement x = new EntryElement();
                    x.EntryDate = Data[i].Entry_date;
                    x.EntryDateStr = x.EntryDate.ToString("dd.MM.yyyy HH:mm tt");
                    x.EntryID = Data[i].EntryID;
                    x.UserID = Data[i].UserID;
                    User y = (from z in context.Users where z.UserID == x.UserID select z).FirstOrDefault();
                    x.UserName = y.UserName;
                    OnDisplay.Add(x);
                }
            }
        }

        #endregion //find by user

        #region CancelFindCommand

        public bool CanCancelFind(object obj)
        {
            //if nothing was find
            return true;
        }

        public void CancelFind(object obj)
        {
            OnDisplay = new ObservableCollection<EntryElement>();
            ObservableCollection<Entry> Data = new ObservableCollection<Entry>(context.Entrys);
            for (int i = 0; i < Data.Count(); ++i)
            {
                EntryElement x = new EntryElement();
                x.EntryDate = Data[i].Entry_date;
                x.EntryDateStr = x.EntryDate.ToString("dd.MM.yyyy HH:mm tt");
                x.EntryID = Data[i].EntryID;
                x.UserID = Data[i].UserID;
                User y = (from z in context.Users where z.UserID == x.UserID select z).FirstOrDefault();
                x.UserName = y.UserName;
                OnDisplay.Add(x);
            }
        }

        #endregion

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region IDisposable Closing context
        public void Dispose()
        {
            context.Dispose();
        }
        #endregion
    }
}
