namespace CleanArchitecture.Domain.Common;

public abstract class BaseAuditableEntity: BaseEntity
{
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? LastModifiedTime { get; set; }
    public string? LastModifiedBy { get; set; }
}