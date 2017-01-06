using System;
using System.IO;
using Ical.Net.Interfaces.DataTypes;
using Ical.Net.Interfaces.General;
using Ical.Net.Serialization.iCalendar.Serializers.DataTypes;

namespace Ical.Net.DataTypes
{
    /// <summary>
    /// A class that represents the return status of an iCalendar request.
    /// </summary>
    public class RequestStatus : EncodableDataType, IRequestStatus, ICloneable
    {
        private string _mDescription;
        private string _mExtraData;
        private IStatusCode _mStatusCode;

        public virtual string Description
        {
            get { return _mDescription; }
            set { _mDescription = value; }
        }

        public virtual string ExtraData
        {
            get { return _mExtraData; }
            set { _mExtraData = value; }
        }

        public virtual IStatusCode StatusCode
        {
            get { return _mStatusCode; }
            set { _mStatusCode = value; }
        }

        public RequestStatus() {}

        public RequestStatus(string value) : this()
        {
            var serializer = new RequestStatusSerializer();
            CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
        }

        protected RequestStatus(RequestStatus other) : base(other)
        {
            Description = other.Description == null
                ? null
                : string.Copy(other.Description);

            ExtraData = other.ExtraData == null
                ? null
                : string.Copy(other.ExtraData);

            StatusCode = other.StatusCode.Clone() as StatusCode;
        }

        public override void CopyFrom(ICopyable obj)
        {
            //base.CopyFrom(obj);
            //if (!(obj is IRequestStatus))
            //{
            //    return;
            //}

            //var rs = (IRequestStatus) obj;
            //if (rs.StatusCode != null)
            //{
            //    StatusCode = rs.StatusCode;
            //}
            //Description = rs.Description;
            //rs.ExtraData = rs.ExtraData;
            base.CopyFrom(obj);
            var copy = obj as RequestStatus;
            if (copy == null)
            {
                return;
            }

            Description = copy.Description == null
                ? null
                : string.Copy(copy.Description);

            ExtraData = copy.ExtraData == null
                ? null
                : string.Copy(copy.ExtraData);

            StatusCode = copy.StatusCode.Clone() as StatusCode;
        }

        public override object Clone()
        {
            return new RequestStatus(this);
            //var clone = base.Clone() as RequestStatus;
            //if (clone == null)
            //{
            //    return null;
            //}

            //clone.Description = Description == null
            //    ? null
            //    : string.Copy(Description);

            //clone.ExtraData = ExtraData == null
            //    ? null
            //    : string.Copy(ExtraData);

            //clone.StatusCode = StatusCode.Clone() as StatusCode;
            //return clone;
        }

        public override string ToString()
        {
            var serializer = new RequestStatusSerializer();
            return serializer.SerializeToString(this);
        }

        protected bool Equals(RequestStatus other)
        {
            return string.Equals(_mDescription, other._mDescription) && string.Equals(_mExtraData, other._mExtraData) &&
                   Equals(_mStatusCode, other._mStatusCode);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((RequestStatus) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _mDescription?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (_mExtraData?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (_mStatusCode?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}