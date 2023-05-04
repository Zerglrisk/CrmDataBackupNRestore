using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Definition.Serialize
{
    [Serializable]
    public class AttributeMetadataWrapper
    {
        public AttributeMetadataWrapper(AttributeMetadata attributeMetadata)
        {
            this.LogicalName = attributeMetadata.LogicalName;
            this.AttributeType = attributeMetadata.AttributeType;
            this.IsCustomAttribute = attributeMetadata.IsCustomAttribute;
        }
        public string LogicalName { get; set; }
        public AttributeTypeCode? AttributeType { get; }
        public bool? IsCustomAttribute { get; }

    }
}
