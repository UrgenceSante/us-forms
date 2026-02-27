namespace UsFormAdmin.Application.Form;

using UsFormAdmin.Model.Form;

public interface IFormRepository
{
    Task AddAsync(Form form);
    Task<Form?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Form>> ListAsync();
    Task<bool> DeleteAsync(Guid id);
}