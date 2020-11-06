using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Core.Definition.Serialize;
using DocumentFormat.OpenXml.Drawing.Diagrams;
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

        private AttributeCollection AttributeDeserialize()
        {
            var ac = new AttributeCollection();

            // Need Fix : Attributes (createdon -> overriddencreatedon) If OnCreate
            if (this.Attributes.ContainsKey("createdon"))
            {
                //if OnCreate
                this.Attributes.RenameKey("createdon", "overriddencreatedon");
            }

            foreach (var attr in this.Attributes)
            {
                if (attr.Value is OptionSetValueWrapper wrapper)
                {
                    ac.Add(attr.Key, wrapper.ToOptionSetValue());
                }
                else if (attr.Value is OptionSetValueCollectionWrapper collectionWrapper)
                {
                    ac.Add(attr.Key, collectionWrapper.ToOptionSetValueCollection());
                }
                else
                {
                    ac.Add(attr.Key, attr.Value);
                }
            }

            return ac;
        }

        public Entity GenerateEntity()
        {
            var entity = new Entity(this.LogicalName);
            if (this.Id != Guid.Empty)
            {
                entity.Id = this.Id;
            }

            entity.Attributes = this.AttributeDeserialize();

            return entity;
        }
        
        //public Dictionary<string, object> ToDictionary(this AttributeCollection attributeCollection)
        //{
        //    return Attributes.ToDictionary(x => x.Key, x => x.Value);
        //}
    }

    public static class EntityWrapperExtender
    {
        public static EntityCollection Deserialize(this IEnumerable<EntityWrapper> entityWrappers)
        {
            var enumerable = entityWrappers.ToList();
            var entities = enumerable.Select(entity => entity.GenerateEntity()).ToList();

            return new EntityCollection(entities){EntityName = enumerable.FirstOrDefault()?.LogicalName};
        }
    }
}
