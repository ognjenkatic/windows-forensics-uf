using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Service;
using WinFo.Service.Usage;
using WinFo.Usage.Model;

namespace WinFo.ViewModel
{
    /// <summary>
    /// The view model for the user session
    /// </summary
    public class UserSessionViewModel : BaseViewModel
    {
        #region fields
        private string[] _days = new[]
            {
                "Sunday",
                "Monday",
                "Tuesday",
                "Wednesday",
                "Thursday",
                "Friday",
                "Saturday"

            };

        private string[] _intervals = new string[]
            {
                 "00:00 - 02:00" ,
                 "02:00 - 04:00" ,
                 "04:00 - 06:00" ,
                 "06:00 - 08:00" ,
                 "08:00 - 10:00" ,
                 "10:00 - 12:00" ,
                 "12:00 - 14:00" ,
                 "14:00 - 16:00" ,
                 "16:00 - 18:00" ,
                 "18:00 - 20:00" ,
                 "20:00 - 22:00" ,
                 "22:00 - 24:00"
            };

        private string[] _hourIntervals = new string[]
            {
                 "1 Hr" ,
                 "2 Hrs" ,
                 "3 Hrs" ,
                 "4 Hrs" ,
                 "5 Hrs" ,
                 "6 Hrs" ,
                 "7 Hrs" ,
                 "8 Hrs" ,
                 "9 Hrs" ,
                 "10 Hrs" ,
                 "11 Hrs" ,
                 "12 Hrs" ,
                 "13 Hrs" ,
                 "14 Hrs" ,
                 "15 Hrs" ,
                 "16 Hrs" ,
                 "17 Hrs" ,
                 "18 Hrs" ,
                 "19 Hrs" ,
                 "20 Hrs" ,
                 "21 Hrs" ,
                 "22 Hrs" ,
                 "23 Hrs" ,
                 "24+ Hrs" ,
            };
        
        private ObservableCollection<UserSession> _userSessions;
        private SeriesCollection _logonLogoffSeriesCollection;
        private SeriesCollection _sessionDurationSeriesCollection;
        private SeriesCollection _sessionDurationByWeekDaySeriesCollection;
        
        private Dictionary<string, int[]> _userLoginHoursCounts;
        private Dictionary<string, int[]> _userLogoffHoursCounts;
        private Dictionary<string, int[]> _userSessionDurationCounts;

        private Dictionary<string, Dictionary<int, List<int>>> _userSessionDurationByDayOfWeek;

        private List<string> _users;
        
        #endregion

        #region properties
        public SeriesCollection SessionDurationByWeekDaySeriesCollection
        {
            get
            {
                return _sessionDurationByWeekDaySeriesCollection;
            }
            set
            {
                if(_sessionDurationByWeekDaySeriesCollection != value)
                {
                    _sessionDurationByWeekDaySeriesCollection = value;
                    RaisePropertyChanged("SessionDurationByWeekDaySeriesCollection");
                }
            }
        }
        
        public SeriesCollection SessionDurationSeriesCollection
        {
            get
            {
                return _sessionDurationSeriesCollection;
            }
            set
            {
                if (_sessionDurationSeriesCollection != value)
                {
                    _sessionDurationSeriesCollection = value;
                    RaisePropertyChanged("SessionDurationSeriesCollection");
                }
            }
        }

        public SeriesCollection LogonLogoffSeriesCollection
        {
            get
            {
                return _logonLogoffSeriesCollection;
            }
            set
            {
                if(_logonLogoffSeriesCollection != value)
                {
                    _logonLogoffSeriesCollection = value;
                    RaisePropertyChanged("LogonLogoffSeriesCollection");
                }
            }
        }

        public string[] HourIntervals
        {
            get
            {
                return _hourIntervals;
            }
            set
            {
                if(_hourIntervals != value)
                {
                    _hourIntervals = value;
                    RaisePropertyChanged("HourIntervals");
                }
            }
        }
        public List<string> Users
        {
            get
            {
                return _users;
            }
            set
            {
                if(_users != value)
                {
                    _users = value;
                    RaisePropertyChanged("Users");
                }
            }
        }

        public Dictionary<string, int[]> UserSessionDurationCounts
        {
            get
            {
                return _userSessionDurationCounts;
            }
            set
            {
                if(_userSessionDurationCounts != value)
                {
                    _userSessionDurationCounts = value;
                    RaisePropertyChanged("UserSessionDurationCounts");
                }
            }
        }


        public string[] Days
        {
            get
            {
                return _days;
            }
            set
            {
                if(_days != value)
                {
                    _days = value;
                    RaisePropertyChanged("Days");
                }
            }
        }

        public Dictionary<string,int[]> UserLoginHoursCounts
        {
            get
            {
                return _userLoginHoursCounts;
            }
            set
            {
                if(_userLoginHoursCounts != value)
                {
                    _userLoginHoursCounts = value;
                    RaisePropertyChanged("UserLoginHoursCounts");
                }
            }
        }

        public Dictionary<string, int[]> UserLogoffHoursCounts
        {
            get
            {
                return _userLogoffHoursCounts;
            }
            set
            {
                if (_userLogoffHoursCounts != value)
                {
                    _userLogoffHoursCounts = value;
                    RaisePropertyChanged("UserLogoffHoursCounts");
                }
            }
        }

        public string[] Intervals
        {
            get
            {
                return _intervals;
            }
            set
            {
                if(_intervals != value)
                {
                    _intervals = value;
                    RaisePropertyChanged("Intervals");
                }
            }
        }
        /// <summary>
        /// A collection of user sessions
        /// </summary>
        public ObservableCollection<UserSession> UserSessions
        {
            get
            {
                return _userSessions;
            }
            set
            {
                _userSessions = value;
                RaisePropertyChanged("UserSessions");
            }
        }
        #endregion

        public async void AsyncUpdateUserSessionInformation()
        {
            IsModelInformationBeingUpdated = true;
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();
            IUserSessionService iuss = sf.CreateUserSessionService();

            iuss.UpdateProgress += UpdateModelInformation;

            List<UserSession> sessions = await Task.Run(() =>
             {
                 return iuss.GetUserSessions();
             });

            if (sessions != null)
            {
                UserSessions = new ObservableCollection<UserSession>();
                LogonLogoffSeriesCollection = new SeriesCollection();
                SessionDurationSeriesCollection = new SeriesCollection();
                SessionDurationByWeekDaySeriesCollection = new SeriesCollection();

                UserLoginHoursCounts = new Dictionary<string, int[]>();
                UserLogoffHoursCounts = new Dictionary<string, int[]>();
                UserSessionDurationCounts = new Dictionary<string, int[]>();

                _userSessionDurationByDayOfWeek = new Dictionary<string, Dictionary<int, List<int>>>();
                Users = new List<string>();

                foreach (UserSession session in sessions)
                {
                    UserSessions.Add(session);

                    //TO-DO clean up and optimize
                    int day = (int)session.Beginning.DayOfWeek;

                    int logonHour = session.Beginning.Hour;
                    int logoffHour = session.End.Hour;

                    int durationDays = (int)Math.Floor((session.Beginning.Hour + (session.Beginning.Minute / 60) + session.Duration.TotalHours) / 24);

                    int firstDayDuration = (logoffHour < logonHour || session.Duration.TotalHours >= 24) ? firstDayDuration = 24 - logonHour : 0;
                    int leftoverDayDuration = (int)Math.Floor((session.Duration.TotalHours - firstDayDuration) % 24);

                    int logonHourIndex = (int)Math.Ceiling(logonHour / 2.0) - 1;
                    int logoffHourIndex = (int)Math.Ceiling(logoffHour / 2.0) - 1;

                    logonHourIndex = logonHourIndex < 0 ? 0 : logonHourIndex;
                    logoffHourIndex = logoffHourIndex < 0 ? 0 : logoffHourIndex;

                    if (!Users.Contains(session.Username))
                        Users.Add(session.Username);

                    if (!UserLoginHoursCounts.ContainsKey(session.Username))
                        UserLoginHoursCounts.Add(session.Username, new int[12]);

                    if (!UserLogoffHoursCounts.ContainsKey(session.Username))
                        UserLogoffHoursCounts.Add(session.Username, new int[12]);

                    if (!UserSessionDurationCounts.ContainsKey(session.Username))
                        UserSessionDurationCounts.Add(session.Username, new int[24]);

                    if (!_userSessionDurationByDayOfWeek.ContainsKey(session.Username))
                    {
                        _userSessionDurationByDayOfWeek.Add(session.Username, new Dictionary<int, List<int>>());
                        for(int i = 0; i < 7; i++)
                        {
                            _userSessionDurationByDayOfWeek[session.Username].Add(i, new List<int>());
                        }
                    }

                    if (session.Duration.TotalHours < 23)
                        UserSessionDurationCounts[session.Username][(int)Math.Floor((double)session.Duration.Hours)]++;
                    else
                        UserSessionDurationCounts[session.Username][23]++;

                    UserLoginHoursCounts[session.Username][logonHourIndex]++;
                    UserLogoffHoursCounts[session.Username][logoffHourIndex]++;


                    for (int i = 0; i < durationDays; i++)
                    {
                        if (i == 0)
                            _userSessionDurationByDayOfWeek[session.Username][(day + i) % 7].Add(firstDayDuration);
                        else
                            _userSessionDurationByDayOfWeek[session.Username][(day + i) % 7].Add(24);
                    }

                   
                    _userSessionDurationByDayOfWeek[session.Username][(day + durationDays) % 7].Add(leftoverDayDuration);

                }

               foreach (KeyValuePair<string, Dictionary<int, List<int>>> userdata in _userSessionDurationByDayOfWeek)
                {
                    ColumnSeries cs = new ColumnSeries();
                    cs.Title = "Hours - "+userdata.Key;

                    cs.Values = new ChartValues<int>();

                    foreach (KeyValuePair<int, List<int>> day in userdata.Value)
                    {
                        day.Value.Sort();

                        int median = 0;
                        //TO-DO fix displaying hours for outliers. Activity in some week days may not be representative if it is not repeated often enough
                        if (day.Value.Count > 0)
                            median = day.Value[(int)Math.Floor((double)(day.Value.Count / 2))];

                        cs.Values.Add(median);
                    }

                    SessionDurationByWeekDaySeriesCollection.Add(cs);
                }

                foreach (string usrn in UserLoginHoursCounts.Keys)
                {
                    ColumnSeries cs = new ColumnSeries();
                    cs.Title = "Logons - "+usrn;
                    cs.Values = new ChartValues<int>();
                    foreach (int day in UserLoginHoursCounts[usrn])
                    {
                        cs.Values.Add(day);
                    }
                    LogonLogoffSeriesCollection.Add(cs);
                }

                foreach (string usrn in UserLogoffHoursCounts.Keys)
                {
                    ColumnSeries cs = new ColumnSeries();
                    cs.Title = "Logoffs - "+usrn;
                    cs.Values = new ChartValues<int>();
                    foreach (int day in UserLogoffHoursCounts[usrn])
                    {
                        cs.Values.Add(day);
                    }

                    LogonLogoffSeriesCollection.Add(cs);
                }

                foreach (string usrn in UserSessionDurationCounts.Keys)
                {
                    ColumnSeries cs = new ColumnSeries();
                    cs.Title = "Sessions - "+usrn;
                    cs.Values = new ChartValues<int>();
                    foreach (int day in UserSessionDurationCounts[usrn])
                    {
                        cs.Values.Add(day);
                    }

                    SessionDurationSeriesCollection.Add(cs);
                }
            }

            IsModelInformationBeingUpdated = false;
        }
        public UserSessionViewModel()
        {
            AsyncUpdateUserSessionInformation();
        }
    }
}
