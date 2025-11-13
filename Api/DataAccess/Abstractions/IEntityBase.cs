namespace Api.DataAccess.Abstractions;

public interface IEntityBase<TId>
{
    TId Id { get; set; }
}