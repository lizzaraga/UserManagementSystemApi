namespace UserManagementSystem.Database.Interfaces;

public interface IDateTrackedEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdateAt { get; set; }
}