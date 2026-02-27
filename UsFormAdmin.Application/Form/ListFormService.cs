using UsFormAdmin.Application.Form;


namespace UsFormAdmin.Application.Forms.Queries;

public class ListFormsService
{
    private readonly IFormRepository _repo;

    public ListFormsService(IFormRepository repo)
    {
        _repo = repo;
    }

    public Task<IReadOnlyList<Model.Form.Form>> ExecuteAsync() => _repo.ListAsync();
}
