using UsFormAdmin.Application.Forms;
using UsFormAdmin.Application.Form;

namespace UsFormAdmin.Application.Forms.Delete;

public class DeleteFormService
{
    private readonly IFormRepository _repo;

    public DeleteFormService(IFormRepository repo)
    {
        _repo = repo;
    }

    public Task<bool> ExecuteAsync(Guid id) => _repo.DeleteAsync(id);
}
