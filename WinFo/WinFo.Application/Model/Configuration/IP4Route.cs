namespace WinFo.Model.Configuration
{
    /// <summary>
    /// Information about an ipv4 route
    /// </summary>
    public class IP4Route
    {
        #region fields
        private int _age;
        private string _caption;
        private string _destination;
        private int _interfaceIndex;
        private string _mask;
        private int _metric1;
        private int _metric2;
        private int _metric3;
        private int _metric4;
        private int _metric5;
        private string _name;
        private string _protocol;
        #endregion

        #region properties
        public int Age { get => _age; set => _age = value; }
        public string Caption { get => _caption; set => _caption = value; }
        public string Destination { get => _destination; set => _destination = value; }
        public int InterfaceIndex { get => _interfaceIndex; set => _interfaceIndex = value; }
        public string Mask { get => _mask; set => _mask = value; }
        public int Metric1 { get => _metric1; set => _metric1 = value; }
        public int Metric2 { get => _metric2; set => _metric2 = value; }
        public int Metric3 { get => _metric3; set => _metric3 = value; }
        public int Metric4 { get => _metric4; set => _metric4 = value; }
        public int Metric5 { get => _metric5; set => _metric5 = value; }
        public string Name { get => _name; set => _name = value; }
        public string Protocol { get => _protocol; set => _protocol = value; }
        #endregion
    }
}
