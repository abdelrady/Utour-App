using System;

namespace ITI.Common.Utilities.General.Datetime
{
    public sealed class DateTimeObject
    {
        #region -- Local Variables --
        private readonly DateTime dt;
        #endregion

        #region -- Properties --
        public DateTime Value
        {
            get { return dt; }
        }
        #endregion

        #region -- Constructor --
        public DateTimeObject(DateTime dt)
        {
            this.dt = dt;
        }
        #endregion

        #region -- Public Methods --
        public override string ToString()
        {
            return dt.ToString();
        }
        #endregion
    }
}
