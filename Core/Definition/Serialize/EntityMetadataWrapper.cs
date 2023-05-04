using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Definition.Serialize
{
    [Serializable]
    public class EntityMetadataWrapper
    {
        public EntityMetadataWrapper(EntityMetadata entityMetadata)
        {
            this.LogicalName = entityMetadata.LogicalName;
            this.PrimaryIdAttribute = entityMetadata.PrimaryIdAttribute;
            this.PrimaryNameAttribute = entityMetadata.PrimaryNameAttribute;
            this.Attributes = entityMetadata.Attributes.Where(
                            x => (x.IsValidForCreate != null && x.IsValidForCreate.Value)
                               && (!string.IsNullOrWhiteSpace(x.DisplayName.UserLocalizedLabel?.Label))
                               && (x.IsValidForRead != null && x.IsValidForRead.Value)).Select(x=> new AttributeMetadataWrapper(x)).ToList();
        }
        public string LogicalName { get; set; }
        public string PrimaryNameAttribute { get; set; }
        public string PrimaryIdAttribute { get; set; }
        public List<AttributeMetadataWrapper> Attributes { get; set; }

        public static EntityMetadataWrapper GetEntityMetadataWrapper(IOrganizationService service, string entityLogicalName)
        {
            if (service == null)
                return null;
            if (string.IsNullOrWhiteSpace(entityLogicalName))
            {
                throw new ArgumentNullException("A parameter entityLogicalName must be not null or empty.");
            }

            return new EntityMetadataWrapper(CrmSdkMessages.RetrieveEntity(service, entityLogicalName, EntityFilters.Attributes));


        }
    }
}
