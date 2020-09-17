using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core.Definition.Serialize;
using Microsoft.Xrm.Sdk;

namespace Core.Definition
{
    [Serializable]
    public class EntityWrapper
    {
        private Dictionary<string, object> _attribute;
        public string LogicalName;
        public Guid Id;
        public Dictionary<string, object> Attributes
        {
            get => _attribute; 
            set { _attribute = value;
                AttributeSerialize();
            }
        }

        private void AttributeSerialize()
        {
            var keys = Attributes.Where(x => x.Value is OptionSetValue).Select(x=>x.Key).ToArray();
            foreach (var key in keys)
            {
                Attributes[key] =  new OptionSetValueWrapper(Attributes[key] as OptionSetValue);
            }

            keys = Attributes.Where(x => x.Value is OptionSetValueCollection).Select(x => x.Key).ToArray();
            foreach (var key in keys)
            {
                Attributes[key] = new OptionSetValueCollectionWrapper(Attributes[key] as OptionSetValueCollection);
            }
        }

        private void AttributeDeserialize()
        {

        }

        public Entity GenerateEntity()
        {
            var entity = new Entity(LogicalName);
            if (Id != Guid.Empty)
            {
                entity.Id = Id;
            }

            return entity;
        }
        
        //public Dictionary<string, object> ToDictionary(this AttributeCollection attributeCollection)
        //{
        //    return Attributes.ToDictionary(x => x.Key, x => x.Value);
        //}
    }
}
