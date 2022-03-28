namespace Activity.Api.Services
{
	public interface IBackendService
	{
		Task<MyActivity?> GetNewActity(CancellationToken cancellationToken = default);
	}
}
