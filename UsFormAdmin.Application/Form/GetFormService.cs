
using UsFormAdmin.Application.Form;


namespace UsFormAdmin.Application.Forms.Queries;

public class GetFormService
{
    private readonly IFormRepository _repo;

    public GetFormService(IFormRepository repo)
    {
        _repo = repo;
    }

    public Task<Model.Form.Form?> ExecuteAsync(Guid id) => _repo.GetByIdAsync(id);
}
