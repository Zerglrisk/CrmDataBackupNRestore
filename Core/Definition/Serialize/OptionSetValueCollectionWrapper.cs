using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace Core.Definition.Serialize
{
    [Serializable]
    public class OptionSetValueCollectionWrapper
    {
        private List<OptionSetValueWrapper> _value;
        [DataMember]
        public List<OptionSetValueWrapper> Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }

        public OptionSetValueCollectionWrapper()
        {
            this.Value = new List<OptionSetValueWrapper>();
        }
        public OptionSetValueCollectionWrapper(OptionSetValueCollection optionSetValueCollection)
        {
            this.Value = new List<OptionSetValueWrapper>();
            foreach (var osv in optionSetValueCollection)
            {
                this._value.Add(new OptionSetValueWrapper(osv));
            }
        }
        //public override bool Equals(object obj)
        //{
        //    this.Value = new List<OptionSetValueWrapper>();

        //    if (obj is OptionSetValueCollectionWrapper OptionSetValueCollectionWrapper)
        //    {
        //        return this == OptionSetValueCollectionWrapper || this._value.Equals(OptionSetValueCollectionWrapper._value);
        //    }
        //    if (!(obj is OptionSetValue optionSetValue))
        //        return false;
        //    return this._value.Equals(optionSetValue.Value);
        //}

        /// <summary>Gets a hash code for the value.</summary>
        /// <returns>Type: Int32
        /// The hash code for the value.</returns>
        public override int GetHashCode()
        {
            return this._value.GetHashCode();
        }
    }
}
