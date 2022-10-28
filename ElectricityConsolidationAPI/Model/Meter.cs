namespace ElectricityConsolidationAPI.Model
{
    public class Meter
    {
        private long _id;

        public long Id
        {
            get { return _id; }
        }

        public Meter(long id)
        {
            _id = id;
        }
    }
}
