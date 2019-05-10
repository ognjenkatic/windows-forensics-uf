using LiveCharts;
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
    /// The view model for the computer session
    /// </summary>
    public class ComputerSessionViewModel : BaseViewModel
    {
        #region fields
        private List<ComputerSession> _computerSessions;

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

        private SeriesCollection _startupShutdownSeriesCollection;
        private SeriesCollection _sessionDurationSeriesCollection;
        private SeriesCollection _sessionDurationByWeekDaySeriesCollection;

        private int[] _startupHoursCounts;
        private int[] _shutdownHoursCounts;
        private int[] _sessionDurationCounts;

        private Dictionary<int, List<int>> _sessionDurationByDayOfWeek;

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
                if (_sessionDurationByWeekDaySeriesCollection != value)
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

        public SeriesCollection StartupShutdownSeriesCollection
        {
            get
            {
                return _startupShutdownSeriesCollection;
            }
            set
            {
                if (_startupShutdownSeriesCollection != value)
                {
                    _startupShutdownSeriesCollection = value;
                    RaisePropertyChanged("StartupShutdownSeriesCollection");
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
                if (_hourIntervals != value)
                {
                    _hourIntervals = value;
                    RaisePropertyChanged("HourIntervals");
                }
            }
        }

        public int[] SessionDurationCounts
        {
            get
            {
                return _sessionDurationCounts;
            }
            set
            {
                if (_sessionDurationCounts != value)
                {
                    _sessionDurationCounts = value;
                    RaisePropertyChanged("SessionDurationCounts");
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
                if (_days != value)
                {
                    _days = value;
                    RaisePropertyChanged("Days");
                }
            }
        }

        public int[] StartupHoursCounts
        {
            get
            {
                return _startupHoursCounts;
            }
            set
            {
                if (_startupHoursCounts != value)
                {
                    _startupHoursCounts = value;
                    RaisePropertyChanged("StartupHoursCounts");
                }
            }
        }

        public int[] ShutdownHoursCounts
        {
            get
            {
                return _shutdownHoursCounts;
            }
            set
            {
                if (_shutdownHoursCounts != value)
                {
                    _shutdownHoursCounts = value;
                    RaisePropertyChanged("ShutdownHoursCounts");
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
                if (_intervals != value)
                {
                    _intervals = value;
                    RaisePropertyChanged("Intervals");
                }
            }
        }
        #endregion
        /// <summary>
        /// A collection of computer sessions
        /// </summary>
        public List<ComputerSession> ComputerSessions
        {
            get
            {
                return _computerSessions;
            }
            set
            {
                _computerSessions = value;
            }
        }

        public async void AsyncUpdateComputerSessionInformation()
        {
            IsModelInformationBeingUpdated = true;

            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IComputerSessionService css = sf.CreateComputerSessionService();

            css.UpdateProgress += UpdateModelInformation;

            ModelInformationUpdateProgress = "Loading computer session data...";

            ComputerSessions = await Task.Run(() =>
            {
                return css.GetComputerSessions();
            });

            RaisePropertyChanged("ComputerSessions");

            if (ComputerSessions != null && ComputerSessions.Count >0)
            {

                StartupShutdownSeriesCollection = new SeriesCollection();
                SessionDurationSeriesCollection = new SeriesCollection();
                SessionDurationByWeekDaySeriesCollection = new SeriesCollection();

                StartupHoursCounts = new int[12];
                ShutdownHoursCounts = new int[12];
                SessionDurationCounts = new int[24];

                _sessionDurationByDayOfWeek = new Dictionary<int, List<int>>();

                for(int i = 0; i < 7; i++)
                {
                    _sessionDurationByDayOfWeek.Add(i, new List<int>());
                }
                foreach (ComputerSession session in ComputerSessions)
                {
                    int day = (int)session.Beginning.DayOfWeek;


                    //TO-DO clean up and optimize
                    int logonHour = session.Beginning.Hour;
                    int logoffHour = session.End.Hour;

                    int durationDays = (int)Math.Floor((session.Beginning.Hour + (session.Beginning.Minute/60)+session.Duration.TotalHours )/ 24);
                   
                    int firstDayDuration = (logoffHour < logonHour || session.Duration.TotalHours >= 24) ? firstDayDuration = 24 - logonHour: 0;
                    int leftoverDayDuration = (int)Math.Floor((session.Duration.TotalHours-firstDayDuration) % 24);

                    int logonHourIndex = (int)Math.Ceiling(logonHour / 2.0) - 1;
                    int logoffHourIndex = (int)Math.Ceiling(logoffHour / 2.0) - 1;

                    logonHourIndex = logonHourIndex < 0 ? 0 : logonHourIndex;
                    logoffHourIndex = logoffHourIndex < 0 ? 0 : logoffHourIndex;
                    

                    if (session.Duration.TotalHours < 23)
                        SessionDurationCounts[(int)Math.Floor((double)session.Duration.Hours)]++;
                    else
                        SessionDurationCounts[23]++;

                    StartupHoursCounts[logonHourIndex]++;
                    ShutdownHoursCounts[logoffHourIndex]++;
                    
                    for(int i = 0; i < durationDays; i++)
                    {
                        if (i==0)
                            _sessionDurationByDayOfWeek[(day + i)%7].Add(firstDayDuration);
                        else
                            _sessionDurationByDayOfWeek[(day + i)%7].Add(24);
                    }

                    _sessionDurationByDayOfWeek[(day + durationDays)%7].Add(leftoverDayDuration);

                }

                ColumnSeries cs = new ColumnSeries();
                cs.Title = "Hours";
                cs.Values = new ChartValues<int>();

                foreach(KeyValuePair<int,List<int>> day in _sessionDurationByDayOfWeek)
                {
                    day.Value.Sort();

                    int median = 0;
                    //TO-DO fix displaying hours for outliers. Activity in some week days may not be representative if it is not repeated often enough
                    if (day.Value.Count > 0)
                        median = day.Value[(int)Math.Floor((double)(day.Value.Count / 2))];
                    
                    cs.Values.Add(median);
                }

                SessionDurationByWeekDaySeriesCollection.Add(cs);

                ColumnSeries cs_startup = new ColumnSeries();
                cs_startup.Title = "Startups";
                cs_startup.Values = new ChartValues<int>();
                foreach (int day in StartupHoursCounts)
                {
                    cs_startup.Values.Add(day);
                }

                StartupShutdownSeriesCollection.Add(cs_startup);

                ColumnSeries cs_shutdown = new ColumnSeries();
                cs_shutdown.Title = "Shutdowns";
                cs_shutdown.Values = new ChartValues<int>();
                foreach (int day in ShutdownHoursCounts)
                {
                    cs_shutdown.Values.Add(day);
                }

                StartupShutdownSeriesCollection.Add(cs_shutdown);

                ColumnSeries cs_sessions = new ColumnSeries();
                cs_sessions.Title = "Sessions";

                cs_sessions.Values = new ChartValues<int>();
                foreach (int day in SessionDurationCounts)
                {
                    cs_sessions.Values.Add(day);
                }

                SessionDurationSeriesCollection.Add(cs_sessions);
            }

            IsModelInformationBeingUpdated = false;
        }
        public ComputerSessionViewModel()
        {
            AsyncUpdateComputerSessionInformation();
        }
    }
}
