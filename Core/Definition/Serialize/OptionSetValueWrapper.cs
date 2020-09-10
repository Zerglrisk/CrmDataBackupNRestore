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
    public class OptionSetValueWrapper
    {
        private int _value;
        [DataMember]
        public int Value
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

        public OptionSetValueWrapper()
        {

        }
        public OptionSetValueWrapper(int value)
        {
            this._value = value;
        }
        public OptionSetValueWrapper(OptionSetValue optionSetValue)
        {
            this._value = optionSetValue.Value;
        }
        public override bool Equals(object obj)
        {
            if (obj is OptionSetValueWrapper optionSetValueWrapper)
            {
                return this == optionSetValueWrapper || this._value.Equals(optionSetValueWrapper._value);
            }
            if (!(obj is OptionSetValue optionSetValue))
                return false;
            return  this._value.Equals(optionSetValue.Value);
        }

        /// <summary>Gets a hash code for the value.</summary>
        /// <returns>Type: Int32
        /// The hash code for the value.</returns>
        public override int GetHashCode()
        {
            return this._value.GetHashCode();
        }
    }
}
